using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetInversionPorEntidadesByEmpresaIdQuery : IRequest<GenericResult<InversionEntidadesResponse>>
{
    public GetInversionPorEntidadesByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
