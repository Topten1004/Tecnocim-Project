using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Queries;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.QueryHandlers;

public class GetFicheroByIdQueryHandler : IRequestHandler<GetFicheroByIdQuery, GenericResult<UploadFicheroResponse>>
{
    private readonly ILogger<GetFicheroByIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetFicheroByIdQueryHandler(
        ILogger<GetFicheroByIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<UploadFicheroResponse>> Handle(GetFicheroByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<UploadFicheroResponse>();

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var ficheros = await unitOfWork.FicheroRepository.GetIncludeAsync(x => x, x => x.FicheroId == request.Id && !x.Deleted.HasValue, null,
                x => x.Include(y => y.Empresa), false);

            if (ficheros is not null && ficheros.Any())
            {
                if (request.Usuario is not Usuario usuario)
                {
                    return result.Failed(401, "El usuario no tiene asignada la empresa a la que pertenece el fichero");
                }

                var usuarioExistente = await unitOfWork.UsuarioRepository.GetFirstAsync(x => !x.Deleted.HasValue && x.UsuarioId == usuario.UsuarioId
                && x.Empresas.Any(t => t.EmpresaId == ficheros.First().EmpresaId), null, x => x.Empresas);

                if (usuarioExistente is null)
                {
                    return result.Failed(401, "El usuario no tiene asignada la empresa a la que pertenece el fichero");
                }

                var ficheroResponse = _mapper.Map<Domain.Fichero, UploadFicheroResponse>(ficheros.First());
                return result.Ok(ficheroResponse);
            }
        }
        catch(Exception exception)
        {
            _logger.LogError(exception, "Error GetFicheroById");
            return result.Failed(500, exception.Message);
        }

        return result.NotFound();
    }
}
