using Tecnocim.Alia.Intermedia.Domain;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;

public class CoreDocumentoRepository : EntityFrameworkRepository<SmartdebtIntermediaContext, CoreDocumento>, ICoreDocumentoRepository
{
    private readonly SmartdebtIntermediaContext _context;

    public CoreDocumentoRepository(SmartdebtIntermediaContext context) : base(context)
    {
        _context = context;
    }
}
