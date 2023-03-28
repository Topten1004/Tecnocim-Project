namespace Tecnocim.Alia.Application.Dtos;

public class RatioInterpretacionDto
{
    public string Concepto { get; set; }
    public string Nombre { get; set; }
    public string ColorPositivo { get; set; }
    public string ColorNegativo { get; set; }
    public string IconoPositivo { get; set; }
    public string IconoNegativo { get; set; }
    public string TendenciaColor { get; set; }
    public string TendenciaIcono { get; set; }
    public string TendenciaAnteriorColor { get; set; }
    public string TendenciaAnteriorIcono { get; set; }
    public int Tipo { get; set; }
}