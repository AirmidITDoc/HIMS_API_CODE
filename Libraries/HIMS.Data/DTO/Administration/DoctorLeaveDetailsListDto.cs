using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public  class DoctorLeaveDetailsListDto
    {
        public long DocLeaveId { get; set; }
        public long DoctorId { get; set; }
        public long? LeaveTypeId { get; set; }
        public string? LeaveType { get; set; }

        public string? Reason { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? LeaveOption { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }


    }
}
