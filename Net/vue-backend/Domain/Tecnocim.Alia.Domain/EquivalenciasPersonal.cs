namespace Tecnocim.Alia.Domain;

public class EquivalenciasPersonal
{
    public EquivalenciasPersonal()
    {
        CirbePersonales = new HashSet<CirbePersonal>();
    }

    public int Id { get; set; }
    public string Tipo { get; set; }
    public string Descripcion { get; set; }

    public virtual ICollection<CirbePersonal> CirbePersonales { get; set; }
}
