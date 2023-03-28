using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetDocumentosVigentesByEmpresaIdQuery : IRequest<GenericResult<DocumentoErroresResponse>>
{
    public GetDocumentosVigentesByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}

