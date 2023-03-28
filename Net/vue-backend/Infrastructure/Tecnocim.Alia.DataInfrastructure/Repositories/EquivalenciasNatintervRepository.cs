using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EquivalenciasNatintervRepository : EntityFrameworkRepository<SmartdebtContext, EquivalenciasNatinterv>, IEquivalenciasNatintervRepository
{
    public EquivalenciasNatintervRepository(SmartdebtContext context) : base(context)
    {
    }
}
