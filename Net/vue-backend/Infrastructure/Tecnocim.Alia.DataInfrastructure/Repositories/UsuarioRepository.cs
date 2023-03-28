using Microsoft.EntityFrameworkCore;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class UsuarioRepository : EntityFrameworkRepository<SmartdebtContext, Usuario>, IUsuarioRepository
{
    private readonly SmartdebtContext _context;

    public UsuarioRepository(SmartdebtContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Empresa>> GetEmpresasByUsuarioId(int usuarioId)
    {
        var empresas = await _context.Usuarios.Where(us => us.UsuarioId == usuarioId && !us.Deleted.HasValue).SelectMany(u => u.Empresas).ToListAsync();

        return empresas;
    }
}
