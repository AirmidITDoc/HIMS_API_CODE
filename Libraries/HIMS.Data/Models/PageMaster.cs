using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class PageMaster
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string PageName { get; set; } = null!;
        public string PageCode { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsShowMenu { get; set; }
        public string? TableNames { get; set; }

        public virtual ModuleMaster Module { get; set; } = null!;
    }
}
