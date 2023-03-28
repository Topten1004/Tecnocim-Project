using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EquivalenciasPersonalRepository : EntityFrameworkRepository<SmartdebtContext, EquivalenciasPersonal>, IEquivalenciasPersonalRepository
{
    public EquivalenciasPersonalRepository(SmartdebtContext context) : base(context)
    {
    }
}
