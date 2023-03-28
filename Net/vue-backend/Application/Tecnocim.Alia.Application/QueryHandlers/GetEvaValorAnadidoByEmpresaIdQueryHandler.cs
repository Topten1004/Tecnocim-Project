using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Extensions;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetEvaValorAnadidoByEmpresaIdQueryHandler : IRequestHandler<GetEvaValorAnadidoByEmpresaIdQuery, GenericResult<EvaValorAnadidoResponse>>
{
    private readonly ILogger<GetEvaValorAnadidoByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetEvaValorAnadidoByEmpresaIdQueryHandler(
        ILogger<GetEvaValorAnadidoByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<EvaValorAnadidoResponse>> Handle(GetEvaValorAnadidoByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<EvaValorAnadidoResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var conceptosAnaliticas = new List<string> { "consumo", "exteriores", "tributos", "personal", "otros gastos de gestión", "financieros", "extraordinarios", "otros ingresos" };
            var now = DateTime.UtcNow;
            var nowDateOnly = new DateOnly(now.Year, now.Month, now.Day);

            var pools = await unitOfWork.PoolRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue
              && x.Documento != null && x.Documento.EmpresaId == request.EmpresaId, null, x => x.Include(p => p.Documento));

            if (pools is null || !pools.Any())
            {
                return result.NotFound();
            }

            var poolIds = pools.Select(x => x.PoolId).ToList();

            var contratos = await unitOfWork.ContratoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue && x.Vencimiento > nowDateOnly,
                null, x => x.Include(t => t.Pools).ThenInclude(e => e.Documento).ThenInclude(e => e.Analiticas));

            if (contratos is { } && contratos.Any())
            {
                contratos = contratos.Where(c => c.Pools.Any(x => !x.Deleted.HasValue)).ToList();

                foreach (var ctr in contratos)
                {
                    ctr.Pools = ctr.Pools.Where(p => !p.Deleted.HasValue && poolIds.Contains(p.PoolId) && p.ContratoId == ctr.ContratoId).ToList();
                }

                var contrato = contratos.OrderByDescending(x => x.Pools?.FirstOrDefault()?.Documento.Fecha).FirstOrDefault();

                var analiticaTotalVentas = contrato?.Pools?.FirstOrDefault()?.Documento?.Analiticas?.FirstOrDefault(a => a.Cuenta == "total ventas");

                var list = new List<ValorAnadidoDto>();

                foreach (var concepto in conceptosAnaliticas)
                {
                    var analitica = contrato?.Pools?.FirstOrDefault()?.Documento?.Analiticas?.FirstOrDefault(a => a.Cuenta == concepto);

                    var margenBrutoVentas = GetMargenBrutoVentas(concepto, analitica, contrato?.Pools?.FirstOrDefault()?.Documento?.Analiticas);

                    var margenBruto = analiticaTotalVentas?.Magnitud != null ? (margenBrutoVentas / analiticaTotalVentas.Magnitud.Value) : 0;

                    list.Add(new ValorAnadidoDto
                    {
                        Concepto = concepto,
                        Magnitud = (analitica?.Magnitud ?? 0).ToTwoDecimalAndSymbolFormat('c'),
                        MargenBrutoVentas = margenBrutoVentas.ToTwoDecimalAndSymbolFormat('c'),
                        MargenBruto = margenBruto.ToTwoDecimalAndSymbolFormat('p'),
                        Divisa = string.Empty
                    });
                }

                var analiticaBeneficios = contrato?.Pools?.FirstOrDefault()?.Documento?.Analiticas?.FirstOrDefault(a => a.Cuenta == "beneficios");
                var analiticaOtrosIngresosTotalVentas = contrato?.Pools?.FirstOrDefault()?.Documento?.Analiticas?.FirstOrDefault(a => a.Cuenta == "otros ingresos");
                var margenBrutoOtrosIngresosVentas = GetMargenBrutoVentas("otros ingresos", analiticaBeneficios, contrato?.Pools?.FirstOrDefault()?.Documento?.Analiticas);

                list.Add(new ValorAnadidoDto
                {
                    Concepto = "beneficios",
                    Magnitud = (analiticaBeneficios?.Magnitud ?? 0).ToTwoDecimalAndSymbolFormat('c'),
                    MargenBrutoVentas = margenBrutoOtrosIngresosVentas.ToTwoDecimalAndSymbolFormat('c'),
                    MargenBruto = (analiticaOtrosIngresosTotalVentas?.Magnitud != null 
                    ? (margenBrutoOtrosIngresosVentas / analiticaOtrosIngresosTotalVentas.Magnitud.Value) : 0).ToTwoDecimalAndSymbolFormat('p'),
                    Divisa = string.Empty
                });

                var value = new EvaValorAnadidoDto { List = list };

                return result.Ok(new EvaValorAnadidoResponse { Value = value });
            }

            return result.NotFound();
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener valor añadido para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }

    private static decimal GetMargenBrutoVentas(string concepto, Analitica? analitica, ICollection<Analitica>? analiticas)
    {
        if (string.IsNullOrEmpty(concepto) || analitica is null || analiticas is null)
        {
            return 0m;
        }

        Analitica analiticaRelated;

        switch (concepto)
        {
            case "consumo":
                analiticaRelated = analiticas.FirstOrDefault(x => x.Cuenta == "total ingresos");
                return analiticaRelated?.Magnitud ?? 0 - analitica.Magnitud ?? 0;

            case "exteriores":
                analiticaRelated = analiticas.FirstOrDefault(x => x.Cuenta == "consumo");
                return analiticaRelated?.Magnitud ?? 0 - analitica.Magnitud ?? 0;

            case "tributos":
                analiticaRelated = analiticas.FirstOrDefault(x => x.Cuenta == "exteriores");
                return analiticaRelated?.Magnitud ?? 0 - analitica.Magnitud ?? 0;

            case "personal":
                analiticaRelated = analiticas.FirstOrDefault(x => x.Cuenta == "tributos");
                return analiticaRelated?.Magnitud ?? 0 - analitica.Magnitud ?? 0;

            case "otros gastos de gestión":
                analiticaRelated = analiticas.FirstOrDefault(x => x.Cuenta == "personal");
                return analiticaRelated?.Magnitud ?? 0 - analitica.Magnitud ?? 0;

            case "financieros":
                analiticaRelated = analiticas.FirstOrDefault(x => x.Cuenta == "otros gastos de gestión");
                return analiticaRelated?.Magnitud ?? 0 - analitica.Magnitud ?? 0;

            case "extraordinarios":
                analiticaRelated = analiticas.FirstOrDefault(x => x.Cuenta == "financieros");
                return analiticaRelated?.Magnitud ?? 0 - analitica.Magnitud ?? 0;

            case "otros ingresos":
                analiticaRelated = analiticas.FirstOrDefault(x => x.Cuenta == "extraordinarios");
                return analiticaRelated?.Magnitud ?? 0 - analitica.Magnitud ?? 0;

            default:
                return 0m;
        }
    }
}
