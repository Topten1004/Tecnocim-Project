namespace Tecnocim.Alia.Domain;

public class EquivalenciasReal
{
    public EquivalenciasReal()
    {
        CirbeReales = new HashSet<CirbeReal>();
    }

    public int Id { get; set; }
    public string Tipo { get; set; }
    public string Descripcion { get; set; }

    public virtual ICollection<CirbeReal> CirbeReales { get; set; }
}
