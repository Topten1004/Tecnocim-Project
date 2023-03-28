using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetEvaBalanceSituacionPasivoByEmpresaIdQuery : IRequest<GenericResult<BalanceSituacionResponse>>
{
    public GetEvaBalanceSituacionPasivoByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
