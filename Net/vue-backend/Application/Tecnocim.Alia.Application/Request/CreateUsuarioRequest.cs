namespace Tecnocim.Alia.Application.Request;

public class CreateUsuarioRequest
{
    public string Nombre { get; set; } = null;
    public string Apellidos { get; set; } = null;
    public string Email { get; set; } = null;
    public string Password { get; set; } = null;
    public string PuestoTrabajo { get; set; } = null;
    public int RolId { get; set; }
}
