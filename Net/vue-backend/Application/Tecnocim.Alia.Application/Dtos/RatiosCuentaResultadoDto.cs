namespace Tecnocim.Alia.Application.Dtos;

public class RatiosCuentaResultadoDto
{
    public string Nombre { get; set; }
    public RatiosRatiosCuentaResultadoDto Ratios { get; set; }
    public string Columnas { get; set; }
}

public class RatiosRatiosCuentaResultadoDto
{
    public TotalRatiosStringDto TotalRatiosFondoManiobra { get; set; }
    public TotalRatiosStringDto TotalRatiosEbitda { get; set; }
}
