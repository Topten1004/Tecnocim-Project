using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetEquivalenciaMonedaByIdQuery : IRequest<GenericResult<EquivalenciaMonedaDto>>
{
    public GetEquivalenciaMonedaByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
