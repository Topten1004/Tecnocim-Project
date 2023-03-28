using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetEvaBalanceSituacionPygByEmpresaIdQuery : IRequest<GenericResult<BalanceSituacionResponse>>
{
    public GetEvaBalanceSituacionPygByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}