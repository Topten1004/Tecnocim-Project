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

public class GetStatusWebEmpresaByIdQueryHandler : IRequestHandler<GetStatusWebEmpresaByIdQuery, GenericResult<int>>
{
    private readonly ILogger<GetStatusWebEmpresaByIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetStatusWebEmpresaByIdQueryHandler(
        ILogger<GetStatusWebEmpresaByIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<int>> Handle(GetStatusWebEmpresaByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<int>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            if (request.Usuario is not Usuario usuario)
            {
                return result.Failed(400, "Usuario no válido");
            }

            var usuarioExistente = await unitOfWork.UsuarioRepository.GetFirstAsync(x => !x.Deleted.HasValue && x.UsuarioId == usuario.UsuarioId
            && x.Empresas.Any(t => t.EmpresaId == request.Id), null, x => x.Empresas);

            if (usuarioExistente is null)
            {
                return result.Failed(400, "El usuario no tiene asignada la empresa a la que pertenece el documento");
            }

            var documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => x.EmpresaId == request.Id && !x.Deleted.HasValue, null,
                   x => x.Include(y => y.Empresa), false);

            if (documentos is not null && documentos.Any())
            {
                var resultCode = 0;
                var pools = await unitOfWork.PoolRepository.GetByEmpresaId(request.Id);
                if (pools is not null)
                {
                    resultCode = !pools.Any() ? 1 : 2;
                }

                return result.Ok(resultCode);
            }

            return result.Ok(0);
        }
        catch (Exception exception)
        {
            var message = $"Error al obtener el status web para la empresa con id: {request.Id}";
            _logger.LogError(message, exception);
            return result.Failed(500, message);
        }
    }
}
