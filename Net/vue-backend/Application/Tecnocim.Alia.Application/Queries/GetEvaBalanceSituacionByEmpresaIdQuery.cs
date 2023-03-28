using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetEvaBalanceSituacionByEmpresaIdQuery : IRequest<GenericResult<BalanceSituacionResponse>>
{
    public GetEvaBalanceSituacionByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
