using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class RoleTemplateDetail
    {
        public long RoleDetailId { get; set; }
        public long? RoleId { get; set; }
        public string? MenuKey { get; set; }
        public bool? IsActive { get; set; }
    }
}
