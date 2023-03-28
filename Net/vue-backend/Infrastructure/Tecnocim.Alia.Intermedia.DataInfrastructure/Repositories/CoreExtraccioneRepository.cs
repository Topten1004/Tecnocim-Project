using Tecnocim.Alia.Intermedia.Domain;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;

public class CoreExtraccioneRepository : EntityFrameworkRepository<SmartdebtIntermediaContext, CoreExtraccione>, ICoreExtraccioneRepository
{
    private readonly SmartdebtIntermediaContext _context;

    public CoreExtraccioneRepository(SmartdebtIntermediaContext context) : base(context)
    {
        _context = context;
    }
}
