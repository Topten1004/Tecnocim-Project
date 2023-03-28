namespace Tecnocim.Alia.Domain;

public class Empresa : AuditableEntity
{
    public Empresa()
    {
        Usuarios = new HashSet<Usuario>();
        EmpresaConfiguraciones = new HashSet<EmpresaConfiguraciones>();
        Documentos = new HashSet<Documento>();
        Operaciones = new HashSet<Operacion>();
        Ficheros = new HashSet<Fichero>();
    }

    public int EmpresaId { get; set; }
    public string CIF { get; set; }
    public string Nombre { get; set; }
    public string Contacto { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; }
    public virtual ICollection<EmpresaConfiguraciones> EmpresaConfiguraciones { get; set; }
    public virtual ICollection<Documento> Documentos { get; set; }
    public virtual ICollection<Operacion> Operaciones { get; set; }
    public virtual ICollection<Fichero> Ficheros { get; set; }
}
