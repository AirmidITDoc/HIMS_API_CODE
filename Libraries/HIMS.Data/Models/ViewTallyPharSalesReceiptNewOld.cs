using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ViewTallyPharSalesReceiptNewOld
    {
        public DateTime? Date { get; set; }
        public string? DebitLedger { get; set; }
        public decimal? Amount { get; set; }
        public string CreditLedger { get; set; } = null!;
        public long? BillNo { get; set; }
        public string? ReceiptNo { get; set; }
        public long? StrId { get; set; }
    }
}
