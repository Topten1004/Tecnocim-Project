namespace Tecnocim.Alia.Domain;

public class Cirbe : AuditableEntity
{
    public Cirbe()
    {
        CirbePersonales = new HashSet<CirbePersonal>();
        CirbeReales = new HashSet<CirbeReal>();
    }

    public int CirbeId { get; set; }
    public string Operacion { get; set; }
    public decimal Dispuesto { get; set; }
    public int Participantes { get; set; }
    public decimal Importes { get; set; }
    public decimal Demora { get; set; }
    public decimal Disponible { get; set; }

    public long? ContratoId { get; set; }
    public virtual Contrato Contrato { get; set; }
    public long DocumentoId { get; set; }
    public virtual Documento Documento { get; set; }
    public int EquivalenciasEntidadId { get; set; }
    public virtual EquivalenciasEntidad EquivalenciasEntidad { get; set; }
    public int EquivalenciasMonedaId { get; set; }
    public virtual EquivalenciasMoneda EquivalenciasMoneda { get; set; }
    public int EquivalenciasNatintervId { get; set; }
    public virtual EquivalenciasNatinterv EquivalenciasNatinterv { get; set; }
    public int EquivalenciasPlazoId { get; set; }
    public virtual EquivalenciasPlazo EquivalenciasPlazo { get; set; }
    public int EquivalenciasProductoId { get; set; }
    public virtual EquivalenciasProducto EquivalenciasProducto { get; set; }
    public int? EquivalenciasSituoperId { get; set; }
    public virtual EquivalenciasSituoper EquivalenciasSituoper { get; set; }
    public int? EquivalenciasSolcolId { get; set; }
    public virtual EquivalenciasSolcol EquivalenciasSolcol { get; set; }
    public int EquivalenciasTipoId { get; set; }
    public virtual EquivalenciasTipo EquivalenciasTipo { get; set; }

    public virtual ICollection<CirbePersonal> CirbePersonales { get; set; }
    public virtual ICollection<CirbeReal> CirbeReales { get; set; }
}
