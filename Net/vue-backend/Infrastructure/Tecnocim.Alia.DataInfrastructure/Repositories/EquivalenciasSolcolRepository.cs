using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EquivalenciasSolcolRepository : EntityFrameworkRepository<SmartdebtContext, EquivalenciasSolcol>, IEquivalenciasSolcolRepository
{
    public EquivalenciasSolcolRepository(SmartdebtContext context) : base(context)
    {
    }
}
