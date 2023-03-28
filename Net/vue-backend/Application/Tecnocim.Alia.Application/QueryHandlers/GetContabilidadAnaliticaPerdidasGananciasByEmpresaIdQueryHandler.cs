using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Extensions;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetContabilidadAnaliticaPerdidasGananciasByEmpresaIdQueryHandler : IRequestHandler<GetContabilidadAnaliticaPerdidasGananciasByEmpresaIdQuery,
    GenericResult<ContabilidadAnaliticaPerdidasGananciasResponse>>
{
    private readonly ILogger<GetContabilidadAnaliticaPerdidasGananciasByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetContabilidadAnaliticaPerdidasGananciasByEmpresaIdQueryHandler(
        ILogger<GetContabilidadAnaliticaPerdidasGananciasByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<ContabilidadAnaliticaPerdidasGananciasResponse>> Handle(GetContabilidadAnaliticaPerdidasGananciasByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<ContabilidadAnaliticaPerdidasGananciasResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var conceptosAnaliticas = new List<string> { "consumo", "exteriores", "tributos", "personal", "otros gastos de gestión", "financieros", "extraordinarios", "amortizaciones" };
            var now = DateTime.UtcNow;
            var nowDateOnly = new DateOnly(now.Year, now.Month, now.Day);

            var documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue && x.EmpresaId == request.EmpresaId,
                            null, x => x.Include(a => a.Analiticas));

            if (documentos is null || !documentos.Any())
            {
                return result.NotFound();
            }

            var documentoConAnaliticas = documentos.OrderByDescending(t => t.Fecha).FirstOrDefault();

            var contratos = await unitOfWork.ContratoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue && x.Vencimiento > nowDateOnly
                    && x.Pools.Any(p => p.DocumentoId == documentoConAnaliticas!.DocumentoId), null,
                    x => x.Include(t => t.EquivalenciasMoneda).Include(t => t.Pools));
            var contrato = contratos.FirstOrDefault();

            var analiticas = documentoConAnaliticas?.Analiticas;

            var analiticaTotalGastos = analiticas?.FirstOrDefault(a => a.Cuenta == "total gastos");
            var analiticaTotalVentas = analiticas?.FirstOrDefault(a => a.Cuenta == "total ventas");
            var analiticaTotalIngresos = analiticas?.FirstOrDefault(a => a.Cuenta == "total ingresos");
            var analiticaOtrosIngresosGestion = analiticas?.FirstOrDefault(a => a.Cuenta == "otros ingresos de gestión");

            var list = new List<CalculosAnaliticasDto>();

            foreach (var concepto in conceptosAnaliticas)
            {
                var analitica = analiticas?.FirstOrDefault(a => a.Cuenta == concepto);

                var (sobreGastos, sobreVentas, sobreIngresos) = GetSobreValues(analiticaTotalGastos, analiticaTotalVentas, analiticaTotalIngresos, analitica);

                var magnitud = analitica?.Magnitud ?? 0;

                list.Add(new CalculosAnaliticasDto
                {
                    Concepto = concepto,
                    Magnitud = magnitud.ToTwoDecimalAndSymbolFormat('c'),
                    SobreGastos = sobreGastos.ToTwoDecimalAndSymbolFormat('p'),
                    SobreVentas = sobreVentas.ToTwoDecimalAndSymbolFormat('p'),
                    SobreIngresos = sobreIngresos.ToTwoDecimalAndSymbolFormat('p'),
                    Divisa = contrato?.EquivalenciasMoneda.Tipo ?? string.Empty
                });
            }

            var analiticaBeneficios = analiticas?.FirstOrDefault(a => a.Cuenta == "beneficios");

            var (sobreGastosBeneficios, sobreVentasBeneficios, sobreIngresosBeneficios) =
                GetSobreValues(analiticaTotalGastos, analiticaTotalVentas, analiticaTotalIngresos, analiticaBeneficios);

            list.Add(new CalculosAnaliticasDto
            {
                Concepto = "beneficios",
                Magnitud = ((analiticaTotalVentas?.Magnitud ?? 0) + (analiticaOtrosIngresosGestion?.Magnitud ?? 0) - (analiticaTotalGastos?.Magnitud ?? 0)).ToTwoDecimalAndSymbolFormat('c'),
                SobreGastos = sobreGastosBeneficios.ToTwoDecimalAndSymbolFormat('p'),
                SobreVentas = sobreVentasBeneficios.ToTwoDecimalAndSymbolFormat('p'),
                SobreIngresos = sobreIngresosBeneficios.ToTwoDecimalAndSymbolFormat('p'),
                Divisa = contrato?.EquivalenciasMoneda.Tipo ?? string.Empty
            });

            var ingresosGastos = new ContabilidadAnaliticaPerdidasGananciasResponse
            {
                TotalGastos = (analiticaTotalGastos?.Magnitud ?? 0).ToTwoDecimalAndSymbolFormat('c'),
                TotalIngresos = (analiticaTotalIngresos?.Magnitud ?? 0).ToTwoDecimalAndSymbolFormat('c'),
                List = list
            };

            return result.Ok(ingresosGastos);
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener la contabilidad analítica de perdidas y ganancias para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }

    private static (decimal sobreGastos, decimal sobreVentas, decimal sobreIngresos) GetSobreValues(Analitica? analiticaTotalGastos, Analitica? analiticaTotalVentas, Analitica? analiticaTotalIngresos, Analitica? analitica)
    {
        var sobreGastos = 0m;
        if (analiticaTotalGastos is not null && analiticaTotalGastos.Magnitud.HasValue)
        {
            sobreGastos = analiticaTotalGastos.Magnitud.Value != 0 ? (analitica?.Magnitud ?? 0m) / analiticaTotalGastos.Magnitud.Value : 0m;
        }

        var sobreVentas = 0m;
        if (analiticaTotalVentas is not null && analiticaTotalVentas.Magnitud.HasValue)
        {
            sobreVentas = analiticaTotalVentas.Magnitud.Value != 0 ? (analitica?.Magnitud ?? 0m) / analiticaTotalVentas.Magnitud.Value : 0m;
        }

        var sobreIngresos = 0m;
        if (analiticaTotalIngresos is not null && analiticaTotalIngresos.Magnitud.HasValue)
        {
            sobreIngresos = analiticaTotalIngresos.Magnitud.Value != 0 ? (analitica?.Magnitud ?? 0m) / analiticaTotalIngresos.Magnitud.Value : 0m;
        }

        return (sobreGastos, sobreVentas, sobreIngresos);
    }
}
