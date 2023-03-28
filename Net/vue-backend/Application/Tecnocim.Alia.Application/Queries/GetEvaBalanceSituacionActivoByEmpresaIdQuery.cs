using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetEvaBalanceSituacionActivoByEmpresaIdQuery : IRequest<GenericResult<BalanceSituacionResponse>>
{
    public GetEvaBalanceSituacionActivoByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
