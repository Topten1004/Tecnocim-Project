namespace Tecnocim.Alia.Domain.Repositories;

public interface IEmpresaRepository : IRepository<Empresa>
{
    Task<IEnumerable<Usuario>> GetUsuariosByEmpresaId(int empresaId);
}
