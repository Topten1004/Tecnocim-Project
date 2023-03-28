using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tecnocim.Alia.Application.Commands;
using Tecnocim.Alia.Application.Responses;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.Application.CommandHandlers;

public class DeleteDocumentoCommandHandler : IRequestHandler<DeleteDocumentoCommand, GenericResult<DeleteDocumentoResponse>>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteDocumentoCommandHandler> _logger;

    public DeleteDocumentoCommandHandler(
        IServiceProvider serviceProvider,
        IMapper mapper,
        ILogger<DeleteDocumentoCommandHandler> logger)
    {
        _serviceProvider = serviceProvider;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GenericResult<DeleteDocumentoResponse>> Handle(DeleteDocumentoCommand request, CancellationToken cancellationToken)
    {
        var result = new GenericResult<DeleteDocumentoResponse>();
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var documentos = await unitOfWork.DocumentoRepository.GetIncludeAsync(x => x, x => x.DocumentoId == request.documentoId && !x.Deleted.HasValue, null,
           x => x.Include(y => y.Empresa), false);

            if (documentos is not null && documentos.Any())
            {
                if (request.usuario is not Usuario usuario)
                {
                    return result.Failed(401, "El usuario no tiene asignada la empresa a la que pertenece el documento");
                }

                var usuarioExistente = await unitOfWork.UsuarioRepository.GetFirstAsync(x => !x.Deleted.HasValue && x.UsuarioId == usuario.UsuarioId
                && x.Empresas.Any(t => t.EmpresaId == documentos.First().EmpresaId), null, x => x.Empresas);

                if (usuarioExistente is null)
                {
                    return result.Failed(401, "El usuario no tiene asignada la empresa a la que pertenece el documento");
                }

                var documento = documentos.First();

                await Task.Run(() =>
                {
                    unitOfWork.DocumentoRepository.Delete(documento);
                    unitOfWork.Commit();
                }, cancellationToken);

                var deleteResponse = new DeleteDocumentoResponse { IsSuccess = true };

                return result.Ok(deleteResponse);
            }

            return result.Failed(404, "No se ha encontrado el documento a eliminar");
        }
        catch (Exception exception)
        {
            _logger.LogError("Error al eliminar el documento", exception);
            return result.Failed(500, $"Error al eliminar el documento: {Environment.NewLine}{exception.Message}");
        }
    }
}
