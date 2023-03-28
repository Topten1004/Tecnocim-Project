using MediatR;
using Tecnocim.Alia.Application.Request;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Commands;

public record struct CreateEmpresaCommand(CreateEmpresaRequest empresa) : IRequest<GenericResult<CreateEmpresaResponse>>;
