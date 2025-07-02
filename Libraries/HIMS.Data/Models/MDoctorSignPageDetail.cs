using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDoctorSignPageDetail
    {
        public long DocSignId { get; set; }
        public long? DoctorId { get; set; }
        public long? PageId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual DoctorMaster? Doctor { get; set; }
    }
}
