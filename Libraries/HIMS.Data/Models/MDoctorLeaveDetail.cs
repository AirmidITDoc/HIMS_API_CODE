using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDoctorLeaveDetail
    {
        public long DocLeaveId { get; set; }
        public long? DoctorId { get; set; }
        public long? LeaveTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? LeaveOption { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual DoctorMaster? Doctor { get; set; }
    }
}
