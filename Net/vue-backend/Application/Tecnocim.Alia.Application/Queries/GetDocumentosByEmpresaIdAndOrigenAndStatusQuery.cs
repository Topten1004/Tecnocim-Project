using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetDocumentosByEmpresaIdAndOrigenAndStatusQuery : IRequest<GenericResult<DocumentoErroresResponse>>
{
    public GetDocumentosByEmpresaIdAndOrigenAndStatusQuery(int empresaId, string? origen, bool? status)
    {
        EmpresaId = empresaId;
        Origen = origen;
        Status = status;
    }

    public int EmpresaId { get; }
    public string? Origen { get; }
    public bool? Status { get; }
}
