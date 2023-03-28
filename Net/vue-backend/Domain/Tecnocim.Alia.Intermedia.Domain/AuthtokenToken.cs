using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class AuthtokenToken
    {
        public string Key { get; set; } = null!;
        public DateTime Created { get; set; }
        public long UserId { get; set; }

        public virtual CoreUser User { get; set; } = null!;
    }
}
