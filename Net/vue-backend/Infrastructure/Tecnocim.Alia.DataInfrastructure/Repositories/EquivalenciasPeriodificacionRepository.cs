using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EquivalenciasPeriodificacionRepository : EntityFrameworkRepository<SmartdebtContext, EquivalenciasPeriodificacion>, IEquivalenciasPeriodificacionRepository
{
    public EquivalenciasPeriodificacionRepository(SmartdebtContext context) : base(context)
    {
    }
}
