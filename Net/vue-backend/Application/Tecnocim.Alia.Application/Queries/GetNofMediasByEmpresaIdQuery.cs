using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetNofMediasByEmpresaIdQuery : IRequest<GenericResult<PrevisionDemandasResponse>>
{
    public GetNofMediasByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
