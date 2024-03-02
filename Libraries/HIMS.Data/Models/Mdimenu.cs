using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class Mdimenu
    {
        public long MenuId { get; set; }
        public string? MenuName { get; set; }
        public long? ParentMenuId { get; set; }
        public long? MenuLevel { get; set; }
        public string? MenuKey { get; set; }
    }
}
