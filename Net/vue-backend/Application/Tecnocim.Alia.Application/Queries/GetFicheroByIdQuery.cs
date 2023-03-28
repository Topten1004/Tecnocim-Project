using MediatR;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetFicheroByIdQuery : IRequest<GenericResult<UploadFicheroResponse>>
{
    public GetFicheroByIdQuery(int id, object? usuario)
    {
        Id = id;
        Usuario = usuario;
    }

    public int Id { get; }
    public object? Usuario { get; }
}