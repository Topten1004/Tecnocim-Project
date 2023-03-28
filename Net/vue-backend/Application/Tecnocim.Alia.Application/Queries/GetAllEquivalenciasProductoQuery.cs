using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetAllEquivalenciasProductoQuery : IRequest<GenericResult<IEnumerable<EquivalenciaProductoDto>>>
{
}
