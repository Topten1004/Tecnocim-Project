using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnocim.Alia.Domain;

public class EmpresaConfiguraciones : AuditableEntity
{
    public int EmpresaConfiguracionesId { get; set; }
    public DateTime Fecha { get; set; }
    public bool PorDefecto { get; set; }
    public string FicheroConfiguracion { get; set; } = null;

    public int EmpresaId { get; set; }
    public virtual Empresa Empresa { get; set; } = null;
}
