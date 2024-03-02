using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ClassMaster
    {
        public long ClassId { get; set; }
        public string? ClassName { get; set; }
        public bool? IsActive { get; set; }
    }
}
