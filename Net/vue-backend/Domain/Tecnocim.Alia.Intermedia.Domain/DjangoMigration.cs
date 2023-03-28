using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class DjangoMigration
    {
        public long Id { get; set; }
        public string App { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime Applied { get; set; }
    }
}
