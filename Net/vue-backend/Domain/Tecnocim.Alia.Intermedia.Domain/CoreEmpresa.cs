using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class CoreEmpresa
    {
        public long Id { get; set; }
        public string Cif { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Contacto { get; set; }
        public long? Telefono { get; set; }
        public string? Email { get; set; }
        public string? ConfigFile { get; set; }
    }
}
