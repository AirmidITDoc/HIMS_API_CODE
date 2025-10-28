using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwBillDiffQuery
    {
        public string? PbillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public byte? OpdIpdType { get; set; }
        public double? TotalChargesAmt { get; set; }
        public decimal? NetChargesAmt { get; set; }
        public double? BillConsAmount { get; set; }
        public decimal? BillAmount { get; set; }
        public double? DiffBalAmount { get; set; }
        public decimal? CardPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public decimal? CashPayAmount { get; set; }
    }
}
