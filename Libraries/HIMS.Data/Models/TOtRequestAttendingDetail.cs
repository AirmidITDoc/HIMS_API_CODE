using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtRequestAttendingDetail
    {
        public long OtrequestAttendingDetId { get; set; }
        public long? OtrequestId { get; set; }
        public long? DoctorTypeId { get; set; }
        public long? DoctorId { get; set; }
        public int? SeqNo { get; set; }

        public virtual TOtRequestHeader? Otrequest { get; set; }
    }
}
