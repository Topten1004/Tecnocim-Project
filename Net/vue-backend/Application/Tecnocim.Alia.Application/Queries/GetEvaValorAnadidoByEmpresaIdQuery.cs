using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetEvaValorAnadidoByEmpresaIdQuery : IRequest<GenericResult<EvaValorAnadidoResponse>>
{
    public GetEvaValorAnadidoByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
