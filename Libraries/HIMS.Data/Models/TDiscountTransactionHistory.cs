using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDiscountTransactionHistory
    {
        public long DiscTranId { get; set; }
        public long? BillNo { get; set; }
        public decimal? DiscountAmt { get; set; }
        public decimal? CompDiscountAmt { get; set; }
        public long? ConcessionReasonId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
