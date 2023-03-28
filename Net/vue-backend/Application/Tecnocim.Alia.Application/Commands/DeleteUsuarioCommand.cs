using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Commands;

public record struct DeleteUsuarioCommand(int usuarioId) : IRequest<GenericResult<DeleteUsuarioResponse>>;
