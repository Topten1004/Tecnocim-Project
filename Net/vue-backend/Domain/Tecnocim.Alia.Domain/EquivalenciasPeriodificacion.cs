namespace Tecnocim.Alia.Domain;

public class EquivalenciasPeriodificacion
{
    public EquivalenciasPeriodificacion()
    {
        Contratos = new HashSet<Contrato>();
    }

    public short Id { get; set; }
    public string Nombre { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; }
}
