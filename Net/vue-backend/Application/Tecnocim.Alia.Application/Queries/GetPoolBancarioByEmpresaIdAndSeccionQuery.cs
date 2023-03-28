using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetPoolBancarioByEmpresaIdAndSeccionQuery : IRequest<GenericResult<PoolBancarioResponse>>
{
    public GetPoolBancarioByEmpresaIdAndSeccionQuery(int empresaId, string seccion)
    {
        EmpresaId = empresaId;
        Seccion = seccion;
    }

    public int EmpresaId { get; }
    public string Seccion { get; }
}
