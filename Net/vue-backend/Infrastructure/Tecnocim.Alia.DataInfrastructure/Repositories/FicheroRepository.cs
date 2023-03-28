using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class FicheroRepository : EntityFrameworkRepository<SmartdebtContext, Fichero>, IFicheroRepository
{
    public FicheroRepository(SmartdebtContext context) : base(context)
    {
    }
}
