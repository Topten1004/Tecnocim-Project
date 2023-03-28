using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetContratoByIdQueryHandler : IRequestHandler<GetContratoByIdQuery, GenericResult<ContratoDto>>
{
    private readonly ILogger<GetContratoByIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public GetContratoByIdQueryHandler(
        ILogger<GetContratoByIdQueryHandler> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<GenericResult<ContratoDto>> Handle(GetContratoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<ContratoDto>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var mapper = scope.ServiceProvider.GetService<IMapper>();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var pools = await unitOfWork.PoolRepository
                .GetIncludeAsync(x => x, x => !x.Deleted.HasValue && x.ContratoId == request.ContratoId, null,
                x => x.Include(p => p.Contrato).Include(p => p.Documento).ThenInclude(d => d.Empresa));

            if (pools is null || !pools.Any())
            {
                return result.NotFound();
            }

            var poolIds = pools.Select(c => c.PoolId);

            var documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x,
                x => x.Pools.Any(p => !p.Deleted.HasValue && poolIds.Contains(p.PoolId)));

            var empresaId = documentos?.Select(x => x.EmpresaId).FirstOrDefault();

            if (empresaId.HasValue)
            {
                if (request.Usuario is not Usuario usuario)
                {
                    return result.Failed(401, "El usuario no tiene asignada la empresa a la que corresponde el contrato");
                }

                var usuarioExistente = await unitOfWork.UsuarioRepository.GetFirstAsync(x => !x.Deleted.HasValue && x.UsuarioId == usuario.UsuarioId
                && x.Empresas.Any(t => t.EmpresaId == empresaId.Value), null, x => x.Empresas);

                if (usuarioExistente is null)
                {
                    return result.Failed(401, "El usuario no tiene asignada la empresa a la que corresponde el contrato");
                }

                var contratos = await unitOfWork.ContratoRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue
                    && x.ContratoId == pools.First().ContratoId, null, x => x.Include(c => c.Pools).Include(c => c.EquivalenciasEntidad)
                    .Include(c => c.EquivalenciasProducto).Include(c => c.EquivalenciasMoneda).Include(c => c.EquivalenciasPeriodificacion));

                if (contratos is null || !contratos.Any())
                {
                    return result.NotFound();
                }

                var contratoDto = mapper.Map<Contrato, ContratoDto>(contratos.First());

                return result.Ok(contratoDto);
            }

            return result.NotFound();
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener el contrato con id: {request.ContratoId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
