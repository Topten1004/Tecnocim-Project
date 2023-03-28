using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class CoreExtraccione
    {
        public CoreExtraccione()
        {
            CoreAnaliticas = new HashSet<CoreAnalitica>();
            CoreCirbes = new HashSet<CoreCirbe>();
            CoreContabilidads = new HashSet<CoreContabilidad>();
            CoreCrudos = new HashSet<CoreCrudo>();
            CoreExtraccionesErrores = new HashSet<CoreExtraccionesErrore>();
            CoreRatios = new HashSet<CoreRatio>();
        }

        public long Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string Ruta { get; set; } = null!;
        public string? RutaUnmerged { get; set; }
        public DateTime Fechahora { get; set; }
        public string? Resultado { get; set; }

        public virtual ICollection<CoreAnalitica> CoreAnaliticas { get; set; }
        public virtual ICollection<CoreCirbe> CoreCirbes { get; set; }
        public virtual ICollection<CoreContabilidad> CoreContabilidads { get; set; }
        public virtual ICollection<CoreCrudo> CoreCrudos { get; set; }
        public virtual ICollection<CoreExtraccionesErrore> CoreExtraccionesErrores { get; set; }
        public virtual ICollection<CoreRatio> CoreRatios { get; set; }
    }
}
