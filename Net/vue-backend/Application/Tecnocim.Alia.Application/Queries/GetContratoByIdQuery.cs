using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetContratoByIdQuery : IRequest<GenericResult<ContratoDto>>
{
    public GetContratoByIdQuery(int contratoId, object? usuario)
    {
        ContratoId = contratoId;
        Usuario = usuario;
    }

    public int ContratoId { get; }
    public object? Usuario { get; }
}