using System;
using System.Collections.Generic;

namespace Tecnocim.Alia.Intermedia.Domain
{
    public partial class AuthPermission
    {
        public AuthPermission()
        {
            AuthGroupPermissions = new HashSet<AuthGroupPermission>();
            CoreUserUserPermissions = new HashSet<CoreUserUserPermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ContentTypeId { get; set; }
        public string Codename { get; set; } = null!;

        public virtual DjangoContentType ContentType { get; set; } = null!;
        public virtual ICollection<AuthGroupPermission> AuthGroupPermissions { get; set; }
        public virtual ICollection<CoreUserUserPermission> CoreUserUserPermissions { get; set; }
    }
}
