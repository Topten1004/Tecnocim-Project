using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class CoreCirbe
    {
        public CoreCirbe()
        {
            CoreCirbePersonals = new HashSet<CoreCirbePersonal>();
            CoreCirbeReals = new HashSet<CoreCirbeReal>();
        }

        public long Id { get; set; }
        public string? Operacion { get; set; }
        public double Dispuesto { get; set; }
        public int Participantes { get; set; }
        public double Importes { get; set; }
        public double Demora { get; set; }
        public long? ContratoId { get; set; }
        public long DocumentoId { get; set; }
        public long EntidadId { get; set; }
        public string MonedaId { get; set; } = null!;
        public string NatIntervId { get; set; } = null!;
        public string PlazoId { get; set; } = null!;
        public string ProductoId { get; set; } = null!;
        public string? SituOperId { get; set; }
        public string? SolColId { get; set; }
        public string TipoId { get; set; } = null!;
        public double Disponible { get; set; }
        public long ExtraccionId { get; set; }

        public virtual CoreContrato? Contrato { get; set; }
        public virtual CoreDocumento Documento { get; set; } = null!;
        public virtual EquivalenciasEntidad Entidad { get; set; } = null!;
        public virtual CoreExtraccione Extraccion { get; set; } = null!;
        public virtual EquivalenciasMonedum Moneda { get; set; } = null!;
        public virtual EquivalenciasNatinterv NatInterv { get; set; } = null!;
        public virtual EquivalenciasPlazo Plazo { get; set; } = null!;
        public virtual EquivalenciasProducto Producto { get; set; } = null!;
        public virtual EquivalenciasSituoper? SituOper { get; set; }
        public virtual EquivalenciasSolcol? SolCol { get; set; }
        public virtual EquivalenciasTipo Tipo { get; set; } = null!;
        public virtual ICollection<CoreCirbePersonal> CoreCirbePersonals { get; set; }
        public virtual ICollection<CoreCirbeReal> CoreCirbeReals { get; set; }
    }
}
