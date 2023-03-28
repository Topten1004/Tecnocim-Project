using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class EquivalenciasReal
    {
        public EquivalenciasReal()
        {
            CoreCirbeReals = new HashSet<CoreCirbeReal>();
        }

        public long Id { get; set; }
        public string Tipo { get; set; } = null!;

        public virtual ICollection<CoreCirbeReal> CoreCirbeReals { get; set; }
    }
}
