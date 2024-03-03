using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class PermissionMaster
    {
        public int Id { get; set; }
        public long RoleId { get; set; }
        public int MenuId { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsView { get; set; }
        public bool IsDelete { get; set; }

        public virtual PageMaster Menu { get; set; } = null!;
        public virtual RoleTemplateMaster Role { get; set; } = null!;
    }
}
