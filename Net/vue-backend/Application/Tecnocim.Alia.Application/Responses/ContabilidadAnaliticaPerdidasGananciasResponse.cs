namespace Tecnocim.Alia.Application.Responses;

public class ContabilidadAnaliticaPerdidasGananciasResponse
{
    public string TotalGastos { get; set; }
    public string TotalIngresos { get; set; }
    public IEnumerable<CalculosAnaliticasDto> List { get; set; }
}
