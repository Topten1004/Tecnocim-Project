using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class EquivalenciasEntidad
    {
        public EquivalenciasEntidad()
        {
            CoreCirbes = new HashSet<CoreCirbe>();
            CoreContratos = new HashSet<CoreContrato>();
        }

        public long Id { get; set; }
        public string Codigo { get; set; } = null!;

        public virtual ICollection<CoreCirbe> CoreCirbes { get; set; }
        public virtual ICollection<CoreContrato> CoreContratos { get; set; }
    }
}
