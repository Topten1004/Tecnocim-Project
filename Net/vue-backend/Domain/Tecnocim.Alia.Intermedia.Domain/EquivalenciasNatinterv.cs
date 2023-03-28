using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class EquivalenciasNatinterv
    {
        public EquivalenciasNatinterv()
        {
            CoreCirbes = new HashSet<CoreCirbe>();
        }

        public string Tipo { get; set; } = null!;

        public virtual ICollection<CoreCirbe> CoreCirbes { get; set; }
    }
}
