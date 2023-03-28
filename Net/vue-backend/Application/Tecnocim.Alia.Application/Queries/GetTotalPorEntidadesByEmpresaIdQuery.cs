using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetTotalPorEntidadesByEmpresaIdQuery : IRequest<GenericResult<TotalEntidadesResponse>>
{
    public GetTotalPorEntidadesByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
