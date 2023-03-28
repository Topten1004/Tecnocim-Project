using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetEquivalenciaProductoByIdQuery : IRequest<GenericResult<EquivalenciaProductoDto>>
{
    public GetEquivalenciaProductoByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
