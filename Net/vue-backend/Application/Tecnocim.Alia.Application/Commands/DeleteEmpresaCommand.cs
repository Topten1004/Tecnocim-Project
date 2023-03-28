using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Commands;

public record struct DeleteEmpresaCommand(int empresaId) : IRequest<GenericResult<DeleteEmpresaResponse>>;
