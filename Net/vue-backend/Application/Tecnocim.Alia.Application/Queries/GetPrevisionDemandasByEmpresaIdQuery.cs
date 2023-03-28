using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetPrevisionDemandasByEmpresaIdQuery : IRequest<GenericResult<PrevisionDemandasResponse>>
{
    public GetPrevisionDemandasByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
