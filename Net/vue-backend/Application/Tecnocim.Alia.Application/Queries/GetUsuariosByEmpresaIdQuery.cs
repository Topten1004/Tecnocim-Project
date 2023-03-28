using MediatR;
using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Application.Queries;

public class GetUsuariosByEmpresaIdQuery : IRequest<IEnumerable<UsuarioListadoDto>>
{
    public GetUsuariosByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}
