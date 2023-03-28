using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Extensions;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Extensions;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetRatiosLiquidezByEmpresaIdQueryHandler : IRequestHandler<GetRatiosLiquidezByEmpresaIdQuery, GenericResult<RatioLiquidezResponse>>
{
    private readonly ILogger<GetRatiosLiquidezByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetRatiosLiquidezByEmpresaIdQueryHandler(
        ILogger<GetRatiosLiquidezByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<GenericResult<RatioLiquidezResponse>> Handle(GetRatiosLiquidezByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<RatioLiquidezResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => x.EmpresaId == request.EmpresaId && !x.Deleted.HasValue, null,
          x => x.Include(y => y.Empresa).Include(y => y.Ratios), false);

            if (documentos is not null && documentos.Any())
            {
                if (request.Usuario is not Usuario usuario)
                {
                    return result.Failed(401, "El usuario no tiene asignada la empresa a la que pertenece el documento");
                }

                var usuarioExistente = await unitOfWork.UsuarioRepository.GetFirstAsync(x => !x.Deleted.HasValue && x.UsuarioId == usuario.UsuarioId
                && x.Empresas.Any(t => t.EmpresaId == documentos.First().EmpresaId), null, x => x.Empresas);

                if (usuarioExistente is null)
                {
                    return result.Failed(401, "El usuario no tiene asignada la empresa a la que pertenece el documento");
                }

                var conceptos = await unitOfWork.InterpretacionRepository.GetAsync();

                var anualidad = DateTime.UtcNow.Year;

                // comprobación de la anualidad
                List<string> origenes = new() { Origen.BSS.ToString(), Origen.Modelo200.ToString() };

                var documents = documentos.Where(x => origenes.Contains(x.Origen) && x.Fecha.Year == anualidad
                                    && x.Ratios.Any(r => r.Concepto.ToLowerInvariant() == "liquidez"));

                while ((documents is null || !documents.Any()) && anualidad >= 2019)
                {
                    anualidad--;
                    documents = documentos.Where(x => origenes.Contains(x.Origen) && x.Fecha.Year == anualidad
                                    && x.Ratios.Any(r => r.Concepto.ToLowerInvariant() == "liquidez"));
                }

                var totalRatiosLiquidez = documentos.GetTotalRatiosByConcepto(anualidad, "liquidez");

                var interpretacionLiquidez = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "liquidez");

                var response = new TotalRatiosLiquidezDto
                {
                   RatioLiquidez = new TotalRatiosStringDto
                    {
                        Descripcion = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "liquidez")?.Nombre,
                        TotalActual = totalRatiosLiquidez.TotalActual.ToTwoDecimalAndSymbolFormat('c'),
                        TotalAnterior = totalRatiosLiquidez.TotalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                        TotalAnterior2 = totalRatiosLiquidez.TotalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                        Tendencia = totalRatiosLiquidez.Tendencia.ToTwoDecimalAndSymbolFormat('p'),
                        TendenciaAnterior = totalRatiosLiquidez.TendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                        Divisa = string.Empty,
                        Interpretacion = interpretacionLiquidez?.ToRatioInterpretacionDto(totalRatiosLiquidez.Tendencia, totalRatiosLiquidez.TendenciaAnterior)!
                    }
                };

                return result.Ok(new RatioLiquidezResponse 
                { 
                    Nombre = "Ratios de Liquidez",
                    Ratios = response,
                    Columnas = $"{new DateTime(anualidad, 12, 31):dd-MM-yyyy},Tendencia,{new DateTime(anualidad - 1, 12, 31):dd-MM-yyyy},Tendencia," +
                    $"{new DateTime(anualidad - 2, 12, 31):dd-MM-yyyy}"
                });
            }

            return result.Ok(new RatioLiquidezResponse());
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener los ratios de liquidez para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
