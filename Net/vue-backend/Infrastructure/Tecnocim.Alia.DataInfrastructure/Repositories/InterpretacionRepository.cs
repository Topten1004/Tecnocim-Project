using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class InterpretacionRepository : EntityFrameworkRepository<SmartdebtContext, Interpretacion>, IInterpretacionRepository
{
    public InterpretacionRepository(SmartdebtContext context) : base(context)
    {
    }
}
