using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetEquivalenciaEntidadByIdQuery : IRequest<GenericResult<EquivalenciaEntidadDto>>
{
    public GetEquivalenciaEntidadByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
