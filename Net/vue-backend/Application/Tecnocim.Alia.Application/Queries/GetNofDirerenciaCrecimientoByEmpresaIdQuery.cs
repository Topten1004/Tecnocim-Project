using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetNofDirerenciaCrecimientoByEmpresaIdQuery : IRequest<GenericResult<NofDirerenciaCrecimientoResponse>>
{
    public GetNofDirerenciaCrecimientoByEmpresaIdQuery(int empresaId, decimal incremento)
    {
        EmpresaId = empresaId;
        Incremento = incremento;
    }

    public int EmpresaId { get; }
    public decimal Incremento { get; }
}

