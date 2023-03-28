namespace Tecnocim.Alia.Domain;

public class Ratio : AuditableEntity
{
    public long RatioId { get; set; }
    public string Concepto { get; set; }
    public decimal Magnitud { get; set; }

    public long DocumentoId { get; set; }
    public virtual Documento Documento { get; set; }
}
