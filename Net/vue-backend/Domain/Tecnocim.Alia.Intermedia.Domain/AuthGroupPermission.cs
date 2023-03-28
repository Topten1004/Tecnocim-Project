using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class AuthGroupPermission
    {
        public long Id { get; set; }
        public int GroupId { get; set; }
        public int PermissionId { get; set; }

        public virtual AuthGroup Group { get; set; } = null!;
        public virtual AuthPermission Permission { get; set; } = null!;
    }
}
