using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class CoreUserGroup
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int GroupId { get; set; }

        public virtual AuthGroup Group { get; set; } = null!;
        public virtual CoreUser User { get; set; } = null!;
    }
}
