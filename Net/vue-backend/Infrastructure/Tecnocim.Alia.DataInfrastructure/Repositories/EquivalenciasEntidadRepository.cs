using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EquivalenciasEntidadRepository : EntityFrameworkRepository<SmartdebtContext, EquivalenciasEntidad>, IEquivalenciasEntidadRepository
{
    public EquivalenciasEntidadRepository(SmartdebtContext context) : base(context)
    {
    }
}
