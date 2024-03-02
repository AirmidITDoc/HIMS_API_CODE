using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDepartmentMaster
    {
        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
