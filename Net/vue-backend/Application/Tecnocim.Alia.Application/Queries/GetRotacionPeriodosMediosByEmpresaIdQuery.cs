using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetRotacionPeriodosMediosByEmpresaIdQuery : IRequest<GenericResult<RotacionPeriodosMediosResponse>>
{
    public GetRotacionPeriodosMediosByEmpresaIdQuery(int empresaId)
    {
        EmpresaId = empresaId;
    }

    public int EmpresaId { get; }
}