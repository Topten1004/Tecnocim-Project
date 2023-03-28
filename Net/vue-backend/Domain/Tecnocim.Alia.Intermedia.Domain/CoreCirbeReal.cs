using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class CoreCirbeReal
    {
        public long Id { get; set; }
        public long CirbeId { get; set; }
        public long RealId { get; set; }

        public virtual CoreCirbe Cirbe { get; set; } = null!;
        public virtual EquivalenciasReal Real { get; set; } = null!;
    }
}
