namespace Tecnocim.Alia.Domain;

public class Pool : AuditableEntity
{
    public long PoolId { get; set; }
    public string Cuenta { get; set; }
    public string Concepto { get; set; }
    public decimal? Dispuesto { get; set; }

    public long? ContratoId { get; set; }
    public virtual Contrato Contrato { get; set; }
    public long DocumentoId { get; set; }
    public virtual Documento Documento { get; set; }
}