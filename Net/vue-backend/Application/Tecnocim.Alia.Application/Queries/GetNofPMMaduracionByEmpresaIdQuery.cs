using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetNofPMMaduracionByEmpresaIdQuery : IRequest<GenericResult<PrevisionDemandasResponse>>
{
    public GetNofPMMaduracionByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}