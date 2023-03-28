using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetPoolBancarioByEmpresaIdAndSeccionQueryHandler : IRequestHandler<GetPoolBancarioByEmpresaIdAndSeccionQuery, GenericResult<PoolBancarioResponse>>
{
    private readonly ILogger<GetPoolBancarioByEmpresaIdAndSeccionQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetPoolBancarioByEmpresaIdAndSeccionQueryHandler(
        ILogger<GetPoolBancarioByEmpresaIdAndSeccionQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<PoolBancarioResponse>> Handle(GetPoolBancarioByEmpresaIdAndSeccionQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<PoolBancarioResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var contratos = await unitOfWork.ContratoRepository.GetPools(request.EmpresaId, request.Seccion);

            if (contratos is { } && contratos.Any())
            {
                var poolBancarioDtos = _mapper.Map<IEnumerable<Contrato>, IEnumerable<PoolBancarioDto>>(contratos);
                return result.Ok(new PoolBancarioResponse { PoolBancarioList = poolBancarioDtos });
            }

            return result.NotFound();

        }
        catch (Exception exception)
        {
            var message = $"Error al obtener el pool bancario para la empresa con id: {request.EmpresaId} y sección: {request.Seccion}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
