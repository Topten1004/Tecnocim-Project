using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetRatiosLiquidezByEmpresaIdQuery : IRequest<GenericResult<RatioLiquidezResponse>>
{
    public GetRatiosLiquidezByEmpresaIdQuery(int empresaId, object? usuario)
    {
        EmpresaId = empresaId;
        Usuario = usuario;
    }

    public int EmpresaId { get; }
    public object? Usuario { get; }
}
