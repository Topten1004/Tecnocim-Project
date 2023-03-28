using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetCirculantePorEntidadesByEmpresaIdQuery : IRequest<GenericResult<TotalEntidadesResponse>>
{
    public GetCirculantePorEntidadesByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
