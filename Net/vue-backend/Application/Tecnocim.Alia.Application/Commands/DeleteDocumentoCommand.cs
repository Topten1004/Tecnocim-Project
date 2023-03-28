using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Commands;

public record struct DeleteDocumentoCommand(int documentoId, object? usuario) : IRequest<GenericResult<DeleteDocumentoResponse>>;