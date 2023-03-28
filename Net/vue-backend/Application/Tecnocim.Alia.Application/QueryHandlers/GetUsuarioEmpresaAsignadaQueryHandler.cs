using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetUsuarioEmpresaAsignadaQueryHandler : IRequestHandler<GetUsuarioEmpresaAsignadaQuery, bool>
{
    private readonly ILogger<GetUsuarioEmpresaAsignadaQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetUsuarioEmpresaAsignadaQueryHandler(
        ILogger<GetUsuarioEmpresaAsignadaQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<bool> Handle(GetUsuarioEmpresaAsignadaQuery request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();


        if (request.Usuario is not Usuario usuario)
        {
            return false;
        }

        var usuarioExistente = await unitOfWork.UsuarioRepository.GetFirstAsync(x => !x.Deleted.HasValue && x.UsuarioId == usuario.UsuarioId
        && x.Empresas.Any(t => t.EmpresaId == request.EmpresaId), null, x => x.Empresas);

        return usuarioExistente is not null;
    }
}
