using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class CoreUser
    {
        public CoreUser()
        {
            CoreUserGroups = new HashSet<CoreUserGroup>();
            CoreUserUserPermissions = new HashSet<CoreUserUserPermission>();
            DjangoAdminLogs = new HashSet<DjangoAdminLog>();
        }

        public long Id { get; set; }
        public string Password { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
        public bool IsSuperuser { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsStaff { get; set; }

        public virtual AuthtokenToken AuthtokenToken { get; set; } = null!;
        public virtual ICollection<CoreUserGroup> CoreUserGroups { get; set; }
        public virtual ICollection<CoreUserUserPermission> CoreUserUserPermissions { get; set; }
        public virtual ICollection<DjangoAdminLog> DjangoAdminLogs { get; set; }
    }
}
