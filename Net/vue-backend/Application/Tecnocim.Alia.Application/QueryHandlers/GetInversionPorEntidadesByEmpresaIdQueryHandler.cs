using System.Globalization;
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

public class GetInversionPorEntidadesByEmpresaIdQueryHandler : IRequestHandler<GetInversionPorEntidadesByEmpresaIdQuery, GenericResult<InversionEntidadesResponse>>
{
    private readonly ILogger<GetInversionPorEntidadesByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetInversionPorEntidadesByEmpresaIdQueryHandler(
        ILogger<GetInversionPorEntidadesByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<InversionEntidadesResponse>> Handle(GetInversionPorEntidadesByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<InversionEntidadesResponse>();
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
                null, x => x.Include(t => t.EquivalenciasEntidad).Include(t => t.EquivalenciasMoneda).Include(t => t.Pools).ThenInclude(e => e.Documento));

            if (contratos is { } && contratos.Any())
            {
                contratos = contratos.Where(c => c.Pools.Any(x => !x.Deleted.HasValue && x.Documento != null && x.Documento.EmpresaId == request.EmpresaId)).ToList();

                foreach (var contrato in contratos)
                {
                    contrato.Pools = contrato.Pools.Where(p => !p.Deleted.HasValue && poolIds.Contains(p.PoolId) && p.ContratoId == contrato.ContratoId).ToList();
                }

                var total = contratos.Sum(w => w.Limite);
                var inversionEntidadDto = contratos.GroupBy(x => x.EquivalenciasEntidad.Id, (key, g) =>
                {
                    var limiteGrupo = g.Sum(p => p.Limite);
                    return new InversionEntidadDto
                    {
                        NombreEntidad = g.FirstOrDefault(t => t.EquivalenciasEntidadId == key)?.EquivalenciasEntidad?.Nombre!,
                        Total = limiteGrupo.ToTwoDecimalAndSymbolFormat('c'),
                        Porcentaje = (limiteGrupo * 100/ total).ToTwoDecimalAndSymbolFormat('p'),
                        Divisa = g.FirstOrDefault(t => t.EquivalenciasEntidadId == key)?.EquivalenciasMoneda?.Tipo ?? string.Empty
                    };
                });

                return result.Ok(new InversionEntidadesResponse { InversionEntidadesDto = inversionEntidadDto });
            }

            return result.NotFound();
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener la inversion por entidades para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
