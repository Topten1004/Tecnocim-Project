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

public class GetPrevisionDemandasByEmpresaIdQueryHandler : IRequestHandler<GetPrevisionDemandasByEmpresaIdQuery,
    GenericResult<PrevisionDemandasResponse>>
{
    private readonly ILogger<GetPrevisionDemandasByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetPrevisionDemandasByEmpresaIdQueryHandler(
        ILogger<GetPrevisionDemandasByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<GenericResult<PrevisionDemandasResponse>> Handle(GetPrevisionDemandasByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<PrevisionDemandasResponse>();
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

                var magnitudExistencias = documentoAnyoActual?.Contabilidades?.FirstOrDefault(a => a.Concepto == "existencias")?.Magnitud ?? 0;
                var magnitudDeudoresComerciales = documentoAnyoActual?.Contabilidades?.FirstOrDefault(a => a.Concepto == "deudores comerciales y otras cuentas a cobrar")?.Magnitud ?? 0;
                var magnitudEfectivo = documentoAnyoActual?.Contabilidades?.FirstOrDefault(a => a.Concepto == "efectivo y otros activos líquidos equivalentes")?.Magnitud ?? 0;
                var magnitudAcreedores = documentoAnyoActual?.Contabilidades?.FirstOrDefault(a => a.Concepto == "acreedores y otras cuentas comerciales a pagar")?.Magnitud ?? 0;
                var totalActual = magnitudExistencias + magnitudDeudoresComerciales + magnitudEfectivo - magnitudAcreedores;

                var magnitudExistenciasAnyoAnterior = documentoAnyoAnterior?.Contabilidades?.FirstOrDefault(a => a.Concepto == "existencias")?.Magnitud ?? 0;
                var magnitudDeudoresComercialesAnyoAnterior = documentoAnyoAnterior?.Contabilidades?.FirstOrDefault(a => a.Concepto == "deudores comerciales y otras cuentas a cobrar")?.Magnitud ?? 0;
                var magnitudEfectivoAnyoAnterior = documentoAnyoAnterior?.Contabilidades?.FirstOrDefault(a => a.Concepto == "efectivo y otros activos líquidos equivalentes")?.Magnitud ?? 0;
                var magnitudAcreedoresAnyoAnterior = documentoAnyoAnterior?.Contabilidades?.FirstOrDefault(a => a.Concepto == "acreedores y otras cuentas comerciales a pagar")?.Magnitud ?? 0;
                var totalAnterior = magnitudExistenciasAnyoAnterior + magnitudDeudoresComercialesAnyoAnterior
                    + magnitudEfectivoAnyoAnterior - magnitudAcreedoresAnyoAnterior;

                var magnitudExistenciasHaceDosAnyos = documentoHaceDosAnyos?.Contabilidades?.FirstOrDefault(a => a.Concepto == "existencias")?.Magnitud ?? 0;
                var magnitudDeudoresComercialesHaceDosAnyos = documentoHaceDosAnyos?.Contabilidades?.FirstOrDefault(a => a.Concepto == "deudores comerciales y otras cuentas a cobrar")?.Magnitud ?? 0;
                var magnitudEfectivoHaceDosAnyos = documentoHaceDosAnyos?.Contabilidades?.FirstOrDefault(a => a.Concepto == "efectivo y otros activos líquidos equivalentes")?.Magnitud ?? 0;
                var magnitudAcreedoresHaceDosAnyos = documentoHaceDosAnyos?.Contabilidades?.FirstOrDefault(a => a.Concepto == "acreedores y otras cuentas comerciales a pagar")?.Magnitud ?? 0;
                var totalAnterior2 = magnitudExistenciasHaceDosAnyos + magnitudDeudoresComercialesHaceDosAnyos
                    + magnitudEfectivoHaceDosAnyos - magnitudAcreedoresHaceDosAnyos;

                var tendencia = totalAnterior != 0 ? (totalActual / totalAnterior) - 1 : 0;
                var tendenciaAnterior = totalAnterior2 != 0 ? (totalAnterior / totalAnterior2) - 1 : 0;

                var listado = new List<TotalPrevisionDemandasStringDto>();

                listado.Add(new TotalPrevisionDemandasStringDto
                {
                    Concepto = "NOF",
                    TotalActual = totalActual.ToTwoDecimalAndSymbolFormat('c'),
                    Tendencia = tendencia.ToTwoDecimalAndSymbolFormat('p'),
                    TotalAnterior = totalAnterior.ToTwoDecimalAndSymbolFormat('c'),
                    TendenciaAnterior = tendenciaAnterior.ToTwoDecimalAndSymbolFormat('p'),
                    TotalAnterior2 = totalAnterior2.ToTwoDecimalAndSymbolFormat('c'),
                    Divisa = string.Empty
                });


                documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue
                && x.EmpresaId == request.EmpresaId, null, x => x.Include(e => e.Ratios));

                documentoAnyoActual = documentos.Where(x => x.Fecha.Year == DateTime.UtcNow.Year)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                documentoAnyoAnterior = documentos.Where(x => x.Fecha.Year == DateTime.UtcNow.Year - 1)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();
                documentoHaceDosAnyos = documentos.Where(x => x.Fecha.Year == DateTime.UtcNow.Year - 2)?.OrderByDescending(x => x.Fecha)?.FirstOrDefault();

                var totalActualFondoManiobra = documentoAnyoActual?.Ratios?.FirstOrDefault(r => r.Concepto == "fondo maniobra")?.Magnitud ?? 0;
                var totalAnteriorFondoManiobra = documentoAnyoAnterior?.Ratios?.FirstOrDefault(r => r.Concepto == "fondo maniobra")?.Magnitud ?? 0;
                var totalAnterior2FondoManiobra = documentoHaceDosAnyos?.Ratios?.FirstOrDefault(r => r.Concepto == "fondo maniobra")?.Magnitud ?? 0;
                var tendenciaFondoManiobra = totalAnteriorFondoManiobra != 0 ? (totalActualFondoManiobra / totalAnteriorFondoManiobra) - 1 : 0;
                var tendenciaAnteriorFondoManiobra = totalAnterior2FondoManiobra != 0 ? (totalAnteriorFondoManiobra / totalAnterior2FondoManiobra) - 1 : 0;

                listado.Add(new TotalPrevisionDemandasStringDto
                {
                    Concepto = "FONDO MANIOBRA",
                    TotalActual = totalActualFondoManiobra.ToTwoDecimalAndSymbolFormat('c'),
                    Tendencia = tendenciaFondoManiobra.ToTwoDecimalAndSymbolFormat('p'),
                    TotalAnterior = totalAnteriorFondoManiobra.ToTwoDecimalAndSymbolFormat('c'),
                    TendenciaAnterior = tendenciaAnteriorFondoManiobra.ToTwoDecimalAndSymbolFormat('p'),
                    TotalAnterior2 = totalAnterior2FondoManiobra.ToTwoDecimalAndSymbolFormat('c'),
                    Divisa = string.Empty
                });

                var totalActualNRN = totalActual - totalActualFondoManiobra;
                var totalAnteriorNRN = totalAnterior - totalAnteriorFondoManiobra;
                var totalAnterior2NRN = totalAnterior2 - totalAnterior2FondoManiobra;

                listado.Add(new TotalPrevisionDemandasStringDto
                {
                    Concepto = "NRN",
                    TotalActual = totalActualNRN.ToTwoDecimalAndSymbolFormat('c'),
                    Tendencia = (totalAnteriorNRN != 0 ? (totalActualNRN / totalAnteriorNRN) - 1 : 0).ToTwoDecimalAndSymbolFormat('p'),
                    TotalAnterior = totalAnteriorNRN.ToTwoDecimalAndSymbolFormat('c'),
                    TendenciaAnterior = (totalAnterior2NRN != 0 ? (totalAnteriorNRN / totalAnterior2NRN) - 1 : 0).ToTwoDecimalAndSymbolFormat('p'),
                    TotalAnterior2 = totalAnterior2NRN.ToTwoDecimalAndSymbolFormat('c'),
                    Divisa = string.Empty
                });

                var actual = documentoAnyoActual is null ? Models.Constants.SinDatos : documentoAnyoActual?.Fecha.ToString("dd-MM-yyyy");
                var anterior = documentoAnyoAnterior is null ? Models.Constants.SinDatos : documentoAnyoAnterior?.Fecha.ToString("dd-MM-yyyy");
                var haceDosAnyos = documentoHaceDosAnyos is null ? Models.Constants.SinDatos : documentoHaceDosAnyos?.Fecha.ToString("dd-MM-yyyy");

                var columnas = $"{actual},Tendencia,{anterior},Tendencia,{haceDosAnyos}";

                var previsionDemandas = new PrevisionDemandasResponse { Columnas = columnas, Listado = listado };

                return result.Ok(previsionDemandas);
            }

            return result.NotFound();
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener la previsión de demandas para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
