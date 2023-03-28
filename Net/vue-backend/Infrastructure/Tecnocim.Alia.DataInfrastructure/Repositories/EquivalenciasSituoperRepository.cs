using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EquivalenciasSituoperRepository : EntityFrameworkRepository<SmartdebtContext, EquivalenciasSituoper>, IEquivalenciasSituoperRepository
{
    public EquivalenciasSituoperRepository(SmartdebtContext context) : base(context)
    {
    }
}
