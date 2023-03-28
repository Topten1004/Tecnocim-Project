using Microsoft.EntityFrameworkCore;
using Tecnocim.Alia.Intermedia.DataInfrastructure.Repositories;
using Tecnocim.Alia.Intermedia.Domain.Repositories;

namespace Tecnocim.Alia.Intermedia.DataInfrastructure;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly SmartdebtIntermediaContext _context;

    public UnitOfWork(SmartdebtIntermediaContext context)
    {
        _context = context;
    }

    
    
    public ICoreEmpresaRepository CoreEmpresaRepository => new CoreEmpresaRepository(_context);

    public ICoreExtraccioneRepository CoreExtraccioneRepository => new CoreExtraccioneRepository(_context);

    public ICoreExtraccionesErroreRepository CoreExtraccionesErroreRepository => new CoreExtraccionesErroreRepository(_context);

    public ICoreAnaliticaRepository CoreAnaliticaRepository => new CoreAnaliticaRepository(_context);
    public ICoreCirbeRepository CoreCirbeRepository => new CoreCirbeRepository(_context);
    public ICoreContabilidadRepository CoreContabilidadRepository => new CoreContabilidadRepository(_context);
    public ICoreCrudoRepository CoreCrudoRepository => new CoreCrudoRepository(_context);
    public ICoreDocumentoRepository CoreDocumentoRepository => new CoreDocumentoRepository(_context);
    public ICorePoolRepository CorePoolRepository => new CorePoolRepository(_context);
    public ICoreRatioRepository CoreRatioRepository => new CoreRatioRepository(_context);


    public void Commit()
    {
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
