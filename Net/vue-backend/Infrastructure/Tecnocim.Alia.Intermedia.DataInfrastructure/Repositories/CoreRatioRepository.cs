using Tecnocim.Alia.Intermedia.Domain;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;

public class CoreRatioRepository : EntityFrameworkRepository<SmartdebtIntermediaContext, CoreRatio>, ICoreRatioRepository
{
    private readonly SmartdebtIntermediaContext _context;

    public CoreRatioRepository(SmartdebtIntermediaContext context) : base(context)
    {
        _context = context;
    }
}
