using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetPoolBancarioPendienteByEmpresaIdQuery : IRequest<GenericResult<PoolDtoResponse>>
{
    public GetPoolBancarioPendienteByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}

