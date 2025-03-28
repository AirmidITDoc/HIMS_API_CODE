using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDoctorDepartmentDet
    {
        public long DocDeptId { get; set; }
        public long DoctorId { get; set; }
        public long DepartmentId { get; set; }

        public virtual MDepartmentMaster Department { get; set; } = null!;
        public virtual DoctorMaster Doctor { get; set; } = null!;
    }
}
