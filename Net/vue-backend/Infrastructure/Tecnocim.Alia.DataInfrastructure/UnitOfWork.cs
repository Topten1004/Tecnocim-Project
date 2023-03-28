using Microsoft.EntityFrameworkCore;
using Tecnocim.Alia.DataInfrastructure.Repositories;
using Tecnocim.Alia.Domain.Repositories;
using Tecnocim.Alia.DataInfrastructure.Extensions;

namespace Tecnocim.Alia.DataInfrastructure;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly SmartdebtContext _context;

    public UnitOfWork(SmartdebtContext context)
    {
        _context = context;
    }

    public IUsuarioRepository UsuarioRepository => new UsuarioRepository(_context);
    public IEmpresaRepository EmpresaRepository => new EmpresaRepository(_context);
    public IRolRepository RolRepository => new RolRepository(_context);
    public IEmpresaConfiguracionesRepository EmpresaConfiguracionesRepository => new EmpresaConfiguracionesRepository(_context);
    public IEquivalenciasEntidadRepository EquivalenciasEntidadRepository => new EquivalenciasEntidadRepository(_context);
    public IEquivalenciasMonedaRepository EquivalenciasMonedaRepository => new EquivalenciasMonedaRepository(_context);
    public IEquivalenciasNatintervRepository EquivalenciasNatintervRepository => new EquivalenciasNatintervRepository(_context);
    public IEquivalenciasPersonalRepository EquivalenciasPersonalRepository => new EquivalenciasPersonalRepository(_context);
    public IEquivalenciasPlazoRepository EquivalenciasPlazoRepository => new EquivalenciasPlazoRepository(_context);    
    public IEquivalenciasProductoRepository EquivalenciasProductoRepository => new EquivalenciasProductoRepository(_context);
    public IEquivalenciasRealRepository EquivalenciasRealRepository => new EquivalenciasRealRepository(_context);
    public IEquivalenciasSituoperRepository EquivalenciasSituoperRepository => new EquivalenciasSituoperRepository(_context);
    public IEquivalenciasSolcolRepository EquivalenciasSolcolRepository => new EquivalenciasSolcolRepository(_context);
    public IEquivalenciasTipoRepository EquivalenciasTipoRepository => new EquivalenciasTipoRepository(_context);
    public IEquivalenciasPeriodificacionRepository EquivalenciasPeriodificacionRepository => new EquivalenciasPeriodificacionRepository(_context);
    public IContratoRepository ContratoRepository => new ContratoRepository(_context);
    public IPoolRepository PoolRepository => new PoolRepository(_context);
    public IDocumentoRepository DocumentoRepository => new DocumentoRepository(_context);
    public ICirbeRepository CirbeRepository => new CirbeRepository(_context);
    public IFicheroRepository FicheroRepository => new FicheroRepository(_context);
    public IRatioRepository RatioRepository => new RatioRepository(_context);
    public ICuotaRepository CuotaRepository => new CuotaRepository(_context);
    public IInterpretacionRepository InterpretacionRepository => new InterpretacionRepository(_context);
    public IAnaliticaRepository AnaliticaRepository => new AnaliticaRepository(_context);
    public IContabilidadRepository ContabilidadRepository => new ContabilidadRepository(_context);
    public IContabilidadConfiguracionRepository ContabilidadConfiguracionRepository => new ContabilidadConfiguracionRepository(_context);


    public void Commit()
    {
        _context.ChangeTracker.SetAuditProperties();
        _context.SaveChanges();
    }

    public void RejectChanges()
    {
        foreach (var entry in _context.ChangeTracker.Entries()
            .Where(e => e.State != EntityState.Unchanged))
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
