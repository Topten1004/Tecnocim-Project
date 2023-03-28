namespace Tecnocim.Alia.Domain;

public class AuditableEntity
{
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public DateTime? Deleted { get; set; }
}