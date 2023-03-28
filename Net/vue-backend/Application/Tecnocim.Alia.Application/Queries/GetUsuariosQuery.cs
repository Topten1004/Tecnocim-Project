using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetUsuariosQuery : IRequest<GenericResult<IEnumerable<UsuarioListadoDto>>>
{
    public GetUsuariosQuery()
    {
    }
}
