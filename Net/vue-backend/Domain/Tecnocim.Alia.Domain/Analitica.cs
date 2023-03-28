namespace Tecnocim.Alia.Domain;

public class Analitica : AuditableEntity
{
    public long AnaliticaId { get; set; }
    public string Cuenta { get; set; }
    public decimal? Magnitud { get; set; }

    public long DocumentoId { get; set; }
    public virtual Documento Documento { get; set; }
}
