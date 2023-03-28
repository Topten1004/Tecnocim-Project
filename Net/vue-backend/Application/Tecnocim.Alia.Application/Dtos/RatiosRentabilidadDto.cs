namespace Tecnocim.Alia.Application.Dtos;

public class RatiosRentabilidadDto
{
    public string Nombre { get; set; }
    public RatiosRatiosRentabilidadDto Ratios { get; set; }
    public string Columnas { get; set; }
}

public class RatiosRatiosRentabilidadDto
{
    public TotalRatiosStringDto TotalRatiosPuntoMuerto { get; set; }
    public TotalRatiosStringDto TotalRatiosROE { get; set; }
    public TotalRatiosStringDto TotalRatiosROAROI { get; set; }
}