using Tecnocim.Alia.Domain.Enums;

namespace Tecnocim.Alia.Domain;

public class Interpretacion
{
    public int InterpretacionId { get; set; }
    public string Concepto { get; set; }
    public string Nombre { get; set; }
    public string ColorPositivo { get; set; }
    public string ColorNegativo { get; set; }
    public string IconoPositivo { get; set; }
    public string IconoNegativo { get; set; }
    public TipoInterpretacion Tipo { get; set; }
}
