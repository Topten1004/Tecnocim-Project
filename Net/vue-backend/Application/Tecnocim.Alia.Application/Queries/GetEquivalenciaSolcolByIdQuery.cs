using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetEquivalenciaSolcolByIdQuery : IRequest<GenericResult<EquivalenciaSolcolDto>>
{
    public GetEquivalenciaSolcolByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
