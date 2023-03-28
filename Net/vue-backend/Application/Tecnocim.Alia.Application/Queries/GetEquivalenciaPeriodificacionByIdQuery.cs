using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetEquivalenciaPeriodificacionByIdQuery : IRequest<GenericResult<EquivalenciaPeriodificacionDto>>
{
    public GetEquivalenciaPeriodificacionByIdQuery(short id)
    {
        Id = id;
    }

    public short Id { get; }
}