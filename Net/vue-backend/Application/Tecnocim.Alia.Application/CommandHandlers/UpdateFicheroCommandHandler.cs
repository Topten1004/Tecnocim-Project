using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.CommandHandlers
{
    public class UpdateFicheroCommandHandler : IRequestHandler<UpdateFicheroCommand, GenericResult<UploadFicheroResponse>>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFicheroCommandHandler> _logger;

        public UpdateFicheroCommandHandler(
           IServiceProvider serviceProvider,
            IMapper mapper,
            ILogger<UpdateFicheroCommandHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericResult<UploadFicheroResponse>> Handle(UpdateFicheroCommand request, CancellationToken cancellationToken)
        {
            var result = new GenericResult<UploadFicheroResponse>();
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

                var ficheros = await unitOfWork.FicheroRepository.GetIncludeAsync(x => x, x => x.FicheroId == request.Fichero.Id && !x.Deleted.HasValue, null,
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

                    var fichero = ficheros.First();

                    fichero.Updated = DateTime.UtcNow;

                    await Task.Run(() =>
                    {
                        unitOfWork.FicheroRepository.Update(fichero);
                        unitOfWork.Commit();
                    }, cancellationToken);

                    var ficheroResponse = _mapper.Map<Fichero, UploadFicheroResponse>(fichero);

                    return result.Ok(ficheroResponse);
                }

                return result.Failed(404, "No se ha encontrado el fichero a actualizar");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error al actualizar el fichero");
                return result.Failed(500, $"Error al actualizar la empresa: {Environment.NewLine}{exception.Message}");
            }
        }
    }
}
