using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Tecnocim.Alia.Domain;
using Tecnocim.Alia.Domain.Repositories;

namespace Tecnocim.Alia.DataInfrastructure.Repositories;

public class ContratoRepository : EntityFrameworkRepository<SmartdebtContext, Contrato>, IContratoRepository
{
    private readonly SmartdebtContext _context;

    public ContratoRepository(SmartdebtContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contrato>> GetPools(int empresaId, string seccion)
    {
        try
        {
            var documento = await _context.Pools.Include(x => x.Documento).AsNoTracking().Where(x => !x.Deleted.HasValue && x.Documento.EmpresaId == empresaId && x.Documento.Origen == "BSS")
                .Select(x => x.Documento).OrderByDescending(x => x.Fecha).FirstOrDefaultAsync();

            if (documento is null)
            {
                return null;
            }

            var pools = await _context.Pools.AsQueryable().Include(x => x.Contrato).ThenInclude(x => x.EquivalenciasProducto).AsNoTracking()
                .Where(x => !x.Deleted.HasValue && x.DocumentoId == documento.DocumentoId).AsAsyncEnumerable().Where(x => GetSeccionFilter(seccion).Compile().Invoke(x)).ToListAsync();

            if (pools is null)
            {
                return null;
            }

            var contratosList = new List<Contrato>();

            var now = DateTime.UtcNow;
            var nowDate = new DateOnly(now.Year, now.Month, now.Day);

            foreach (var pool in pools)
            {
                var contratos = await _context.Contratos
                                .Include(c => c.Cuotas)
                                .Include(c => c.Pools)
                                .Include(c => c.EquivalenciasProducto)
                                .Include(c => c.EquivalenciasMoneda)
                                .Include(c => c.EquivalenciasEntidad).AsNoTracking()
                                .Where(c => !c.Deleted.HasValue && c.Vencimiento > nowDate && c.Pools.Contains(pool))
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
                && x.Contrato != null && x.Contrato.EquivalenciasProducto != null 
                && x.Contrato.EquivalenciasProducto.Tipo != Seccion.compras.ToString()
                && x.Contrato.EquivalenciasProducto.Tipo != Seccion.ventas.ToString();
                break;
            case Seccion.compras:
                poolExpression = x => x.Cuenta.StartsWith("52") && x.Contrato != null && x.Contrato.EquivalenciasProducto != null 
                && x.Contrato.EquivalenciasProducto.Tipo == Seccion.compras.ToString();
                break;
            case Seccion.ventas:
                poolExpression = x => x.Cuenta.StartsWith("52") && x.Contrato != null && x.Contrato.EquivalenciasProducto != null 
                && x.Contrato.EquivalenciasProducto.Tipo == Seccion.ventas.ToString();
                break;
            default:
                throw new ArgumentOutOfRangeException($"La sección {seccion} no está contemplada");
        }

        return poolExpression;
    }
}
