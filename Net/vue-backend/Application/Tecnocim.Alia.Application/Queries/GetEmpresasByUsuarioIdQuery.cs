using MediatR;
using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Application.Queries;

public class GetEmpresasByUsuarioIdQuery : IRequest<IEnumerable<EmpresaUsuarioDto>>
{
    public GetEmpresasByUsuarioIdQuery(int usuarioId)
    {
        UsuarioId = usuarioId;
    }

    public int UsuarioId { get; }
}
