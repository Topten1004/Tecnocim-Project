using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Application.Services;
using Tecnocim.Alia.DataInfrastructure;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;


namespace Tecnocim.Alia.Application.CommandHandlers
{
    public class UploadFicheroCommandHandler : IRequestHandler<UploadFicheroCommand, GenericResult<UploadFicheroResponse>>
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IExtractorService _extractorService;
        private readonly Models.Fichero _ficheroOptions;
        private readonly ILogger<UploadFicheroCommandHandler> _logger;

        public UploadFicheroCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<Models.Fichero> ficheroOptions,
            IExtractorService extractorService,
            ILogger<UploadFicheroCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _extractorService = extractorService;
            _ficheroOptions = ficheroOptions.Value ?? throw new ArgumentNullException(nameof(ficheroOptions));
            _logger = logger;
        }

        public async Task<GenericResult<UploadFicheroResponse>> Handle(UploadFicheroCommand request, CancellationToken cancellationToken)
        {
            var result = new GenericResult<UploadFicheroResponse>();
            try
            {
                if (request.Usuario is not Usuario usuario)
                {
                    return result.Failed(404, "El usuario no es válido.");
                }

                var existingUser = await _unitOfWork.UsuarioRepository.GetFirstOrDefault(x => x, x => x.UsuarioId == usuario.UsuarioId && !x.Deleted.HasValue,
                 null, null, false);

                if (existingUser is null)
                {
                    return result.Failed(404, "No se ha encontrado el usuario al que pertenece la empresa.");
                }

                var file = request.FicheroRequest.Fichero;
                var fileName = file.FileName;
                var newFileName = fileName;

                if (!Directory.Exists(_ficheroOptions.RutaCompleta))
                {
                    Directory.CreateDirectory(_ficheroOptions.RutaCompleta);
                }

                if (!File.Exists(Path.Combine(_ficheroOptions.RutaCompleta, newFileName)))
                {
                    using var stream = new FileStream(Path.Combine(_ficheroOptions.RutaCompleta, newFileName), FileMode.Create);
                    file.CopyTo(stream);
                }
                else
                {
                    var i = 1;
                    while (true)
                    {
                        if (fileName.LastIndexOf('.') <= 0)
                        {
                            throw new ApplicationException("No se encuentra el punto de la extensión del fichero");
                        }

                        newFileName = fileName.Insert(fileName.LastIndexOf('.'), $"_{i}");
                        if (!File.Exists(Path.Combine(_ficheroOptions.RutaCompleta, newFileName)))
                        {
                            using var stream = new FileStream(Path.Combine(_ficheroOptions.RutaCompleta, newFileName), FileMode.Create);
                            file.CopyTo(stream);
                            break;
                        }
                        i++;
                    }
                }

                Fichero? fichero = null;
                await Task.Run(() =>
                {
                    fichero = new Fichero
                    {
                        EmpresaId = request.FicheroRequest.EmpresaId,
                        Estado = null,
                        ExtractorId = null,
                        FechaContenido = DateOnly.FromDateTime(request.FicheroRequest.FechaContenido),
                        Nombre = newFileName,
                        Origen = request.FicheroRequest.Origen,
                        UsuarioId = usuario.UsuarioId,
                        Created = DateTime.UtcNow,
                        Updated = DateTime.UtcNow
                    };

                    _unitOfWork.FicheroRepository.Insert(fichero);
                    _unitOfWork.Commit();
                }, cancellationToken);

                if(fichero is null)
                {
                    return result.Failed(500, "Error al guardar la información del fichero");
                }

                var response = new UploadFicheroResponse
                {
                    FicheroId = fichero.FicheroId,
                    Estado = fichero.Estado,
                    ExtractorId = fichero.ExtractorId,
                    FechaContenido = new DateTime(fichero.FechaContenido.Year, fichero.FechaContenido.Month, fichero.FechaContenido.Day),
                    Nombre = fichero.Nombre,
                    Origen = fichero.Origen,
                    Created = fichero.Created,
                    Updated = fichero.Updated,
                    EmpresaId = fichero.EmpresaId
                };

                // llamada al extractor
                await _extractorService.Extract(response);
                // grabo estado del proceso
                fichero.Estado = response.Estado;
                fichero.ExtractorId = response.ExtractorId;
                
                // si hay ido bien realizo la copia de datos entre bases de datos
                
                if (fichero.Estado=="ok")
                {
                    await _extractorService.Sincroniza(response);
                    fichero.Estado = response.Estado;
                }
                // paso de respuesta para el frontend el simplificado, 1 o 0
                // podría pasar el campo Status del Documento pero quizá no es exacto pq puede que el Documento esté a 1 y haya habido problemas de copia
                if (response.Estado == "SINCRO-FULL")
                {
                    response.Estado = "1";
                } else
                {
                    response.Estado = "0";
                }
                // Al response debo agregarle el Documento ID y la info de Errores

                _unitOfWork.FicheroRepository.Update(fichero);
                _unitOfWork.Commit();

                return result.Ok(response);
            }
            catch (Exception exception)
            {
                
                _logger.LogError("Error al publicar el fichero", exception.Message + " " + exception.StackTrace);
                return result.Failed(500, $"Error al publicar el fichero: {exception.Message + " " + exception.StackTrace}");
            }
        }
    }
}
