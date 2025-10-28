using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TableSubhash
    {
        public long SalesId { get; set; }
        public string? SalesNo { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? PayTmamount { get; set; }
        public long? OpIpId { get; set; }
        public long? CashCounterId { get; set; }
    }
}
