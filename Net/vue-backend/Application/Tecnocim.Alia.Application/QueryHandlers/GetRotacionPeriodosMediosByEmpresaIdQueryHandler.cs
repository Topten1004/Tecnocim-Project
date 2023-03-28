using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Extensions;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetRotacionPeriodosMediosByEmpresaIdQueryHandler : IRequestHandler<GetRotacionPeriodosMediosByEmpresaIdQuery,
    GenericResult<RotacionPeriodosMediosResponse>>
{
    private readonly ILogger<GetRotacionPeriodosMediosByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetRotacionPeriodosMediosByEmpresaIdQueryHandler(
        ILogger<GetRotacionPeriodosMediosByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<GenericResult<RotacionPeriodosMediosResponse>> Handle(GetRotacionPeriodosMediosByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<RotacionPeriodosMediosResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            // Con contabilidades
            var documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue
                && x.EmpresaId == request.EmpresaId, null, x => x.Include(e => e.Contabilidades));

            if (documentos is { } && documentos.Any())
            {
                var anualidad = DateTime.UtcNow.Year;

                // comprobación de la anualidad
                var documentoAnyoActual = documentos.Where(x => x.Fecha.Year == anualidad)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();

                while (documentoAnyoActual is null && anualidad >= 2019)
                {
                    anualidad--;
                    documentoAnyoActual = documentos.Where(x => x.Fecha.Year == anualidad)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                }

                var documentoAnyoAnterior = documentos.Where(x => x.Fecha.Year == anualidad - 1)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                var documentoHaceDosAnyos = documentos.Where(x => x.Fecha.Year == anualidad - 2)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                var documentoHaceTresAnyos = documentos.Where(x => x.Fecha.Year == anualidad - 3)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();

                // Año actual
                var existenciasFinalesActual = documentoAnyoActual?.Contabilidades?.FirstOrDefault(c => c.Concepto == "existencias")?.Magnitud ?? 0;
                var existenciasInicialesActual = documentoAnyoAnterior?.Contabilidades?.FirstOrDefault(c => c.Concepto == "existencias")?.Magnitud ?? 0;
                var aprovisionamientos1Actual = documentoAnyoActual?.Contabilidades?.FirstOrDefault(c => c.Concepto == "aprovisionamientos")?.Magnitud ?? 0;
                var ratioTempActual = existenciasFinalesActual - aprovisionamientos1Actual - existenciasInicialesActual;

                // Año - 1
                var existenciasFinalesAnterior = documentoAnyoAnterior?.Contabilidades?.FirstOrDefault(c => c.Concepto == "existencias")?.Magnitud ?? 0;
                var existenciasInicialesAnterior = documentoHaceDosAnyos?.Contabilidades?.FirstOrDefault(c => c.Concepto == "existencias")?.Magnitud ?? 0;
                var aprovisionamientos1Anterior = documentoAnyoAnterior?.Contabilidades?.FirstOrDefault(c => c.Concepto == "aprovisionamientos")?.Magnitud ?? 0;
                var ratioTempAnterior = existenciasFinalesAnterior - aprovisionamientos1Anterior - existenciasInicialesAnterior;

                // Año - 2
                var existenciasFinalesHaceDosAnyos = documentoHaceDosAnyos?.Contabilidades?.FirstOrDefault(c => c.Concepto == "existencias")?.Magnitud ?? 0;
                var existenciasInicialesHaceDosAnyos = documentoHaceTresAnyos?.Contabilidades?.FirstOrDefault(c => c.Concepto == "existencias")?.Magnitud ?? 0;
                var aprovisionamientos1HaceDosAnyos = documentoHaceDosAnyos?.Contabilidades?.FirstOrDefault(c => c.Concepto == "aprovisionamientos")?.Magnitud ?? 0;
                var ratioTempHaceDosAnyos = existenciasFinalesHaceDosAnyos - aprovisionamientos1HaceDosAnyos - existenciasInicialesHaceDosAnyos;

                // Con Ratios
                documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue
                && x.EmpresaId == request.EmpresaId, null, x => x.Include(e => e.Ratios));

                documentoAnyoActual = documentos?.Where(x => x.Fecha.Year == anualidad)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                documentoAnyoAnterior = documentos?.Where(x => x.Fecha.Year == anualidad - 1)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                documentoHaceDosAnyos = documentos?.Where(x => x.Fecha.Year == anualidad - 2)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();

                var PMProveedoresActual = documentoAnyoActual?.Ratios?.FirstOrDefault(a => a.Concepto == "PM Proveedores")?.Magnitud ?? 0;
                var PMCobroActual = documentoAnyoActual?.Ratios?.FirstOrDefault(a => a.Concepto == "PM Cobro")?.Magnitud ?? 0;
                var PMAlmacenamientoActual = ratioTempActual != 0 ? 365 * ((existenciasFinalesActual + existenciasInicialesActual) / 2) / ratioTempActual : 0;
                var PMMaduracionActual = PMCobroActual + PMAlmacenamientoActual - PMProveedoresActual;

                var PMProveedoresAnterior = documentoAnyoAnterior?.Ratios?.FirstOrDefault(a => a.Concepto == "PM Proveedores")?.Magnitud ?? 0;
                var PMCobroAnterior = documentoAnyoAnterior?.Ratios?.FirstOrDefault(a => a.Concepto == "PM Cobro")?.Magnitud ?? 0;
                var PMAlmacenamientoAnterior = ratioTempAnterior != 0 ? 365 * ((existenciasFinalesAnterior + existenciasInicialesAnterior) / 2) / ratioTempAnterior : 0;
                var PMMaduracionAnterior = PMCobroAnterior + PMAlmacenamientoAnterior - PMProveedoresAnterior;

                var PMProveedoresHaceDosAnyos = documentoHaceDosAnyos?.Ratios?.FirstOrDefault(a => a.Concepto == "PM Proveedores")?.Magnitud ?? 0;
                var PMCobroHaceDosAnyos = documentoHaceDosAnyos?.Ratios?.FirstOrDefault(a => a.Concepto == "PM Cobro")?.Magnitud ?? 0;
                var PMAlmacenamientoHaceDosAnyos = ratioTempHaceDosAnyos != 0 ? 365 * ((existenciasFinalesHaceDosAnyos + existenciasInicialesHaceDosAnyos) / 2) / ratioTempHaceDosAnyos : 0;
                var PMMaduracionHaceDosAnyos = PMCobroHaceDosAnyos + PMAlmacenamientoHaceDosAnyos - PMProveedoresHaceDosAnyos;

                // Tendencias
                var tendenciaProveedores = PMProveedoresAnterior != 0 ? (PMProveedoresActual / PMProveedoresAnterior) - 1 : 0;
                var tendenciaProveedoresAnterior = PMProveedoresHaceDosAnyos != 0 ? (PMProveedoresAnterior / PMProveedoresHaceDosAnyos) - 1 : 0;

                var tendenciaCobro = PMCobroAnterior != 0 ? (PMCobroActual / PMCobroAnterior) - 1 : 0;
                var tendenciaCobroAnterior = PMCobroHaceDosAnyos != 0 ? (PMCobroAnterior / PMCobroHaceDosAnyos) - 1 : 0;

                var tendenciaAlmacenamiento = PMAlmacenamientoAnterior != 0 ? (PMAlmacenamientoActual / PMAlmacenamientoAnterior) - 1 : 0;
                var tendenciaAlmacenamientoAnterior = PMAlmacenamientoHaceDosAnyos != 0 ? (PMAlmacenamientoAnterior / PMAlmacenamientoHaceDosAnyos) - 1 : 0;

                var tendenciaMaduracion = PMMaduracionAnterior != 0 ? (PMMaduracionActual / PMMaduracionAnterior) - 1 : 0;
                var tendenciaMaduracionAnterior = PMMaduracionHaceDosAnyos != 0 ? (PMMaduracionAnterior / PMMaduracionHaceDosAnyos) - 1 : 0;

                var listado = new RatiosRotacionPeriodosMediosDto
                {
                    TotalPmProveedores = new TotalPrevisionDemandasStringDto
                    {
                        Concepto = "PM PROVEEDORES",
                        TotalActual = PMProveedoresActual.ToTwoDecimalAndSymbolFormat('c'),
                        Tendencia = tendenciaProveedores.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior = PMProveedoresAnterior.ToTwoDecimalAndSymbolFormat('c'),
                        TendenciaAnterior = tendenciaProveedoresAnterior.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior2 = PMProveedoresHaceDosAnyos.ToTwoDecimalAndSymbolFormat('c'),
                        Divisa = string.Empty
                    },

                    TotalPmCobro = new TotalPrevisionDemandasStringDto
                    {
                        Concepto = "PM COBRO",
                        TotalActual = PMCobroActual.ToTwoDecimalAndSymbolFormat('c'),
                        Tendencia = tendenciaCobro.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior = PMCobroAnterior.ToTwoDecimalAndSymbolFormat('c'),
                        TendenciaAnterior = tendenciaCobroAnterior.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior2 = PMCobroHaceDosAnyos.ToTwoDecimalAndSymbolFormat('c'),
                        Divisa = string.Empty
                    },

                    TotalPmAlmacenamiento = new TotalPrevisionDemandasStringDto
                    {
                        Concepto = "PM ALMACENAMIENTO",
                        TotalActual = PMAlmacenamientoActual.ToTwoDecimalAndSymbolFormat('c'),
                        Tendencia = tendenciaAlmacenamiento.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior = PMAlmacenamientoAnterior.ToTwoDecimalAndSymbolFormat('c'),
                        TendenciaAnterior = tendenciaAlmacenamientoAnterior.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior2 = PMAlmacenamientoHaceDosAnyos.ToTwoDecimalAndSymbolFormat('c'),
                        Divisa = string.Empty
                    },

                    TotalPmMaduracion = new TotalPrevisionDemandasStringDto
                    {
                        Concepto = "PM MADURACIÓN",
                        TotalActual = PMMaduracionActual.ToTwoDecimalAndSymbolFormat('c'),
                        Tendencia = tendenciaMaduracion.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior = PMMaduracionAnterior.ToTwoDecimalAndSymbolFormat('c'),
                        TendenciaAnterior = tendenciaMaduracionAnterior.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior2 = PMMaduracionHaceDosAnyos.ToTwoDecimalAndSymbolFormat('c'),
                        Divisa = string.Empty
                    }
                };

                var actual = documentoAnyoActual is null ? Models.Constants.SinDatos : documentoAnyoActual?.Fecha.ToString("dd-MM-yyyy");
                var anterior = documentoAnyoAnterior is null ? Models.Constants.SinDatos : documentoAnyoAnterior?.Fecha.ToString("dd-MM-yyyy");
                var haceDosAnyos = documentoHaceDosAnyos is null ? Models.Constants.SinDatos : documentoHaceDosAnyos?.Fecha.ToString("dd-MM-yyyy");

                var columnas = $"{actual},Tendencia,{anterior},Tendencia,{haceDosAnyos}";

                var rotacionPeriodosMedios = new RotacionPeriodosMediosResponse { Nombre = "Rotación y periodos medios", Columnas = columnas, Ratios = listado };

                return result.Ok(rotacionPeriodosMedios);
            }

            return result.NotFound();
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener la rotacion de periodos medios para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
