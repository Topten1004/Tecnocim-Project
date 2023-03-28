using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetUsuarioByIdQuery : IRequest<GenericResult<UsuarioListadoDto>>
{
    public GetUsuarioByIdQuery(int usuarioId)
    {
        UsuarioId = usuarioId;
    }

    public int UsuarioId { get; }
}