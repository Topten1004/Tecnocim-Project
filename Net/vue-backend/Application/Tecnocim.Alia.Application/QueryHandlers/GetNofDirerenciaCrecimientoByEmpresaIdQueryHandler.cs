using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Extensions;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetNofDirerenciaCrecimientoByEmpresaIdQueryHandler : IRequestHandler<GetNofDirerenciaCrecimientoByEmpresaIdQuery,
    GenericResult<NofDirerenciaCrecimientoResponse>>
{
    private readonly ILogger<GetNofDirerenciaCrecimientoByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetNofDirerenciaCrecimientoByEmpresaIdQueryHandler(
        ILogger<GetNofDirerenciaCrecimientoByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<GenericResult<NofDirerenciaCrecimientoResponse>> Handle(GetNofDirerenciaCrecimientoByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<NofDirerenciaCrecimientoResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue
                && x.EmpresaId == request.EmpresaId, null, x => x.Include(e => e.Contabilidades));

            if (documentos is { } && documentos.Any())
            {
                var documentoAnyoActual = documentos.Where(x => x.Fecha.Year == DateTime.UtcNow.Year)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                var documentoAnyoAnterior = documentos.Where(x => x.Fecha.Year == DateTime.UtcNow.Year - 1)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                var documentoHaceDosAnyos = documentos.Where(x => x.Fecha.Year == DateTime.UtcNow.Year - 2)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();

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

                // con Ratios

                documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue
                    && x.EmpresaId == request.EmpresaId, null, x => x.Include(e => e.Ratios));

                documentoAnyoActual = documentos.Where(x => x.Fecha.Year == DateTime.UtcNow.Year)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                documentoAnyoAnterior = documentos.Where(x => x.Fecha.Year == DateTime.UtcNow.Year - 1)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();

                var totalActualFondoManiobra = documentoAnyoActual?.Ratios?.FirstOrDefault(r => r.Concepto == "fondo maniobra")?.Magnitud ?? 0;
                var totalAnteriorFondoManiobra = documentoAnyoAnterior?.Ratios?.FirstOrDefault(r => r.Concepto == "fondo maniobra")?.Magnitud ?? 0;

                var NRNMediasActual = nofMediasActual - totalActualFondoManiobra;
                var NRNMediasAnterior = nofMediasAnterior - totalAnteriorFondoManiobra;

                var crecimiento = NRNMediasActual / NRNMediasAnterior;
                var porcentaje = request.Incremento != 0 ? (crecimiento - request.Incremento) / request.Incremento : 0;

                var response = new NofDirerenciaCrecimientoResponse
                {
                    Label = "Diferencia de crecimiento entre las ventas y las NRN",
                    DiferenciaCrecimiento = porcentaje.ToTwoDecimalAndSymbolFormat('p')
                };

                return result.Ok(response);
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
