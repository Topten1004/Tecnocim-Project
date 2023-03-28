namespace Tecnocim.Alia.Application.Responses;

public class AnalisisServicioDeuda
{
    public long ContratoId { get; set; }
    public string Entidad { get; set; }
    public decimal Limite { get; set; }
    public string Inicio { get; set; }
    public string Vencimiento { get; set; }
    public string Divisa { get; set; }
    public IEnumerable<CuotaDto> Cuotas { get; set; }
}
