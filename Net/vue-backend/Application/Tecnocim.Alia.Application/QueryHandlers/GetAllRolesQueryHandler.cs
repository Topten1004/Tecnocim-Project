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

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, GenericResult<IEnumerable<RolDto>>>
{
    private readonly ILogger<GetAllRolesQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetAllRolesQueryHandler(
        ILogger<GetAllRolesQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<IEnumerable<RolDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<IEnumerable<RolDto>>() { Result = Enumerable.Empty<RolDto>() };

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var roles = await unitOfWork.RolRepository.GetAsync();

            if (roles is not null && roles.Any())
            {
                var rolesDtos = _mapper.Map<IEnumerable<Rol>, IEnumerable<RolDto>>(roles);
                return result.Ok(rolesDtos);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("Error al obtener todos los roles", exception);
            return result.Failed(500, "Error al obtener todos los roles.");
        }

        return result;
    }
}
