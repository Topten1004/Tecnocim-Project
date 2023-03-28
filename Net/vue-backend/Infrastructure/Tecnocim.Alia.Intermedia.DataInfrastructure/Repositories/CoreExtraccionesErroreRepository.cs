using Tecnocim.Alia.Intermedia.Domain;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;

public class CoreExtraccionesErroreRepository : EntityFrameworkRepository<SmartdebtIntermediaContext, CoreExtraccionesErrore>, ICoreExtraccionesErroreRepository
{
    private readonly SmartdebtIntermediaContext _context;

    public CoreExtraccionesErroreRepository(SmartdebtIntermediaContext context) : base(context)
    {
        _context = context;
    }
}
