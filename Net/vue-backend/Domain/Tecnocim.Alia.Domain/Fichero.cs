namespace Tecnocim.Alia.Domain;

public class Fichero : AuditableEntity
{
    public long FicheroId { get; set; }
    public string Nombre { get; set; }
    public string Origen { get; set; }
    public DateOnly FechaContenido { get; set; }

    public int EmpresaId { get; set; }
    public virtual Empresa Empresa { get; set; }
    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }
    public int? ExtractorId { get; set; } = null;
    public string? Estado { get; set; } = null;
}
