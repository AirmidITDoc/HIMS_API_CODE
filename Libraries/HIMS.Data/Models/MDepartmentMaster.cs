using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDepartmentMaster
    {
        public MDepartmentMaster()
        {
            MDoctorDepartmentDets = new HashSet<MDoctorDepartmentDet>();
        }

        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<MDoctorDepartmentDet> MDoctorDepartmentDets { get; set; }
    }
}
