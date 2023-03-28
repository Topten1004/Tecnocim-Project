namespace Tecnocim.Alia.Application.Dtos;

public class TotalRatiosStringDto
{
    public string Descripcion { get; set; }
    public string TotalActual { get; set; }
    public string Tendencia { get; set; }
    public string TotalAnterior { get; set; }
    public string TendenciaAnterior { get; set; }
    public string TotalAnterior2 { get; set; }
    public string Divisa { get; set; }
    public RatioInterpretacionDto Interpretacion { get; set; }
}
