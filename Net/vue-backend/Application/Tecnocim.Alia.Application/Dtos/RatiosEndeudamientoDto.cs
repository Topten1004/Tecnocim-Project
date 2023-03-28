namespace Tecnocim.Alia.Application.Dtos;

public class RatiosEndeudamientoDto
{
    public string Nombre { get; set; }
    public RatiosRatiosEndeudamientoDto Ratios { get; set; }
    public string Columnas { get; set; }
}

public class RatiosRatiosEndeudamientoDto
{
    public TotalRatiosStringDto TotalRatiosEndeudamiento { get; set; }
    public TotalRatiosStringDto TotalRatiosCalidadDeuda { get; set; }
}
