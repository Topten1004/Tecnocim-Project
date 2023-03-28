using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetEbitdaVsServicioDeudaByEmpresaIdQuery : IRequest<GenericResult<VsServicioDeudaDto>>
{
    public GetEbitdaVsServicioDeudaByEmpresaIdQuery(int empresaId, int? anualidad, object? usuario)
    {
        EmpresaId = empresaId;
        Anualidad = anualidad;
        Usuario = usuario;
    }

    public int EmpresaId { get; }
    public int? Anualidad { get; }
    public object? Usuario { get; }
}
