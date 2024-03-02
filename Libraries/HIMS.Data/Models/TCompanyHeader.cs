using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCompanyHeader
    {
        public long CompanyId { get; set; }
        public long? PatientCompanyId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Tdsamount { get; set; }
        public decimal? PayAmount { get; set; }
        public decimal? WrfAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public DateTime? SettlementDate { get; set; }
        public DateTime? SettlementTime { get; set; }
        public string? ReceiptNo { get; set; }
    }
}
