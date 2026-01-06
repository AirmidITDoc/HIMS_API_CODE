using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtPreOperationSurgeryDetail
    {
        public long OtpreOperationSurgeryDetId { get; set; }
        public long? OtpreOperationId { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public long? SurgeryId { get; set; }
        public string? SurgeryPart { get; set; }
        public DateTime? SurgeryFromTime { get; set; }
        public DateTime? SurgeryEndTime { get; set; }
        public double? SurgeryDuration { get; set; }
        public string? IsPrimary { get; set; }
        public long? SurgeonId { get; set; }
        public long? AnesthetistId { get; set; }
        public int? SeqNo { get; set; }
        public long? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual TOtPreOperationHeader? OtpreOperation { get; set; }
    }
}
