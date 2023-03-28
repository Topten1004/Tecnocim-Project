using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class EquivalenciasPersonal
    {
        public EquivalenciasPersonal()
        {
            CoreCirbePersonals = new HashSet<CoreCirbePersonal>();
        }

        public long Id { get; set; }
        public string Tipo { get; set; } = null!;

        public virtual ICollection<CoreCirbePersonal> CoreCirbePersonals { get; set; }
    }
}
