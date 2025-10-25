using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDoctorPayoutProcessDetail
    {
        public long DoctorPayoutDetId { get; set; }
        public long? DoctorPayoutId { get; set; }
        public long? DoctorId { get; set; }
        public long? ChargeId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TDoctorPayoutProcessHeader? DoctorPayout { get; set; }
    }
}
