using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EquivalenciasPlazoRepository : EntityFrameworkRepository<SmartdebtContext, EquivalenciasPlazo>, IEquivalenciasPlazoRepository
{
    public EquivalenciasPlazoRepository(SmartdebtContext context) : base(context)
    {
    }
}
