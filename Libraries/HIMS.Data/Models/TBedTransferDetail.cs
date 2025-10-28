using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TBedTransferDetail
    {
        public long TransferId { get; set; }
        public long? AdmissionId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? FromTime { get; set; }
        public long? FromWardId { get; set; }
        public long? FromBedId { get; set; }
        public long? FromClassId { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? ToTime { get; set; }
        public long? ToWardId { get; set; }
        public long? ToBedId { get; set; }
        public long? ToClassId { get; set; }
        public string? Remark { get; set; }
        public long? AddedBy { get; set; }
        public long? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
    }
}
