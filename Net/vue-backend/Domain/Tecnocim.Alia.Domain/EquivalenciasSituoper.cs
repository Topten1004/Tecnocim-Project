namespace Tecnocim.Alia.Domain;

public class EquivalenciasSituoper
{
    public EquivalenciasSituoper()
    {
        Cirbes = new HashSet<Cirbe>();
    }

    public int Id { get; set; }
    public string Tipo { get; set; }
    public string Descripcion { get; set; }

    public virtual ICollection<Cirbe> Cirbes { get; set; }
}
