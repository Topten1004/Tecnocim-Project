using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class EquivalenciasProducto
    {
        public EquivalenciasProducto()
        {
            CoreCirbes = new HashSet<CoreCirbe>();
            CoreContratos = new HashSet<CoreContrato>();
        }

        public string Tipo { get; set; } = null!;

        public virtual ICollection<CoreCirbe> CoreCirbes { get; set; }
        public virtual ICollection<CoreContrato> CoreContratos { get; set; }
    }
}
