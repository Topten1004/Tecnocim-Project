using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetDocumentoByIdQuery : IRequest<GenericResult<DocumentoErroresDto>>
{
    public GetDocumentoByIdQuery(long documentoId, object usuario)
    {
        DocumentoId = documentoId;
        Usuario = usuario;
    }

    public object Usuario { get; }
    public long DocumentoId { get; }
}