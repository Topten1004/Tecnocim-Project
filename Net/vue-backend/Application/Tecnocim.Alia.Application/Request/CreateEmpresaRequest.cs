namespace Tecnocim.Alia.Application.Request;

public class CreateEmpresaRequest
{
    public string CIF { get; set; }
    public string Nombre { get; set; }
    public string Contacto { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public int UsuarioId { get; set; }
}
