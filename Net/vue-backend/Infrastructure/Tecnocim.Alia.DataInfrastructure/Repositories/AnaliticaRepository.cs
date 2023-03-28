using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class AnaliticaRepository : EntityFrameworkRepository<SmartdebtContext, Analitica>, IAnaliticaRepository
{
    public AnaliticaRepository(SmartdebtContext context) : base(context)
    {
    }
}
