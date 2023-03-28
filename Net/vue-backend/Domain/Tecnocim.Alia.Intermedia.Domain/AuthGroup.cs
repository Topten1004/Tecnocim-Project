using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class AuthGroup
    {
        public AuthGroup()
        {
            AuthGroupPermissions = new HashSet<AuthGroupPermission>();
            CoreUserGroups = new HashSet<CoreUserGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<AuthGroupPermission> AuthGroupPermissions { get; set; }
        public virtual ICollection<CoreUserGroup> CoreUserGroups { get; set; }
    }
}
