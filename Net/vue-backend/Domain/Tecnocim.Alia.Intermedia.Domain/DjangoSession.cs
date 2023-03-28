using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class DjangoSession
    {
        public string SessionKey { get; set; } = null!;
        public string SessionData { get; set; } = null!;
        public DateTime ExpireDate { get; set; }
    }
}
