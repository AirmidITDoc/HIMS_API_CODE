using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SalesReturnAmt
    {
        public DateTime? Mdate { get; set; }
        public decimal? Amount { get; set; }
        public string Debit { get; set; } = null!;
        public string? Credit { get; set; }
        public int SrNo { get; set; }
        public long AdmissionId { get; set; }
        public long? StoreId { get; set; }
    }
}
