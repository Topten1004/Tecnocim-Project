using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class CirbeRepository : EntityFrameworkRepository<SmartdebtContext, Cirbe>, ICirbeRepository
{
    public CirbeRepository(SmartdebtContext context) : base(context)
    {
    }
}
