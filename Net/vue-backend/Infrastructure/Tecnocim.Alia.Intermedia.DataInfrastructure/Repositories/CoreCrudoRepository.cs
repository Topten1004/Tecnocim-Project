using Tecnocim.Alia.Intermedia.Domain;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;

public class CoreCrudoRepository : EntityFrameworkRepository<SmartdebtIntermediaContext, CoreCrudo>, ICoreCrudoRepository
{
    private readonly SmartdebtIntermediaContext _context;

    public CoreCrudoRepository(SmartdebtIntermediaContext context) : base(context)
    {
        _context = context;
    }
}
