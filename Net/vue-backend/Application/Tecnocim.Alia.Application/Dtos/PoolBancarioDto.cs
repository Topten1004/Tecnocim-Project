using Tecnocim.Alia.Domain;

namespace Tecnocim.Alia.Application.Dtos;

public class PoolBancarioDto
{
    public string Entidad { get; set; }
    public string Producto { get; set; }
    public decimal ImporteInicial { get; set; }
    public decimal Dispuesto { get; set; }
    public decimal Disponible { get; set; }
    public string Inicio { get; set; }
    public string Fin { get; set; }
    public short FormaPago { get; set; }
    public decimal Cuota { get; set; }
    public long Numero { get; set; }
    public decimal ServicioDeuda { get; set; }
    public bool Cirbe { get; set; }
    public decimal Valoracion { get; set; }
    public string Notas { get; set; }
    public decimal Precio { get; set; }
    public short PolizaDigitalizada { get; set; }
    public short Minimis { get; set; }
    public string Divisa { get; set; }
}
