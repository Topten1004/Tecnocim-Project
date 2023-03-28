namespace Tecnocim.Alia.Domain;

public class Contabilidad : AuditableEntity
{
    public long ContabilidadId { get; set; }
    public string Concepto { get; set; }
    public decimal? Magnitud { get; set; }
    public string Codigo { get; set; }

    public long DocumentoId { get; set; }
    public virtual Documento Documento { get; set; }
}
