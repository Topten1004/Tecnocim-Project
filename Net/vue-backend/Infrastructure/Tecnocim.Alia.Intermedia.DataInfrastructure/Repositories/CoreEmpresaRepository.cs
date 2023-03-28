using Tecnocim.Alia.Intermedia.Domain;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;

public class CoreEmpresaRepository : EntityFrameworkRepository<SmartdebtIntermediaContext, CoreEmpresa>, ICoreEmpresaRepository
{
    private readonly SmartdebtIntermediaContext _context;

    public CoreEmpresaRepository(SmartdebtIntermediaContext context) : base(context)
    {
        _context = context;
    }
}
