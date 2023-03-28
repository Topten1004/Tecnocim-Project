using Tecnocim.Alia.Intermedia.Domain;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;

public class CorePoolRepository : EntityFrameworkRepository<SmartdebtIntermediaContext, CorePool>, ICorePoolRepository
{
    private readonly SmartdebtIntermediaContext _context;

    public CorePoolRepository(SmartdebtIntermediaContext context) : base(context)
    {
        _context = context;
    }
}
