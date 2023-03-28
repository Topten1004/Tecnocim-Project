using System.Globalization;
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

public class GetRatiosCuentaResultadoByEmpresaIdQueryHandler : IRequestHandler<GetRatiosCuentaResultadoByEmpresaIdQuery, GenericResult<RatiosCuentaResultadoDto>>
{
    private readonly ILogger<GetRatiosCuentaResultadoByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetRatiosCuentaResultadoByEmpresaIdQueryHandler(
        ILogger<GetRatiosCuentaResultadoByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<GenericResult<RatiosCuentaResultadoDto>> Handle(GetRatiosCuentaResultadoByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<RatiosCuentaResultadoDto>();
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

                var conceptos = await unitOfWork.InterpretacionRepository.GetAsync(x => x.Concepto == "fondo maniobra" || x.Concepto == "EBITDA");

                var anualidad = DateTime.UtcNow.Year;

                // comprobación de la anualidad
                List<string> origenes = new() { Origen.BSS.ToString(), Origen.Modelo200.ToString() };

                var documents = documentos.Where(x => origenes.Contains(x.Origen) && x.Fecha.Year == anualidad
                                    && x.Ratios.Any(r => r.Concepto.ToLowerInvariant() == "fondo maniobra"));
                
                while ((documents is null || !documents.Any()) && anualidad >= 2019)
                {
                    anualidad--;
                    documents = documentos.Where(x => origenes.Contains(x.Origen) && x.Fecha.Year == anualidad
                                    && x.Ratios.Any(r => r.Concepto.ToLowerInvariant() == "fondo maniobra"));
                }


                var totalFondoManiobra = documentos.GetTotalRatiosByConcepto(anualidad, "fondo maniobra");
                var totalEbitda = documentos.GetTotalRatiosByConcepto(anualidad, "EBITDA".ToLowerInvariant());

                var interpretacionFondoManiobra = conceptos.FirstOrDefault(x => x.Concepto == "fondo maniobra");
                var interpretacionEbitda = conceptos.FirstOrDefault(x => x.Concepto == "EBITDA".ToLowerInvariant());

                return result.Ok(new RatiosCuentaResultadoDto
                {
                    Nombre = "Ratios de Cuenta Resultado",
                    Ratios = new RatiosRatiosCuentaResultadoDto
                    {
                        TotalRatiosFondoManiobra = new TotalRatiosStringDto
                        {
                            Descripcion = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "fondo maniobra")?.Nombre,
                            TotalActual = totalFondoManiobra.TotalActual.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior = totalFondoManiobra.TotalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior2 = totalFondoManiobra.TotalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                            Tendencia = totalFondoManiobra.Tendencia.ToTwoDecimalAndSymbolFormat('p'),
                            TendenciaAnterior = totalFondoManiobra.TendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                            Divisa = string.Empty,
                            Interpretacion = interpretacionFondoManiobra?.ToRatioInterpretacionDto(totalFondoManiobra.Tendencia, totalFondoManiobra.TendenciaAnterior)!
                        },
                        TotalRatiosEbitda = new TotalRatiosStringDto
                        {
                            Descripcion = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "ebitda")?.Nombre,
                            TotalActual = totalEbitda.TotalActual.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior = totalEbitda.TotalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior2 = totalEbitda.TotalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                            Tendencia = totalEbitda.Tendencia.ToTwoDecimalAndSymbolFormat('p'),
                            TendenciaAnterior = totalEbitda.TendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                            Divisa = string.Empty,
                            Interpretacion = interpretacionEbitda?.ToRatioInterpretacionDto(totalEbitda.Tendencia, totalEbitda.TendenciaAnterior)!
                        }
                    },
                    Columnas = $"{new DateTime(anualidad, 12, 31):dd-MM-yyyy},Tendencia,{new DateTime(anualidad - 1, 12, 31):dd-MM-yyyy},Tendencia," +
                    $"{new DateTime(anualidad - 2, 12, 31):dd-MM-yyyy}"
                });
            }

            return result.Ok(new RatiosCuentaResultadoDto());
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener los ratios de cuenta resultados para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
