namespace Tecnocim.Alia.Application.Dtos;

public class VsServicioDeudaDto
{
    public string Nombre { get; set; }
    public RatiosVsServicioDeudaDto Ratios { get; set; }
    public string Columnas { get; set; }
}

public class RatiosVsServicioDeudaDto
{
    public TotalRatiosStringDto TotalRatios1 { get; set; }
    public TotalRatiosStringDto TotalRatios2 { get; set; }
    public TotalRatiosStringDto TotalRatios3 { get; set; }
}
