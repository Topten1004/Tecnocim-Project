namespace Tecnocim.Alia.Domain.Repositories;

public interface IUnitOfWork
{
    IUsuarioRepository UsuarioRepository { get; }
    IEmpresaRepository EmpresaRepository { get; }
    IRolRepository RolRepository { get; }
    IEmpresaConfiguracionesRepository EmpresaConfiguracionesRepository { get; } 

    IEquivalenciasEntidadRepository EquivalenciasEntidadRepository { get; } 
    IEquivalenciasMonedaRepository EquivalenciasMonedaRepository { get; }   
    IEquivalenciasNatintervRepository EquivalenciasNatintervRepository { get; } 
    IEquivalenciasPersonalRepository EquivalenciasPersonalRepository { get; }   
    IEquivalenciasPlazoRepository EquivalenciasPlazoRepository { get; } 
    IEquivalenciasProductoRepository EquivalenciasProductoRepository { get; }
    IEquivalenciasRealRepository EquivalenciasRealRepository { get; }   
    IEquivalenciasSituoperRepository EquivalenciasSituoperRepository { get; }   
    IEquivalenciasSolcolRepository EquivalenciasSolcolRepository { get; }
    IEquivalenciasTipoRepository EquivalenciasTipoRepository { get; }
    IEquivalenciasPeriodificacionRepository EquivalenciasPeriodificacionRepository { get; }
    IContratoRepository ContratoRepository { get; }
    IPoolRepository PoolRepository { get; }
    IDocumentoRepository DocumentoRepository { get; }
    ICirbeRepository CirbeRepository { get; }
    IFicheroRepository FicheroRepository { get; }
    IRatioRepository RatioRepository { get; }
    ICuotaRepository CuotaRepository { get; }
    IInterpretacionRepository InterpretacionRepository { get; }
    IAnaliticaRepository AnaliticaRepository { get; }
    IContabilidadRepository ContabilidadRepository { get; }
    IContabilidadConfiguracionRepository ContabilidadConfiguracionRepository { get; }

    void Commit();
    void RejectChanges();
    void Dispose();
}
