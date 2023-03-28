using MediatR;

namespace Tecnocim.Alia.Application.Queries;

public class GetUsuarioEmpresaAsignadaQuery : IRequest<bool>
{
    public GetUsuarioEmpresaAsignadaQuery(object usuario, int empresaId)
    {
        Usuario = usuario;
        EmpresaId = empresaId;
    }

    public Object Usuario { get; }
    public int EmpresaId { get; }
}
