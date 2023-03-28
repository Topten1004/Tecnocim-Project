namespace Tecnocim.Alia.Application.Dtos;

public class EmpresaConfiguracionesDto
{
    public int EmpresaConfiguracionesId { get; set; }
    public string Fecha { get; set; }
    public bool PorDefecto { get; set; }
    public string FicheroConfiguracion { get; set; } = null;
}
