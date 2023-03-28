namespace Tecnocim.Alia.Domain.Repositories;

public interface IPoolRepository : IRepository<Pool>
{
    Task<IEnumerable<Pool>> GetByEmpresaId(int empresaId);
    Task<IEnumerable<Pool>> GetPendientesTipo52ByEmpresaId(int empresaId);
    Task<IEnumerable<Pool>> GetTipo52ByEmpresaId(int empresaId);
    Task<IEnumerable<Pool>> GetEstadosByEmpresaId(int empresaId);
}
