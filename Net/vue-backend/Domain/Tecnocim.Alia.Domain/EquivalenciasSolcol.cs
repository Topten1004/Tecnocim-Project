namespace Tecnocim.Alia.Domain;

public class EquivalenciasSolcol
{
    public EquivalenciasSolcol()
    {
        Cirbes = new HashSet<Cirbe>();
    }

    public int Id { get; set; }
    public string Tipo { get; set; }

    public virtual ICollection<Cirbe> Cirbes { get; set; }
}
