namespace Tecnocim.Alia.Domain;

public class Operacion : AuditableEntity
{
    public long OperacionId { get; set; }
    public DateTime Momento { get; set; }
    public string TextoOperacion { get; set; }

    public long? DocumentoId { get; set; }
    public virtual Documento Documento { get; set; }
    public int EmpresaId { get; set; }
    public virtual Empresa Empresa { get; set; }
    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }
}
