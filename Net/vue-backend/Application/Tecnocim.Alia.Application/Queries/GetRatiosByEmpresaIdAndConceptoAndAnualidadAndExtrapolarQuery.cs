using MediatR;
using Tecnocim.Alia.Application.Dtos;
using Tecnocim.Alia.Application.Responses;

namespace Tecnocim.Alia.Application.Queries;

public class GetRatiosByEmpresaIdAndConceptoAndAnualidadAndExtrapolarQuery : IRequest<GenericResult<RatioEmpresaConceptoResponse>>
{
    public GetRatiosByEmpresaIdAndConceptoAndAnualidadAndExtrapolarQuery(int empresaId, string concepto, int? anualidad, bool? extrapolar, object? usuario)
    {
        EmpresaId = empresaId;
        Concepto = concepto;
        Anualidad = anualidad;
        Extrapolar = extrapolar;
        Usuario = usuario;
    }

    public int EmpresaId { get; }
    public string Concepto { get; }
    public int? Anualidad { get; }
    public bool? Extrapolar { get; }
    public object? Usuario { get; }
}
