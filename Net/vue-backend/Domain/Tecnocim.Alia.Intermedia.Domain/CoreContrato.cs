using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class CoreContrato
    {
        public CoreContrato()
        {
            CoreCirbes = new HashSet<CoreCirbe>();
        }

        public long Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Vencimiento { get; set; }
        public int Carencia { get; set; }
        public double Precio { get; set; }
        public double Limite { get; set; }
        public int Periodificacion { get; set; }
        public int? Valoracion { get; set; }
        public string? Notas { get; set; }
        public bool? Digitalizada { get; set; }
        public long EntidadId { get; set; }
        public string MonedaId { get; set; } = null!;
        public string ProductoId { get; set; } = null!;
        public bool? Minimis { get; set; }

        public virtual EquivalenciasEntidad Entidad { get; set; } = null!;
        public virtual EquivalenciasMonedum Moneda { get; set; } = null!;
        public virtual EquivalenciasProducto Producto { get; set; } = null!;
        public virtual ICollection<CoreCirbe> CoreCirbes { get; set; }
    }
}
