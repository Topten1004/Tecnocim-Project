using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetPoolBancarioEstadosByEmpresaIdQuery : IRequest<GenericResult<PoolBancarioEstadosResponse>>
{
    public GetPoolBancarioEstadosByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}


