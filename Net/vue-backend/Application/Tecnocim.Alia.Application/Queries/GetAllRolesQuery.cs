using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetAllRolesQuery : IRequest<GenericResult<IEnumerable<RolDto>>>
{
    public GetAllRolesQuery()
    {
    }
}
