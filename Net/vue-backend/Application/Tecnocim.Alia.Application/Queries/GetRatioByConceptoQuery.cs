using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetInterpretacionByConceptoQuery : IRequest<GenericResult<InterpretacionDto>>
{
    public GetInterpretacionByConceptoQuery(string concepto)
    {
        Concepto = concepto;
    }

    public string Concepto { get; }
}
