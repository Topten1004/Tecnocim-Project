using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnocim.Alia.Domain;

public class Usuario : AuditableEntity
{
    public Usuario()
    {
        Empresas = new HashSet<Empresa>();
        RefreshTokens = new HashSet<RefreshToken>();
        Operaciones = new HashSet<Operacion>();
        Ficheros = new HashSet<Fichero>();
    }

    public int UsuarioId { get; set; }
    public string Nombre { get; set; } = null;
    public string Apellidos { get; set; } = null;
    public string Email { get; set; } = null;
    public string Password { get; set; } = null;
    public string PuestoTrabajo { get; set; } = null;

    public int RolId  { get; set; }
    public virtual Rol Rol { get; set; } = null;

    public virtual ICollection<Empresa> Empresas { get; set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    public virtual ICollection<Operacion> Operaciones { get; set; }
    public virtual ICollection<Fichero> Ficheros { get; set; }
}
