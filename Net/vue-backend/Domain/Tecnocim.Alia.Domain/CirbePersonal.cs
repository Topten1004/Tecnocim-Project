namespace Tecnocim.Alia.Domain;

public class CirbePersonal : AuditableEntity
{
    public int CirbePersonalId { get; set; }

    public int CirbeId { get; set; }
    public virtual Cirbe Cirbe { get; set; }
    public int EquivalenciasPersonalId { get; set; }
    public virtual EquivalenciasPersonal EquivalenciasPersonal { get; set; }
}
