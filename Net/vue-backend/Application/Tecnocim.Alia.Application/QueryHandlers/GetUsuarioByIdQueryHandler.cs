using System.Reflection.Metadata.Ecma335;
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

public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, GenericResult<UsuarioListadoDto>>
{
    private readonly ILogger<GetUsuarioByIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetUsuarioByIdQueryHandler(
        ILogger<GetUsuarioByIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<UsuarioListadoDto>> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<UsuarioListadoDto>();

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var usuarios = await unitOfWork.UsuarioRepository.GetIncludeAsync(x => x, x => x.UsuarioId == request.UsuarioId && !x.Deleted.HasValue, null,
                x => x.Include(y => y.Rol).Include(w => w.Empresas));

            if (usuarios is not null && usuarios.Any())
            {
                var usuarioListadoDto = _mapper.Map<Usuario, UsuarioListadoDto>(usuarios.First());
                return result.Ok(usuarioListadoDto);
            }

            return result.NotFound();
        }
        catch (Exception exception)
        {
            _logger.LogError("Error al obtener el usuario", exception);
            return result.Failed(500, "Error al obtener el usuario.");
        }
    }
}
