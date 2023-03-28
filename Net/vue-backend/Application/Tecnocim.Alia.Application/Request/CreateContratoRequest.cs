namespace Tecnocim.Alia.Application.Request;

public class CreateContratoRequest
{
    public long Cuenta { get; set; }
    public long? Cuenta2 { get; set; }
    public int Entidad { get; set; }
    public int TipoProducto { get; set; }
    public decimal ImporteInicial { get; set; }
    public int Moneda { get; set; }
    public short FormaDePago { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public decimal Precio { get; set; }
    public int Carencia { get; set; }
    public int Cirbe { get; set; }
    public bool? PolizaDigitalizada { get; set; }
    public bool? Minimis { get; set; }
    public int? Valoracion { get; set; }
    public string NotasConsultor { get; set; }
}
