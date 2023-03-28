using System.ComponentModel.DataAnnotations.Schema;

namespace Tecnocim.Alia.Domain;

public class Rol
{
    public Rol()
    {
        Usuarios = new HashSet<Usuario>();
    }

    public int RolId { get; set; }
    public string Nombre { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; }
}
