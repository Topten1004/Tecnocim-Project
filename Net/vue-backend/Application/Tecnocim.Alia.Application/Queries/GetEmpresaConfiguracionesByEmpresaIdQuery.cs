using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetEmpresaConfiguracionesByEmpresaIdQuery : IRequest<GenericResult<IEnumerable<EmpresaConfiguracionesDto>>>
{
    public GetEmpresaConfiguracionesByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}

