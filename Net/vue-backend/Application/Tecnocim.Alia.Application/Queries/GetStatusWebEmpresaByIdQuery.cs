using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetStatusWebEmpresaByIdQuery : IRequest<GenericResult<int>>
{
    public GetStatusWebEmpresaByIdQuery(int id, object? usuario)
    {
        Id = id;
        Usuario = usuario;
    }

    public int Id { get; }
    public object? Usuario { get; }
}
