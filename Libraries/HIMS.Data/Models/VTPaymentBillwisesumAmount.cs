using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class VTPaymentBillwisesumAmount
    {
        public long? BillNo { get; set; }
        public long? UnitId { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? CardAmount { get; set; }
        public decimal? ChequeAmount { get; set; }
        public decimal? OnlineAmount { get; set; }
        public string? UpitranNo { get; set; }
    }
}
