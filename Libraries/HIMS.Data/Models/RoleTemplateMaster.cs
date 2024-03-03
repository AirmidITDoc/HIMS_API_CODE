using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class RoleTemplateMaster
    {
        public RoleTemplateMaster()
        {
            PermissionMasters = new HashSet<PermissionMaster>();
        }

        public long RoleId { get; set; }
        public string? RoleName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<PermissionMaster> PermissionMasters { get; set; }
    }
}
