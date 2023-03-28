using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetPoolBancarioPendienteTipo52ByEmpresaIdQuery : IRequest<GenericResult<PoolDtoResponse>>
{
    public GetPoolBancarioPendienteTipo52ByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}

