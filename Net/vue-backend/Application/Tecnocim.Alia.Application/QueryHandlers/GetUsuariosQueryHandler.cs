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

public class GetUsuariosQueryHandler : IRequestHandler<GetUsuariosQuery, GenericResult<IEnumerable<UsuarioListadoDto>>>
{
    private readonly ILogger<GetUsuariosQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetUsuariosQueryHandler(
        ILogger<GetUsuariosQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<IEnumerable<UsuarioListadoDto>>> Handle(GetUsuariosQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<IEnumerable<UsuarioListadoDto>>() { Result = Enumerable.Empty<UsuarioListadoDto>() };

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var usuarios = await unitOfWork.UsuarioRepository.GetIncludeAsync(x => x, x => !x.Deleted.HasValue, x => x.OrderBy(y => y.Apellidos),
                x => x.Include(y => y.Rol).Include(w => w.Empresas));

            if (usuarios is not null && usuarios.Any())
            {
                var usuariosListadoDtos = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioListadoDto>>(usuarios);
                return result.Ok(usuariosListadoDtos);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("Error al obtener todos los usuarios", exception);
            return result.Failed(500, "Error al obtener todos los usuarios.");
        }

        return result;
    }
}
