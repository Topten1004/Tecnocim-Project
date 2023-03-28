using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class ContabilidadRepository : EntityFrameworkRepository<SmartdebtContext, Contabilidad>, IContabilidadRepository
{
    public ContabilidadRepository(SmartdebtContext context) : base(context)
    {
    }
}
