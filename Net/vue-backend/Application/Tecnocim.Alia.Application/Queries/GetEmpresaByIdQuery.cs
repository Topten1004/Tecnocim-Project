using MediatR;
using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Application.Queries;

public class GetEmpresaByIdQuery : IRequest<EmpresaDto>
{
    public GetEmpresaByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
