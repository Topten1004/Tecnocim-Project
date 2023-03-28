namespace Tecnocim.Alia.Domain;

public class EquivalenciasEntidad
{
    public EquivalenciasEntidad()
    {
        Contratos = new HashSet<Contrato>();
        Cirbes = new HashSet<Cirbe>();
    }

    public int Id { get; set; }
    public string Codigo { get; set; }
    public string Nombre { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; }
    public virtual ICollection<Cirbe> Cirbes { get; set; }
}
