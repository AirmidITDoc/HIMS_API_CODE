using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwBillDateWise
    {
        public long? RegId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long BillNo { get; set; }
        public decimal? TotalAmt { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmt { get; set; }
        public DateTime? BillDate { get; set; }
    }
}
