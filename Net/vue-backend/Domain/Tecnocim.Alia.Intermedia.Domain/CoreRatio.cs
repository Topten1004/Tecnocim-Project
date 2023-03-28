using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class CoreRatio
    {
        public long Id { get; set; }
        public string Concepto { get; set; } = null!;
        public double Magnitud { get; set; }
        public long DocumentoId { get; set; }
        public long ExtraccionId { get; set; }

        public virtual CoreDocumento Documento { get; set; } = null!;
        public virtual CoreExtraccione Extraccion { get; set; } = null!;
    }
}
