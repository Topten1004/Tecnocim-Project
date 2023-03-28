using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetAnalisisServicioDeudaByEmpresaIdQuery : IRequest<GenericResult<AnalisisServicioDeudaResponse>>
{
    public GetAnalisisServicioDeudaByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
