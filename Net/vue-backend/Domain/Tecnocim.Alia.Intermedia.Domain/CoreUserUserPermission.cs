using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class CoreUserUserPermission
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int PermissionId { get; set; }

        public virtual AuthPermission Permission { get; set; } = null!;
        public virtual CoreUser User { get; set; } = null!;
    }
}
