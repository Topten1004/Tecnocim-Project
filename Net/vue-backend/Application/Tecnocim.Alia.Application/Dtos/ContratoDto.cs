namespace Tecnocim.Alia.Application.Dtos;

public class ContratoDto
{
    public long ContratoId { get; set; }
    public string Cuenta2 { get; set; }
    public string Inicio { get; set; }
    public string Vencimiento { get; set; }
    public int Carencia { get; set; }
    public string Precio { get; set; }
    public string Limite { get; set; }
    public int? PlazosAmortizacion { get; set; }
    public int? Valoracion { get; set; }
    public string Notas { get; set; }
    public bool? Digitalizada { get; set; }
    public bool? Minimis { get; set; }
    public string Created { get; set; }
    public EquivalenciaEntidadDto EquivalenciasEntidad { get; set; }
    public EquivalenciaMonedaDto EquivalenciasMoneda { get; set; }
    public EquivalenciaProductoDto EquivalenciasProducto { get; set; }
    public EquivalenciaPeriodificacionDto EquivalenciasPeriodificacion { get; set; }
}