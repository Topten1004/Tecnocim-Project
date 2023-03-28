namespace Tecnocim.Alia.Domain.Repositories;

public interface IContratoRepository : IRepository<Contrato>
{
    Task<IEnumerable<Contrato>> GetPools(int empresaId, string seccion);
}
