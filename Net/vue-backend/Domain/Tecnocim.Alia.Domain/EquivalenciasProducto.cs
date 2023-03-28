namespace Tecnocim.Alia.Domain;

public class EquivalenciasProducto
{
    public EquivalenciasProducto()
    {
        Contratos = new HashSet<Contrato>();
        Cirbes = new HashSet<Cirbe>();
    }

    public int Id { get; set; }
    public string Tipo { get; set; }
    public string Descripcion { get; set; }
    public string Subtipo { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; }
    public virtual ICollection<Cirbe> Cirbes { get; set; }
}
