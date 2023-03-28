using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class CoreDocumento
    {
        public CoreDocumento()
        {
            CoreAnaliticas = new HashSet<CoreAnalitica>();
            CoreCirbes = new HashSet<CoreCirbe>();
            CoreContabilidads = new HashSet<CoreContabilidad>();
            CoreCrudos = new HashSet<CoreCrudo>();
            CorePools = new HashSet<CorePool>();
            CoreRatios = new HashSet<CoreRatio>();
        }

        public long Id { get; set; }
        public string Documento { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public long EmpresaId { get; set; }
        public string Origen { get; set; } = null!;
        public bool Status { get; set; }
        public long ExtraccionId { get; set; }

        public virtual ICollection<CoreAnalitica> CoreAnaliticas { get; set; }
        public virtual ICollection<CoreCirbe> CoreCirbes { get; set; }
        public virtual ICollection<CoreContabilidad> CoreContabilidads { get; set; }
        public virtual ICollection<CoreCrudo> CoreCrudos { get; set; }
        public virtual ICollection<CorePool> CorePools { get; set; }
        public virtual ICollection<CoreRatio> CoreRatios { get; set; }
    }
}
