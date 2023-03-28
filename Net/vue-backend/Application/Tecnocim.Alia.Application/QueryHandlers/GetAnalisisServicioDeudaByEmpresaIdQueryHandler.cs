using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetAnalisisServicioDeudaByEmpresaIdQueryHandler : IRequestHandler<GetAnalisisServicioDeudaByEmpresaIdQuery, GenericResult<AnalisisServicioDeudaResponse>>
{
    private readonly ILogger<GetAnalisisServicioDeudaByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetAnalisisServicioDeudaByEmpresaIdQueryHandler(
        ILogger<GetAnalisisServicioDeudaByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<AnalisisServicioDeudaResponse>> Handle(GetAnalisisServicioDeudaByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<AnalisisServicioDeudaResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var now = DateTime.UtcNow;
            var nowDateOnly = new DateOnly(now.Year, now.Month, now.Day);

            var pools = await unitOfWork.PoolRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue
              && x.Cuenta.StartsWith("17") && x.Documento != null && x.Documento.EmpresaId == request.EmpresaId,
              null, x => x.Include(p => p.Documento));

            if (pools is null || !pools.Any())
            {
                return result.NotFound();
            }

            var poolIds = pools.Select(x => x.PoolId).ToList();

            var contratos = await unitOfWork.ContratoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue && x.Vencimiento > nowDateOnly,
               null, x => x.Include(t => t.Cuotas).Include(t => t.Pools).Include(t => t.EquivalenciasEntidad).Include(t => t.EquivalenciasMoneda)
               .Include(t => t.Pools).ThenInclude(f => f.Documento));

            if (contratos is { } && contratos.Any())
            {
                contratos = contratos.Where(c => c.Pools.Any(x => !x.Deleted.HasValue && x.Documento != null && x.Documento.EmpresaId == request.EmpresaId)).ToList();

                var analisisServicioDeuda = contratos.Select(c =>
                            new AnalisisServicioDeuda
                            {
                                ContratoId = c.ContratoId,
                                Entidad = c.EquivalenciasEntidad.Nombre,
                                Limite = decimal.Round(c.Limite, 2, MidpointRounding.AwayFromZero),
                                Inicio = c.Inicio.ToString("yyyy-MM-dd"),
                                Vencimiento = c.Vencimiento.ToString("yyyy-MM-dd"),
                                Divisa = c.EquivalenciasMoneda?.Tipo ?? string.Empty,
                                Cuotas = c.Cuotas.Select(cuota => new CuotaDto { Fecha = cuota.Fecha.ToString("yyyy-MM"), Importe = decimal.Round(cuota.Importe, 2, MidpointRounding.AwayFromZero) })
                            });


                return result.Ok(new AnalisisServicioDeudaResponse { AnalisisServicioDeudas = analisisServicioDeuda });
            }

            return result.NotFound();
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener el análisis del servicio deuda para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
