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

public class GetPoolBancarioTipo52ByEmpresaIdQueryHandler : IRequestHandler<GetPoolBancarioTipo52ByEmpresaIdQuery, GenericResult<PoolDtoResponse>>
{
    private readonly ILogger<GetPoolBancarioTipo52ByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetPoolBancarioTipo52ByEmpresaIdQueryHandler(
        ILogger<GetPoolBancarioTipo52ByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<PoolDtoResponse>> Handle(GetPoolBancarioTipo52ByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<PoolDtoResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var pools = await unitOfWork.PoolRepository.GetTipo52ByEmpresaId(request.EmpresaId);

            if (pools is { } && pools.Any())
            {
                var poolDtos = _mapper.Map<IEnumerable<Pool>, IEnumerable<PoolDto>>(pools);
                return result.Ok(new PoolDtoResponse { PoolDtoList = poolDtos });
            }

            return result.NotFound();
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener el pool bancario tipo 52 para la empresa con id: {request.EmpresaId}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
