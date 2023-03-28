using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetPoolBancarioTipo52ByEmpresaIdQuery : IRequest<GenericResult<PoolDtoResponse>>
{
    public GetPoolBancarioTipo52ByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
