using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class CoreCirbePersonal
    {
        public long Id { get; set; }
        public long CirbeId { get; set; }
        public long PersonalId { get; set; }

        public virtual CoreCirbe Cirbe { get; set; } = null!;
        public virtual EquivalenciasPersonal Personal { get; set; } = null!;
    }
}
