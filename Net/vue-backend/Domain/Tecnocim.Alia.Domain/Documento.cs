namespace Tecnocim.Alia.Domain;

public class Documento : AuditableEntity
{
    public Documento()
    {
        Ratios = new HashSet<Ratio>();
        Operaciones = new HashSet<Operacion>();
        Analiticas = new HashSet<Analitica>();
        Contabilidades = new HashSet<Contabilidad>();
        Pools = new HashSet<Pool>();
        Cirbes = new HashSet<Cirbe>();
    }

    public long DocumentoId { get; set; }
    public string RutaDocumento { get; set; }
    public DateTime Fecha { get; set; }
    public int? ExtractorId { get; set; }
    public string Origen { get; set; }
    public bool Status { get; set; }

    public int EmpresaId { get; set; }
    public virtual Empresa Empresa { get; set; }

    public virtual ICollection<Ratio> Ratios { get; set; }
    public virtual ICollection<Operacion> Operaciones { get; set; }
    public virtual ICollection<Analitica> Analiticas { get; set; }
    public virtual ICollection<Contabilidad> Contabilidades { get; set; }
    public virtual ICollection<Pool> Pools { get; set; }
    public virtual ICollection<Cirbe> Cirbes { get; set; }
}
