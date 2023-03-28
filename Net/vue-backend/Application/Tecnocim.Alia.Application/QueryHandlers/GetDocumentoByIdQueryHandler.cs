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

public class GetDocumentoByIdQueryHandler : IRequestHandler<GetDocumentoByIdQuery, GenericResult<DocumentoErroresDto>>
{
    private readonly ILogger<GetDocumentoByIdQueryHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public GetDocumentoByIdQueryHandler(
        ILogger<GetDocumentoByIdQueryHandler> logger,
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }

    public async Task<GenericResult<DocumentoErroresDto>> Handle(GetDocumentoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<DocumentoErroresDto>();

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => x.DocumentoId == request.DocumentoId && !x.Deleted.HasValue, null,
                x => x.Include(y => y.Empresa));

            if (documentos is not null && documentos.Any())
            {
                if (request.Usuario is not Usuario usuario)
                {
                    return result.Failed(401, "El usuario no tiene asignada la empresa a la que pertenece el documento");
                }

                var usuarioExistente = await unitOfWork.UsuarioRepository.GetFirstAsync(x => !x.Deleted.HasValue && x.UsuarioId == usuario.UsuarioId
                && x.Empresas.Any(t => t.EmpresaId == documentos.First().EmpresaId), null, x => x.Empresas);

                if(usuarioExistente is null)
                {
                    return result.Failed(401, "El usuario no tiene asignada la empresa a la que pertenece el documento");
                }

                var documentoDto = _mapper.Map<Documento, DocumentoErroresDto>(documentos.First());
                
                var intermedia_unitOfWork = scope.ServiceProvider.GetService<Intermedia.Domain.Repositories.IUnitOfWork>();
                var errores = await intermedia_unitOfWork.CoreExtraccionesErroreRepository
                            .GetIncludeAsync(z => z, z => z.ExtraccionId == documentoDto.ExtractorId);
                documentoDto.Errores = errores?.Select(z => new ErroresResponse(z.Mensaje, z.Traza, z.Bloqueo));

                return result.Ok(documentoDto);
            }

            return result.NotFound();
        }
        catch (Exception exception)
        {
            _logger.LogError("Error al obtener el documento", exception);
            return result.Failed(500, "Error al obtener el documento.");
        }
    }
}
