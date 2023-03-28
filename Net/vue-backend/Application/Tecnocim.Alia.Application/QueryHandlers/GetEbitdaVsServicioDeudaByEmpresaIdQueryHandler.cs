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

public class GetEbitdaVsServicioDeudaByEmpresaIdQueryHandler : IRequestHandler<GetEbitdaVsServicioDeudaByEmpresaIdQuery, GenericResult<VsServicioDeudaDto>>
{
    private readonly ILogger<GetEbitdaVsServicioDeudaByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetEbitdaVsServicioDeudaByEmpresaIdQueryHandler(
        ILogger<GetEbitdaVsServicioDeudaByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<GenericResult<VsServicioDeudaDto>> Handle(GetEbitdaVsServicioDeudaByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<VsServicioDeudaDto>();
        try
        {
            const string concepto = "EBITDA";
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

                var anualidad = request.Anualidad ?? DateTime.UtcNow.Year;

                if (anualidad.ToString().Length != 4)
                {
                    return result.Failed(400, "La anualidad no tiene el formato de año (4 dígitos)");
                }

                // chequeamos la anualidad
                List<string> origenes = new() { Origen.BSS.ToString(), Origen.Modelo200.ToString() };

                var documents = documentos.Where(x => origenes.Contains(x.Origen) && x.Fecha.Year == anualidad
                                    && x.Ratios.Any(r => r.Concepto.ToLowerInvariant() == concepto.ToLowerInvariant()));

                while ((documents is null || !documents.Any()) && anualidad >= 2019)
                {
                    anualidad--;
                    documents = documentos.Where(x => origenes.Contains(x.Origen) && x.Fecha.Year == anualidad
                                    && x.Ratios.Any(r => r.Concepto.ToLowerInvariant() == concepto.ToLowerInvariant()));
                }

                var totalRatioDto1 = documentos.GetTotalRatiosByConcepto(anualidad, concepto.ToLowerInvariant());

                var totalRatioDto2 = new TotalRatiosDto();
                var totalRatioDto3 = new TotalRatiosDto();

                var pools = await unitOfWork.PoolRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue && x.DocumentoId == documentos.First().DocumentoId, null,
                    x => x.Include(t => t.Documento).Include(t => t.Contrato).ThenInclude(t => t.Cuotas).Include(t => t.Contrato.EquivalenciasMoneda));

                var pool = pools.FirstOrDefault(x => x.Documento != null && !x.Documento.Deleted.HasValue && x.Documento.EmpresaId == request.EmpresaId);

                var divisa = string.Empty;

                if (pool is not null)
                {
                    divisa = pool.Contrato?.EquivalenciasMoneda?.Tipo ?? string.Empty;

                    var totalActual = pool.Contrato?.Cuotas?.Where(x => x.Fecha.Year == anualidad)?.Sum(x => x.Importe) ?? 0;
                    var totalAnterior = pool.Contrato?.Cuotas?.Where(x => x.Fecha.Year == anualidad - 1)?.Sum(x => x.Importe) ?? 0;
                    var totalAnterior2 = pool.Contrato?.Cuotas?.Where(x => x.Fecha.Year == anualidad - 2)?.Sum(x => x.Importe) ?? 0;
                    var tendencia = totalAnterior != 0 ? ((totalActual - totalAnterior) / totalAnterior) * 100 : 0;
                    var tendenciaAnterior = totalAnterior2 != 0 ? ((totalAnterior - totalAnterior2) / totalAnterior2) * 100 : 0;

                    totalRatioDto2.TotalActual = totalActual;
                    totalRatioDto2.TotalAnterior = totalAnterior;
                    totalRatioDto2.TotalAnterior2 = totalAnterior2;
                    totalRatioDto2.Tendencia = tendencia;
                    totalRatioDto2.TendenciaAnterior = tendenciaAnterior;

                    totalActual = totalRatioDto2.TotalActual != 0 ? totalRatioDto1.TotalActual / totalRatioDto2.TotalActual : 0;
                    totalAnterior = totalRatioDto2.TotalAnterior != 0 ? totalRatioDto1.TotalAnterior / totalRatioDto2.TotalAnterior : 0;
                    totalAnterior2 = totalRatioDto2.TotalAnterior2 != 0 ? totalRatioDto1.TotalAnterior2 / totalRatioDto2.TotalAnterior2 : 0;

                    totalRatioDto3.TotalActual = totalActual;
                    totalRatioDto3.TotalAnterior = totalAnterior;
                    totalRatioDto3.TotalAnterior2 = totalAnterior2;
                    totalRatioDto3.Tendencia = totalAnterior != 0 ? ((totalActual - totalAnterior) / totalAnterior) * 100 : 0;
                    totalRatioDto3.TendenciaAnterior = totalAnterior2 != 0 ? ((totalAnterior - totalAnterior2) / totalAnterior2) * 100 : 0;
                }

                var conceptos = await unitOfWork.InterpretacionRepository.GetAsync();

                var interpretacionEbitda = conceptos.FirstOrDefault(x => x.Concepto == concepto);
                var interpretacionServicioDeuda = conceptos.FirstOrDefault(x => x.Concepto == "servicio deuda");
                var interpretacionCobertura = conceptos.FirstOrDefault(x => x.Concepto == "porcentaje cobertura");

                return result.Ok(new VsServicioDeudaDto
                {
                    Nombre = "Servicio de ratios versus deuda",
                    Ratios = new RatiosVsServicioDeudaDto
                    {
                        TotalRatios1 = new TotalRatiosStringDto
                        {
                            Descripcion = conceptos.FirstOrDefault(x => x.Concepto == concepto)?.Nombre,
                            TotalActual = totalRatioDto1.TotalActual.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior = totalRatioDto1.TotalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior2 = totalRatioDto1.TotalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                            Tendencia = totalRatioDto1.Tendencia.ToTwoDecimalAndSymbolFormat('p'),
                            TendenciaAnterior = totalRatioDto1.TendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                            Divisa = divisa,
                            Interpretacion = interpretacionEbitda?.ToRatioInterpretacionDto(totalRatioDto1.Tendencia, totalRatioDto1.TendenciaAnterior)!
                        },
                        TotalRatios2 = new TotalRatiosStringDto
                        {
                            Descripcion = conceptos.FirstOrDefault(x => x.Concepto == "servicio deuda")?.Nombre,
                            TotalActual = totalRatioDto2.TotalActual.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior = totalRatioDto2.TotalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior2 = totalRatioDto2.TotalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                            Tendencia = totalRatioDto2.Tendencia.ToTwoDecimalAndSymbolFormat('p'),
                            TendenciaAnterior = totalRatioDto2.TendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                            Divisa = divisa,
                            Interpretacion = interpretacionServicioDeuda?.ToRatioInterpretacionDto(totalRatioDto2.Tendencia, totalRatioDto2.TendenciaAnterior)!
                        },
                        TotalRatios3 = new TotalRatiosStringDto
                        {
                            Descripcion = conceptos.FirstOrDefault(x => x.Concepto == "porcentaje cobertura")?.Nombre,
                            TotalActual = totalRatioDto3.TotalActual.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior = totalRatioDto3.TotalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                            TotalAnterior2 = totalRatioDto3.TotalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                            Tendencia = totalRatioDto3.Tendencia.ToTwoDecimalAndSymbolFormat('p'),
                            TendenciaAnterior = totalRatioDto3.TendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                            Divisa = divisa,
                            Interpretacion = interpretacionCobertura?.ToRatioInterpretacionDto(totalRatioDto3.Tendencia, totalRatioDto3.TendenciaAnterior)!
                        }
                    },
                    Columnas = $"{new DateTime(anualidad, 12, 31):dd-MM-yyyy},Tendencia,{new DateTime(anualidad - 1, 12, 31):dd-MM-yyyy},Tendencia," +
                    $"{new DateTime(anualidad - 2, 12, 31):dd-MM-yyyy}"
                });
            }

            return result.Ok(new VsServicioDeudaDto());
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener los ratios Ebitda Vs Servicio Deuda para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
