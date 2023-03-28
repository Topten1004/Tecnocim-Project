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

public class GetRatiosEndeudamientoByEmpresaIdQueryHandler : IRequestHandler<GetRatiosEndeudamientoByEmpresaIdQuery, GenericResult<RatiosEndeudamientoDto>>
{
    private readonly ILogger<GetRatiosEndeudamientoByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetRatiosEndeudamientoByEmpresaIdQueryHandler(
        ILogger<GetRatiosEndeudamientoByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<GenericResult<RatiosEndeudamientoDto>> Handle(GetRatiosEndeudamientoByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<RatiosEndeudamientoDto>();
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

                var conceptos = await unitOfWork.InterpretacionRepository.GetAsync(x => x.Concepto == "endeudamiento" || x.Concepto == "calidad deuda");

                var anualidad = DateTime.UtcNow.Year;

                // comprobación de la anualidad
                List<string> origenes = new() { Origen.BSS.ToString(), Origen.Modelo200.ToString() };

                var documents = documentos.Where(x => origenes.Contains(x.Origen) && x.Fecha.Year == anualidad
                                    && x.Ratios.Any(r => r.Concepto.ToLowerInvariant() == "endeudamiento"));

                while ((documents is null || !documents.Any()) && anualidad >= 2019)
                {
                    anualidad--;
                    documents = documentos.Where(x => origenes.Contains(x.Origen) && x.Fecha.Year == anualidad
                                    && x.Ratios.Any(r => r.Concepto.ToLowerInvariant() == "endeudamiento"));
                }

                var totalEndeudamiento = documentos.GetTotalRatiosByConcepto(anualidad, "endeudamiento", false);
                var totalCalidadDeuda = documentos.GetTotalRatiosByConcepto(anualidad, "calidad deuda", false);

                var interpretacionEndeudamiento = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "endeudamiento");
                var interpretacionCalidadDeuda = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "calidad deuda");

                return result.Ok(new RatiosEndeudamientoDto
                {
                    Nombre = "Ratios de Endeudamiento",
                    Ratios = new RatiosRatiosEndeudamientoDto
                    {
                        TotalRatiosEndeudamiento = new TotalRatiosStringDto
                        {
                            Descripcion = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "endeudamiento")?.Nombre,
                            TotalActual = totalEndeudamiento.TotalActual.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior = totalEndeudamiento.TotalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior2 = totalEndeudamiento.TotalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                            Tendencia = totalEndeudamiento.Tendencia.ToTwoDecimalAndSymbolFormat('p'),
                            TendenciaAnterior = totalEndeudamiento.TendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                            Divisa = string.Empty,
                            Interpretacion = interpretacionEndeudamiento?.ToRatioInterpretacionDto(totalEndeudamiento.Tendencia, totalEndeudamiento.TendenciaAnterior)!
                        },
                        TotalRatiosCalidadDeuda = new TotalRatiosStringDto
                        {
                            Descripcion = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "calidad deuda")?.Nombre,
                            TotalActual = totalCalidadDeuda.TotalActual.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior = totalCalidadDeuda.TotalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior2 = totalCalidadDeuda.TotalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                            Tendencia = totalCalidadDeuda.Tendencia.ToTwoDecimalAndSymbolFormat('p'),
                            TendenciaAnterior = totalCalidadDeuda.TendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                            Divisa = string.Empty,
                            Interpretacion = interpretacionCalidadDeuda?.ToRatioInterpretacionDto(totalCalidadDeuda.Tendencia, totalCalidadDeuda.TendenciaAnterior)!
                        }
                    },
                    Columnas = $"{new DateTime(anualidad, 12, 31):dd-MM-yyyy},Tendencia,{new DateTime(anualidad - 1, 12, 31):dd-MM-yyyy},Tendencia," +
                    $"{new DateTime(anualidad - 2, 12, 31):dd-MM-yyyy}"
                });
            }

            return result.Ok(new RatiosEndeudamientoDto());
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener los ratios de endeudamiento para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
