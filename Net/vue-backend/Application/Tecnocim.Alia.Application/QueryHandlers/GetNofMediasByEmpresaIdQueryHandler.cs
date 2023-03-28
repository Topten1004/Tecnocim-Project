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

public class GetNofMediasByEmpresaIdQueryHandler : IRequestHandler<GetNofMediasByEmpresaIdQuery,
    GenericResult<PrevisionDemandasResponse>>
{
    private readonly ILogger<GetNofMediasByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetNofMediasByEmpresaIdQueryHandler(
        ILogger<GetNofMediasByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<GenericResult<PrevisionDemandasResponse>> Handle(GetNofMediasByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<PrevisionDemandasResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue
                && x.EmpresaId == request.EmpresaId, null, x => x.Include(e => e.Contabilidades));

            var anualidad = DateTime.UtcNow.Year;

            if (documentos is { } && documentos.Any())
            {
                var documentoAnyoActual = documentos.Where(x => x.Fecha.Year == anualidad)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();

                // comprobación de la anualidad
                while (documentoAnyoActual is null && anualidad >= 2019)
                {
                    anualidad--;
                    documentoAnyoActual = documentos.Where(x => x.Fecha.Year == anualidad)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                }

                var documentoAnyoAnterior = documentos.Where(x => x.Fecha.Year == anualidad - 1)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                var documentoHaceDosAnyos = documentos.Where(x => x.Fecha.Year == anualidad - 2)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                var documentoHaceTresAnyos = documentos.Where(x => x.Fecha.Year == anualidad - 3)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();

                var ventasAnyoActual = documentoAnyoActual?.Ratios?.FirstOrDefault(a => a.Concepto == "ventas")?.Magnitud ?? 0;
                var cobroAnyoActual = documentoAnyoActual?.Ratios?.FirstOrDefault(a => a.Concepto == "PM cobro")?.Magnitud ?? 0;
                var aprovisionamientosAnyoActual = documentoAnyoActual?.Contabilidades?.FirstOrDefault(a => a.Concepto == "aprovisionamientos")?.Magnitud ?? 0;
                var proveedoresAnyoActual = documentoAnyoActual?.Ratios?.FirstOrDefault(a => a.Concepto == "PM proveedores")?.Magnitud ?? 0;
                var existenciasFinalesAnyoActual = documentoAnyoActual?.Contabilidades?.FirstOrDefault(a => a.Concepto == "existencias")?.Magnitud ?? 0;
                var existenciasInicialesAnyoActual = documentoAnyoAnterior?.Contabilidades?.FirstOrDefault(a => a.Concepto == "existencias")?.Magnitud ?? 0;
                var deudaAnyoActual = documentoAnyoActual?.Contabilidades?.FirstOrDefault(a => a.Concepto == "deuda con caracteristicas especiales a corto plazo")?.Magnitud ?? 0;

                var ventasAnyoAnterior = documentoAnyoAnterior?.Ratios?.FirstOrDefault(a => a.Concepto == "ventas")?.Magnitud ?? 0;
                var cobroAnyoAnterior = documentoAnyoAnterior?.Ratios?.FirstOrDefault(a => a.Concepto == "PM cobro")?.Magnitud ?? 0;
                var aprovisionamientosAnyoAnterior = documentoAnyoAnterior?.Contabilidades?.FirstOrDefault(a => a.Concepto == "aprovisionamientos")?.Magnitud ?? 0;
                var proveedoresAnyoAnterior = documentoAnyoAnterior?.Ratios?.FirstOrDefault(a => a.Concepto == "PM proveedores")?.Magnitud ?? 0;
                var existenciasFinalesAnyoAnterior = documentoAnyoAnterior?.Contabilidades?.FirstOrDefault(a => a.Concepto == "existencias")?.Magnitud ?? 0;
                var existenciasInicialesAnyoAnterior = documentoHaceDosAnyos?.Contabilidades?.FirstOrDefault(a => a.Concepto == "existencias")?.Magnitud ?? 0;
                var deudaAnyoAnterior = documentoAnyoAnterior?.Contabilidades?.FirstOrDefault(a => a.Concepto == "deuda con caracteristicas especiales a corto plazo")?.Magnitud ?? 0;

                var ventasHaceDosAnyos = documentoHaceDosAnyos?.Ratios?.FirstOrDefault(a => a.Concepto == "ventas")?.Magnitud ?? 0;
                var cobroHaceDosAnyos = documentoHaceDosAnyos?.Ratios?.FirstOrDefault(a => a.Concepto == "PM cobro")?.Magnitud ?? 0;
                var aprovisionamientosHaceDosAnyos = documentoHaceDosAnyos?.Contabilidades?.FirstOrDefault(a => a.Concepto == "aprovisionamientos")?.Magnitud ?? 0;
                var proveedoresHaceDosAnyos = documentoHaceDosAnyos?.Ratios?.FirstOrDefault(a => a.Concepto == "PM proveedores")?.Magnitud ?? 0;
                var existenciasFinalesHaceDosAnyos = documentoHaceDosAnyos?.Contabilidades?.FirstOrDefault(a => a.Concepto == "existencias")?.Magnitud ?? 0;
                var existenciasInicialesHaceDosAnyos = documentoHaceTresAnyos?.Contabilidades?.FirstOrDefault(a => a.Concepto == "existencias")?.Magnitud ?? 0;
                var deudaHaceDosAnyos = documentoHaceDosAnyos?.Contabilidades?.FirstOrDefault(a => a.Concepto == "deuda con caracteristicas especiales a corto plazo")?.Magnitud ?? 0;

                var nofMediasActual = (cobroAnyoActual != 0 && proveedoresAnyoActual != 0)
                    ? ((ventasAnyoActual * 0.012m) + (ventasAnyoActual / (365 * cobroAnyoActual)))
                        - (aprovisionamientosAnyoActual / (365 * proveedoresAnyoActual))
                        + ((existenciasFinalesAnyoActual + existenciasInicialesAnyoActual) / 2) - deudaAnyoActual
                    : 0;

                var nofMediasAnterior = (cobroAnyoAnterior != 0 && proveedoresAnyoAnterior != 0)
                    ? ((ventasAnyoAnterior * 0.012m) + (ventasAnyoAnterior / (365 * cobroAnyoAnterior)))
                        - (aprovisionamientosAnyoAnterior / (365 * proveedoresAnyoAnterior))
                        + ((existenciasFinalesAnyoAnterior + existenciasInicialesAnyoAnterior) / 2) - deudaAnyoAnterior
                    : 0;

                var nofMediasHaceDosAnyos = (cobroHaceDosAnyos != 0 && proveedoresHaceDosAnyos != 0)
                    ? ((ventasHaceDosAnyos * 0.012m) + (ventasHaceDosAnyos / (365 * cobroHaceDosAnyos)))
                        - (aprovisionamientosHaceDosAnyos / (365 * proveedoresHaceDosAnyos))
                        + ((existenciasFinalesHaceDosAnyos + existenciasInicialesHaceDosAnyos) / 2) - deudaHaceDosAnyos
                    : 0;

                var tendenciaNof = nofMediasAnterior != 0 ? (nofMediasActual / nofMediasAnterior) - 1 : 0;
                var tendenciaAnteriorNof = nofMediasHaceDosAnyos != 0 ? (nofMediasAnterior / nofMediasHaceDosAnyos) - 1 : 0;

                // con Ratios

                documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue
                    && x.EmpresaId == request.EmpresaId, null, x => x.Include(e => e.Ratios));

                documentoAnyoActual = documentos.Where(x => x.Fecha.Year == anualidad)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                documentoAnyoAnterior = documentos.Where(x => x.Fecha.Year == anualidad - 1)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                documentoHaceDosAnyos = documentos.Where(x => x.Fecha.Year == anualidad - 2)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();

                var totalActualFondoManiobra = documentoAnyoActual?.Ratios?.FirstOrDefault(r => r.Concepto == "fondo maniobra")?.Magnitud ?? 0;
                var totalAnteriorFondoManiobra = documentoAnyoAnterior?.Ratios?.FirstOrDefault(r => r.Concepto == "fondo maniobra")?.Magnitud ?? 0;
                var totalAnterior2FondoManiobra = documentoHaceDosAnyos?.Ratios?.FirstOrDefault(r => r.Concepto == "fondo maniobra")?.Magnitud ?? 0;
                var tendenciaFondoManiobra = totalAnteriorFondoManiobra != 0 ? (totalActualFondoManiobra / totalAnteriorFondoManiobra) - 1 : 0;
                var tendenciaAnteriorFondoManiobra = totalAnterior2FondoManiobra != 0 ? (totalAnteriorFondoManiobra / totalAnterior2FondoManiobra) - 1 : 0;

                var NRNMediasActual = nofMediasActual - totalActualFondoManiobra;
                var NRNMediasAnterior = nofMediasAnterior - totalAnteriorFondoManiobra;
                var NRNMediasAnterior2 = nofMediasHaceDosAnyos - totalAnterior2FondoManiobra;
                var tendenciaNRN = NRNMediasAnterior != 0 ? (NRNMediasActual / NRNMediasAnterior) - 1 : 0;
                var tendenciaAnteriorNRN = NRNMediasAnterior2 != 0 ? (NRNMediasAnterior / NRNMediasAnterior2) - 1 : 0;

                var listado = new List<TotalPrevisionDemandasStringDto>
                {
                    new TotalPrevisionDemandasStringDto
                    {
                        Concepto = "NOF MEDIAS",
                        TotalActual = nofMediasActual.ToTwoDecimalAndSymbolFormat('c'),
                        Tendencia = tendenciaNof.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior = nofMediasAnterior.ToTwoDecimalAndSymbolFormat('c'),
                        TendenciaAnterior = tendenciaAnteriorNof.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior2 = nofMediasHaceDosAnyos.ToTwoDecimalAndSymbolFormat('c'),
                        Divisa = string.Empty
                    },
                    new TotalPrevisionDemandasStringDto
                    {
                        Concepto = "FONDO MANIOBRA",
                        TotalActual = totalActualFondoManiobra.ToTwoDecimalAndSymbolFormat('c'),
                        Tendencia = tendenciaFondoManiobra.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior = totalAnteriorFondoManiobra.ToTwoDecimalAndSymbolFormat('c'),
                        TendenciaAnterior = tendenciaAnteriorFondoManiobra.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior2 = totalAnterior2FondoManiobra.ToTwoDecimalAndSymbolFormat('c'),
                        Divisa = string.Empty
                    },
                    new TotalPrevisionDemandasStringDto
                    {
                        Concepto = "NRN",
                        TotalActual = NRNMediasActual.ToTwoDecimalAndSymbolFormat('c'),
                        Tendencia = tendenciaNRN.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior = NRNMediasAnterior.ToTwoDecimalAndSymbolFormat('c'),
                        TendenciaAnterior = tendenciaAnteriorNRN.ToTwoDecimalAndSymbolFormat('p'),
                        TotalAnterior2 = NRNMediasAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                        Divisa = string.Empty
                    }
                };

                var actual = documentoAnyoActual is null ? Models.Constants.SinDatos : documentoAnyoActual?.Fecha.ToString("dd-MM-yyyy");
                var anterior = documentoAnyoAnterior is null ? Models.Constants.SinDatos : documentoAnyoAnterior?.Fecha.ToString("dd-MM-yyyy");
                var haceDosAnyos = documentoHaceDosAnyos is null ? Models.Constants.SinDatos : documentoHaceDosAnyos?.Fecha.ToString("dd-MM-yyyy");

                var columnas = $"{actual},Tendencia,{anterior},Tendencia,{haceDosAnyos}";

                var nofMedias = new PrevisionDemandasResponse { Columnas = columnas, Listado = listado };

                return result.Ok(nofMedias);
            }

            return result.NotFound();
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener NOF medias para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
