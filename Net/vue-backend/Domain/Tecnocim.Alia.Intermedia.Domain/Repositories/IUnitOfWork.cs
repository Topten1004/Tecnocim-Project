namespace Tecnocim.Alia.Intermedia.Domain.Repositories;

public interface IUnitOfWork
{
    ICoreEmpresaRepository CoreEmpresaRepository { get; }

    ICoreExtraccioneRepository CoreExtraccioneRepository { get; }

    ICoreExtraccionesErroreRepository CoreExtraccionesErroreRepository { get; }

    ICoreAnaliticaRepository CoreAnaliticaRepository { get; }
    ICoreCirbeRepository CoreCirbeRepository { get; }
    ICoreContabilidadRepository CoreContabilidadRepository { get; }
    ICoreCrudoRepository CoreCrudoRepository { get; }
    ICoreDocumentoRepository CoreDocumentoRepository { get; }
    ICorePoolRepository CorePoolRepository { get; }
    ICoreRatioRepository CoreRatioRepository { get; }

    void Commit();
    void RejectChanges();
    void Dispose();
}
