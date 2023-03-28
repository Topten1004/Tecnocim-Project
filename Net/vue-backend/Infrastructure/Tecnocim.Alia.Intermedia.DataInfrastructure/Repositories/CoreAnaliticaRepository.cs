using Tecnocim.Alia.Intermedia.Domain;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;

public class CoreAnaliticaRepository : EntityFrameworkRepository<SmartdebtIntermediaContext, CoreAnalitica>, ICoreAnaliticaRepository
{
    private readonly SmartdebtIntermediaContext _context;

    public CoreAnaliticaRepository(SmartdebtIntermediaContext context) : base(context)
    {
        _context = context;
    }
}
