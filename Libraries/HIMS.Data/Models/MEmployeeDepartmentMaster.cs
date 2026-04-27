using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MEmployeeDepartmentMaster
    {
        public long EmpDepartmentId { get; set; }
        public string? EmpDepartmentName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
