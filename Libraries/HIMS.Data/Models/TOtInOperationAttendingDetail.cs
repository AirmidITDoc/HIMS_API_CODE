using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtInOperationAttendingDetail
    {
        public long OtinOperationAttendingDetId { get; set; }
        public long? OtinOperationId { get; set; }
        public long? DoctorTypeId { get; set; }
        public long? DoctorId { get; set; }
        public int? SeqNo { get; set; }
        public long? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual TOtInOperationHeader? OtinOperation { get; set; }
    }
}
