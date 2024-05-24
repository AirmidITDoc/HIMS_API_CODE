using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class PageMaster
    {
        public int Id { get; set; }
        public int? ModuleId { get; set; }
        public string? PageName { get; set; }
        public string? PageCode { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsShowMenu { get; set; }
        public string? TableNames { get; set; }
    }
}
