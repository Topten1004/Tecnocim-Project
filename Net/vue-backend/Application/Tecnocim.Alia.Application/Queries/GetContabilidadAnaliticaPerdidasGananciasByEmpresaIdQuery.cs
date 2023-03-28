using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetContabilidadAnaliticaPerdidasGananciasByEmpresaIdQuery : IRequest<GenericResult<ContabilidadAnaliticaPerdidasGananciasResponse>>
{
    public GetContabilidadAnaliticaPerdidasGananciasByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
