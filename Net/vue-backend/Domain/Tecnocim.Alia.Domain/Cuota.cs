namespace Tecnocim.Alia.Domain;

public class Cuota
{
    public long ContratoId { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Importe { get; set; }
    public bool Carencia { get; set; }
    
    public virtual Contrato Contrato { get; set; }
}
