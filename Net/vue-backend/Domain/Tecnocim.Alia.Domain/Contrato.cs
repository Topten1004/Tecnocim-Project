namespace Tecnocim.Alia.Domain;

public class Contrato : AuditableEntity
{
    public Contrato()
    {
        Pools = new HashSet<Pool>();
        Cirbes = new HashSet<Cirbe>();
        Cuotas = new HashSet<Cuota>();
    }

    public long ContratoId { get; set; }
    public DateOnly Inicio { get; set; }
    public DateOnly Vencimiento { get; set; }
    public int Carencia { get; set; }
    public decimal Precio { get; set; }
    public decimal Limite { get; set; }
    public int? PlazosAmortizacion { get; set; }
    public int? Valoracion { get; set; }
    public string Notas { get; set; }
    public bool? Digitalizada { get; set; }
    public bool? Minimis { get; set; }

    public int EquivalenciasEntidadId { get; set; }
    public virtual EquivalenciasEntidad EquivalenciasEntidad { get; set; }
    public int EquivalenciasMonedaId { get; set; }
    public virtual EquivalenciasMoneda EquivalenciasMoneda { get; set; }
    public int EquivalenciasProductoId { get; set; }
    public virtual EquivalenciasProducto EquivalenciasProducto { get; set; }
    public short EquivalenciasPeriodificacionId { get; set; }
    public virtual EquivalenciasPeriodificacion EquivalenciasPeriodificacion { get; set; }

    public virtual ICollection<Pool> Pools { get; set; }
    public virtual ICollection<Cirbe> Cirbes { get; set; }
    public virtual ICollection<Cuota> Cuotas { get; set; }
}
