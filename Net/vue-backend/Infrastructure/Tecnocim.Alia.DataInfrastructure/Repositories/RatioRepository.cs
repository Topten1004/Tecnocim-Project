using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class RatioRepository : EntityFrameworkRepository<SmartdebtContext, Ratio>, IRatioRepository
{
    public RatioRepository(SmartdebtContext context) : base(context)
    {
    }
}
