using Tecnocim.Alia.Intermedia.Domain;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;

public class CoreCirbeRepository : EntityFrameworkRepository<SmartdebtIntermediaContext, CoreCirbe>, ICoreCirbeRepository
{
    private readonly SmartdebtIntermediaContext _context;

    public CoreCirbeRepository(SmartdebtIntermediaContext context) : base(context)
    {
        _context = context;
    }
}
