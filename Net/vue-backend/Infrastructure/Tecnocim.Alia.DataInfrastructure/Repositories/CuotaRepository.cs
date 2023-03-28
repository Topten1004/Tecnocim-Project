using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class CuotaRepository : EntityFrameworkRepository<SmartdebtContext, Cuota>, ICuotaRepository
{
    public CuotaRepository(SmartdebtContext context) : base(context)
    {
    }
}
