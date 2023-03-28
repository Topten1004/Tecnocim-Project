namespace Tecnocim.Alia.Application.Dtos
{
    public class UsuarioListadoDto
    {
        public UsuarioListadoDto()
        {
            Empresas = new HashSet<EmpresaDto>();
        }

        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public int RolId { get; set; }
        public string NombreRol { get; set; }
        public string PuestoTrabajo { get; set; }

        public ICollection<EmpresaDto> Empresas { get; set; }
    }
}
