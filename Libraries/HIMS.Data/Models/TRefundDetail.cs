using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TRefundDetail
    {
        public long RefundDetId { get; set; }
        public long? RefundId { get; set; }
        public long? ServiceId { get; set; }
        public decimal? ServiceAmount { get; set; }
        public decimal? RefundAmount { get; set; }
        public long? DoctorId { get; set; }
        public string? Remark { get; set; }
        public long? AddBy { get; set; }
        public long? ChargesId { get; set; }
        public decimal? HospitalAmount { get; set; }
        public decimal? DoctorAmount { get; set; }
    }
}
