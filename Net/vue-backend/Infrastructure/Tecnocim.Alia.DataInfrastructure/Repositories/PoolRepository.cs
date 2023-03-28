using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Tecnocim.Alia.DataInfrastructure.Comparers;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class PoolRepository : EntityFrameworkRepository<SmartdebtContext, Pool>, IPoolRepository
{
    private readonly SmartdebtContext _context;

    public PoolRepository(SmartdebtContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contrato>> GetPools(int empresaId, string seccion)
    {
        try
        {
            var documento = await _context.Pools.Include(x => x.Documento).AsNoTracking().Where(x => x.Documento.EmpresaId == empresaId && x.Documento.Origen == "BSS")
                .Select(x => x.Documento).OrderByDescending(x => x.Fecha).FirstOrDefaultAsync();

            if (documento is null)
            {
                return null;
            }

            var pools = await _context.Pools.AsQueryable().Include(x => x.Contrato).ThenInclude(x => x.EquivalenciasProducto).AsNoTracking()
                .Where(x => x.DocumentoId == documento.DocumentoId).AsAsyncEnumerable().Where(x => GetSeccionFilter(seccion).Compile().Invoke(x)).ToListAsync();

            if (pools is null)
            {
                return null;
            }

            var contratosList = new List<Contrato>();

            var now = DateTime.UtcNow;
            var nowDate = new DateOnly(now.Year, now.Month, now.Day);

            foreach (var pool in pools)
            {
                var contratos = await _context.Contratos.Include(c => c.Pools)
                                .Include(c => c.EquivalenciasProducto)
                                .Include(c => c.EquivalenciasEntidad).AsNoTracking()
                                .Where(c => c.Vencimiento > nowDate && c.Pools.Contains(pool))
                                .ToListAsync();
                contratosList.AddRange(contratos);
            } 

            return contratosList;
        }
        catch(Exception exception)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Pool>> GetByEmpresaId(int empresaId)
    {
        return await _context.Pools.Include(x => x.Documento).Where(x => !x.Deleted.HasValue && 
        x.ContratoId == null && x.Documento != null && x.Documento.EmpresaId == empresaId)
            .AsAsyncEnumerable()
            .OrderBy(x => x.Cuenta) // , new CuentaPendientesComparer())
            .ToListAsync();
    }

    public async Task<IEnumerable<Pool>> GetPendientesTipo52ByEmpresaId(int empresaId)
    {
        return await _context.Pools.Include(x => x.Documento).Where(x => !x.Deleted.HasValue &&
        x.ContratoId == null && x.Documento != null && x.Documento.EmpresaId == empresaId && x.Cuenta.StartsWith("52"))
            .AsAsyncEnumerable()
            .OrderBy(x => x.Cuenta) //, new CuentaPendientesComparer())
            .ToListAsync();
    }

    public async Task<IEnumerable<Pool>> GetEstadosByEmpresaId(int empresaId)
    {
        var pools = await _context.Pools.Include(x => x.Documento).Where(x => !x.Deleted.HasValue &&
         x.Documento != null && x.Documento.EmpresaId == empresaId && x.Documento.Origen == "BSS")
            .AsAsyncEnumerable()
            .OrderByDescending(x => x.Documento.Fecha)
            .ToListAsync();

        if (pools.Any())
        {
            var documentoId = pools.First().Documento?.DocumentoId;

            return pools.Where(x => x.DocumentoId == documentoId);
        }

        return Enumerable.Empty<Pool>();
    }

    public async Task<IEnumerable<Pool>> GetTipo52ByEmpresaId(int empresaId)
    {
        return await _context.Pools.Include(x => x.Documento).Where(x => !x.Deleted.HasValue &&
        x.Documento != null && x.Documento.EmpresaId == empresaId && x.Cuenta.StartsWith("52"))
            .AsAsyncEnumerable()
            .OrderBy(x => x.Cuenta) //, new CuentaPendientesComparer())
            .ToListAsync();
    }

    private static Expression<Func<Pool, bool>> GetSeccionFilter(string seccion)
    {
        if (!Enum.TryParse(seccion, out Seccion seccionEnum))
        {
            throw new ArgumentOutOfRangeException($"La sección {seccion} no está contemplada");
        }

        Expression<Func<Pool, bool>> poolExpression = (Expression<Func<Pool, bool>>)Expression.Lambda(Expression.Constant(true), Expression.Parameter(typeof(Pool)));

        switch (seccionEnum)
        {
            case Seccion.largoplazo:
                poolExpression = x => x.Cuenta.StartsWith("170") || x.Cuenta.StartsWith("171");
                break;
            case Seccion.creditos:
                poolExpression = x => x.Cuenta.StartsWith("52")
                && x.Contrato.EquivalenciasProducto.Tipo != Seccion.compras.ToString()
                && x.Contrato.EquivalenciasProducto.Tipo != Seccion.ventas.ToString();
                break;
            case Seccion.compras:
                poolExpression = x => x.Cuenta.StartsWith("52") && x.Contrato.EquivalenciasProducto.Tipo == Seccion.compras.ToString();
                break;
            case Seccion.ventas:
                poolExpression = x => x.Cuenta.StartsWith("52") && x.Contrato.EquivalenciasProducto.Tipo == Seccion.ventas.ToString();
                break;
            default:
                throw new ArgumentOutOfRangeException($"La sección {seccion} no está contemplada");
        }

        return poolExpression;
    }
}
