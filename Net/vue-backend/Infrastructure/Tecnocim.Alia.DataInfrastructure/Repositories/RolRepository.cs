using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class RolRepository : EntityFrameworkRepository<SmartdebtContext, Rol>, IRolRepository
{
    private readonly SmartdebtContext _context;

    public RolRepository(SmartdebtContext context) : base(context)
    {
        _context = context;
    }
}
