using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EquivalenciasProductoRepository : EntityFrameworkRepository<SmartdebtContext, EquivalenciasProducto>, IEquivalenciasProductoRepository
{
    public EquivalenciasProductoRepository(SmartdebtContext context) : base(context)
    {
    }
}
