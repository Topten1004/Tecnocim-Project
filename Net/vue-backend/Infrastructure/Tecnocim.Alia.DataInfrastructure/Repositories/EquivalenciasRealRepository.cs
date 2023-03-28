using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EquivalenciasRealRepository : EntityFrameworkRepository<SmartdebtContext, EquivalenciasReal>, IEquivalenciasRealRepository
{
    public EquivalenciasRealRepository(SmartdebtContext context) : base(context)
    {
    }
}
