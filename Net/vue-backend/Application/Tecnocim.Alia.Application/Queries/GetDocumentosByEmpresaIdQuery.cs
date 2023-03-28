using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetDocumentosByEmpresaIdQuery : IRequest<GenericResult<DocumentoErroresResponse>>
{
    public GetDocumentosByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
