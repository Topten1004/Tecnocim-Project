using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetUsuariosByEmpresaIdQueryHandler : IRequestHandler<GetUsuariosByEmpresaIdQuery, IEnumerable<UsuarioListadoDto>>
{
    private readonly ILogger<GetUsuariosByEmpresaIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetUsuariosByEmpresaIdQueryHandler(
        ILogger<GetUsuariosByEmpresaIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UsuarioListadoDto>> Handle(GetUsuariosByEmpresaIdQuery request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

        var usuarios = await unitOfWork.EmpresaRepository.GetUsuariosByEmpresaId(request.EmpresaId);

        if (usuarios is not null && usuarios.Any())
        {
            return _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioListadoDto>>(usuarios);
        }

        return Enumerable.Empty<UsuarioListadoDto>();
    }
}
