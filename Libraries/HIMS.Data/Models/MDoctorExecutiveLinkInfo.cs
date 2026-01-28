using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDoctorExecutiveLinkInfo
    {
        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public long? EmployeId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
