namespace Tecnocim.Alia.Domain;

public class CirbeReal : AuditableEntity
{
    public int CirbeRealId { get; set; }

    public int CirbeId { get; set; }
    public virtual Cirbe Cirbe { get; set; }
    public int EquivalenciasRealId { get; set; }
    public virtual EquivalenciasReal EquivalenciasReal { get; set; }
}