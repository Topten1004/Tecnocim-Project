using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EquivalenciasTipoRepository : EntityFrameworkRepository<SmartdebtContext, EquivalenciasTipo>, IEquivalenciasTipoRepository
{
    public EquivalenciasTipoRepository(SmartdebtContext context) : base(context)
    {
    }
}