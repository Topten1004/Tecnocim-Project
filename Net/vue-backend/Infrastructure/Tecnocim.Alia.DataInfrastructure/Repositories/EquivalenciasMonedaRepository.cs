using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EquivalenciasMonedaRepository : EntityFrameworkRepository<SmartdebtContext, EquivalenciasMoneda>, IEquivalenciasMonedaRepository
{
    public EquivalenciasMonedaRepository(SmartdebtContext context) : base(context)
    {
    }
}
