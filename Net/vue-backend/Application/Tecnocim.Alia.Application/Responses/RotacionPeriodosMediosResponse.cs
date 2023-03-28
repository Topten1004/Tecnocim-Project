using Tecnocim.Alia.Application.Dtos;

namespace Tecnocim.Alia.Application.Responses;

public class RotacionPeriodosMediosResponse
{
    public string Nombre { get; set; }
    public RatiosRotacionPeriodosMediosDto Ratios { get; set; }
    public string Columnas { get; set; }
}

public class RatiosRotacionPeriodosMediosDto
{
    public TotalPrevisionDemandasStringDto TotalPmProveedores { get; set; }
    public TotalPrevisionDemandasStringDto TotalPmCobro { get; set; }
    public TotalPrevisionDemandasStringDto TotalPmAlmacenamiento { get; set; }
    public TotalPrevisionDemandasStringDto TotalPmMaduracion { get; set; }
}