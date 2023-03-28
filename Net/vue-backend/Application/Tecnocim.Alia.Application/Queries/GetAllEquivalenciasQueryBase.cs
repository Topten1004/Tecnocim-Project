using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetAllEquivalenciasQueryBase : IRequest<GenericResult<IEnumerable<EquivalenciaDto>>>
{

}
