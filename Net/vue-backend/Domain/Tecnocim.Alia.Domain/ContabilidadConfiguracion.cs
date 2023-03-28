namespace Tecnocim.Alia.Domain;

public class ContabilidadConfiguracion
{
    public int ContabilidadConfiguracionId { get; set; }
    public string Grupo { get; set; } = null;
    public string Concepto { get; set; } = null;
    public int Prioridad { get; set; }
    public string Etiqueta { get; set; } = null;
}