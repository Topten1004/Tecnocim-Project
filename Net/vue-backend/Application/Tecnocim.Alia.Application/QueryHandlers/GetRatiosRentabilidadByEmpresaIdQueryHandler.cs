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

public class GetRatiosRentabilidadByEmpresaIdQueryHandler : IRequestHandler<GetRatiosRentabilidadByEmpresaIdQuery, GenericResult<RatiosRentabilidadDto>>
{
    private readonly ILogger<GetRatiosRentabilidadByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetRatiosRentabilidadByEmpresaIdQueryHandler(
        ILogger<GetRatiosRentabilidadByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<GenericResult<RatiosRentabilidadDto>> Handle(GetRatiosRentabilidadByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<RatiosRentabilidadDto>();
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
                                    && x.Ratios.Any(r => r.Concepto.ToLowerInvariant() == "punto muerto"));

                while ((documents is null || !documents.Any()) && anualidad >= 2019)
                {
                    anualidad--;
                    documents = documentos.Where(x => origenes.Contains(x.Origen) && x.Fecha.Year == anualidad
                                    && x.Ratios.Any(r => r.Concepto.ToLowerInvariant() == "punto muerto"));
                }

                var totalRatiosPuntoMuerto = documentos.GetTotalRatiosByConcepto(anualidad, "punto muerto");
                var totalRatiosROE = documentos.GetTotalRatiosByConcepto(anualidad, "roe");
                var totalRatiosROAROI = documentos.GetTotalRatiosByConcepto(anualidad, "roa/roi");

                var interpretacionPuntoMuerto = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "punto muerto");
                var interpretacionROE = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "roe");
                var interpretacionROAROI = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "roa/roi");

                return result.Ok(new RatiosRentabilidadDto
                {
                    Nombre = "Ratios de Rentabilidad",
                    Ratios = new RatiosRatiosRentabilidadDto
                    {
                        TotalRatiosPuntoMuerto = new TotalRatiosStringDto
                        {
                            Descripcion = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "punto muerto")?.Nombre,
                            TotalActual = totalRatiosPuntoMuerto.TotalActual.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior = totalRatiosPuntoMuerto.TotalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior2 = totalRatiosPuntoMuerto.TotalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                            Tendencia = totalRatiosPuntoMuerto.Tendencia.ToTwoDecimalAndSymbolFormat('p'),
                            TendenciaAnterior = totalRatiosPuntoMuerto.TendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                            Divisa = string.Empty,
                            Interpretacion = interpretacionPuntoMuerto?.ToRatioInterpretacionDto(totalRatiosPuntoMuerto.Tendencia, totalRatiosPuntoMuerto.TendenciaAnterior)!
                        },
                        TotalRatiosROE = new TotalRatiosStringDto
                        {
                            Descripcion = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "roe")?.Nombre,
                            TotalActual = totalRatiosROE.TotalActual.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior = totalRatiosROE.TotalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior2 = totalRatiosROE.TotalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                            Tendencia = totalRatiosROE.Tendencia.ToTwoDecimalAndSymbolFormat('p'),
                            TendenciaAnterior = totalRatiosROE.TendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                            Divisa = string.Empty,
                            Interpretacion = interpretacionROE?.ToRatioInterpretacionDto(totalRatiosROE.Tendencia, totalRatiosROE.TendenciaAnterior)!
                        },
                        TotalRatiosROAROI = new TotalRatiosStringDto
                        {
                            Descripcion = conceptos.FirstOrDefault(x => x.Concepto.ToLowerInvariant() == "roa/roi")?.Nombre,
                            TotalActual = totalRatiosROAROI.TotalActual.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior = totalRatiosROAROI.TotalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior2 = totalRatiosROAROI.TotalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                            Tendencia = totalRatiosROAROI.Tendencia.ToTwoDecimalAndSymbolFormat('p'),
                            TendenciaAnterior = totalRatiosROAROI.TendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                            Divisa = string.Empty,
                            Interpretacion = interpretacionROAROI?.ToRatioInterpretacionDto(totalRatiosROAROI.Tendencia, totalRatiosROAROI.TendenciaAnterior)!
                        }
                    },
                    Columnas = $"{new DateTime(anualidad, 12, 31):dd-MM-yyyy},Tendencia,{new DateTime(anualidad - 1, 12, 31):dd-MM-yyyy},Tendencia," +
                    $"{new DateTime(anualidad - 2, 12, 31):dd-MM-yyyy}"
                });
            }

            return result.Ok(new RatiosRentabilidadDto());
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener los ratios de rentabilidad para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
