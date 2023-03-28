using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class DocumentoRepository : EntityFrameworkRepository<SmartdebtContext, Documento>, IDocumentoRepository
{
    private readonly SmartdebtContext _context;

    public DocumentoRepository(SmartdebtContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Documento>> GetByEmpresaIdAndOrigenAndStatus(int empresaId, string? origen, bool? status)
    {
        return await _context.Documentos.AsNoTracking().Where(GetFilter(empresaId, origen, status)).ToListAsync();
    }

    private static Expression<Func<Documento, bool>> GetFilter(int empresaId, string? origen, bool? status)
    {
        var predicate = PredicateBuilder.New<Documento>(x => x.EmpresaId == empresaId && !x.Deleted.HasValue);

        if (!string.IsNullOrEmpty(origen))
        {
            predicate = predicate.And(y => y.Origen == origen);
        }

        if (status.HasValue)
        {
            predicate = predicate.And(z => z.Status == status.Value);
        }

        return predicate;
    }
}
