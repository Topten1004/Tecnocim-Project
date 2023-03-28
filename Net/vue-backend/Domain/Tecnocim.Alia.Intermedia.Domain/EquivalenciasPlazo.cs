using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class EquivalenciasPlazo
    {
        public EquivalenciasPlazo()
        {
            CoreCirbes = new HashSet<CoreCirbe>();
        }

        public string Tipo { get; set; } = null!;

        public virtual ICollection<CoreCirbe> CoreCirbes { get; set; }
    }
}
