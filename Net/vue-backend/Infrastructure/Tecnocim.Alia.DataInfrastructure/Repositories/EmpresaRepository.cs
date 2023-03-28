using Microsoft.EntityFrameworkCore;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class EmpresaRepository : EntityFrameworkRepository<SmartdebtContext, Empresa>, IEmpresaRepository
{
    private readonly SmartdebtContext _context;

    public EmpresaRepository(SmartdebtContext context) : base(context)
	{
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetUsuariosByEmpresaId(int empresaId)
    {
        var usuarios = await _context.Empresas.Where(us => us.EmpresaId == empresaId && !us.Deleted.HasValue).SelectMany(u => u.Usuarios).ToListAsync();

        return usuarios;
    }
}
