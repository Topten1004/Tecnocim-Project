using Tecnocim.Alia.Intermedia.Domain;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;

public class CoreContabilidadRepository : EntityFrameworkRepository<SmartdebtIntermediaContext, CoreContabilidad>, ICoreContabilidadRepository
{
    private readonly SmartdebtIntermediaContext _context;

    public CoreContabilidadRepository(SmartdebtIntermediaContext context) : base(context)
    {
        _context = context;
    }
}
