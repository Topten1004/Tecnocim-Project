namespace Tecnocim.Alia.Domain.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<IEnumerable<Empresa>> GetEmpresasByUsuarioId(int usuarioId);
}