using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class RoleMaster
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
