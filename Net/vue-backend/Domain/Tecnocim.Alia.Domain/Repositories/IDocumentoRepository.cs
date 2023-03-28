namespace Tecnocim.Alia.Domain.Repositories;

public interface IDocumentoRepository : IRepository<Documento>
{
    Task<IEnumerable<Documento>> GetByEmpresaIdAndOrigenAndStatus(int empresaId, string? origen, bool? status);
}