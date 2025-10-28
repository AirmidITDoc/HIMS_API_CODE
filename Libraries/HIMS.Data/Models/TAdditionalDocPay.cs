using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TAdditionalDocPay
    {
        public long TranId { get; set; }
        public DateTime? TranDate { get; set; }
        public DateTime? TranTime { get; set; }
        public long? PbillNo { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? BillDate { get; set; }
        public string? ServiceName { get; set; }
        public decimal? Price { get; set; }
        public float? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DocAmount { get; set; }
        public long? IsProcess { get; set; }
        public long? DocId { get; set; }
        public string? PatientName { get; set; }
    }
}
