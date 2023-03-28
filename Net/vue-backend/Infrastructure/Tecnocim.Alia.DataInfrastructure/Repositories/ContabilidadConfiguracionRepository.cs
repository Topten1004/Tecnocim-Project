using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class ContabilidadConfiguracionRepository : EntityFrameworkRepository<SmartdebtContext, ContabilidadConfiguracion>, IContabilidadConfiguracionRepository
{
    public ContabilidadConfiguracionRepository(SmartdebtContext context) : base(context)
    {
    }
}
