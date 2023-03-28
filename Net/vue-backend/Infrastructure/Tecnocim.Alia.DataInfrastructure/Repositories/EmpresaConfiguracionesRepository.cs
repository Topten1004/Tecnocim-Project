using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EmpresaConfiguracionesRepository : EntityFrameworkRepository<SmartdebtContext, EmpresaConfiguraciones>, IEmpresaConfiguracionesRepository
{
    public EmpresaConfiguracionesRepository(SmartdebtContext context) : base(context)
	{
    }
}
