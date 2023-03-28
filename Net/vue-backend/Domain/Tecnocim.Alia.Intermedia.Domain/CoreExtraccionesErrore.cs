using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class CoreExtraccionesErrore
    {
        public long Id { get; set; }
        public string? Mensaje { get; set; }
        public string? Traza { get; set; }
        public int? Bloqueo { get; set; }
        public string? Tabla { get; set; }
        public string? Campo { get; set; }
        public long ExtraccionId { get; set; }

        public virtual CoreExtraccione Extraccion { get; set; } = null!;
    }
}
