using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtAnesthesiaRecord
    {
        public TOtAnesthesiaRecord()
        {
            TOtAnesthesiaPreOpdiagnoses = new HashSet<TOtAnesthesiaPreOpdiagnosis>();
        }

        public long AnesthesiaId { get; set; }
        public long? OtreservationId { get; set; }
        public DateTime? AnesthesiaDate { get; set; }
        public DateTime? AnesthesiaTime { get; set; }
        public string? AnesthesiaNo { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public DateTime? AnesthesiaStartDate { get; set; }
        public DateTime? AnesthesiaStartTime { get; set; }
        public DateTime? AnesthesiaEndDate { get; set; }
        public DateTime? AnesthesiaEndTime { get; set; }
        public DateTime? RecoveryStartDate { get; set; }
        public DateTime? RecoveryStartTime { get; set; }
        public DateTime? RecoveryEndDate { get; set; }
        public DateTime? RecoveryEndTime { get; set; }
        public long? AnesthesiaType { get; set; }
        public string? AnesthesiaNotes { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TOtAnesthesiaPreOpdiagnosis> TOtAnesthesiaPreOpdiagnoses { get; set; }
    }
}
