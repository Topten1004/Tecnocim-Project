using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public abstract class GetEquivalenciaByIdQueryBase : IRequest<GenericResult<EquivalenciaDto>>
{
    public GetEquivalenciaByIdQueryBase(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
