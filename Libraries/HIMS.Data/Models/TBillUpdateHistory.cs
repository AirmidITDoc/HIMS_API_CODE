using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TBillUpdateHistory
    {
        public long HistoryId { get; set; }
        public long? BillNo { get; set; }
        public string? TranType { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
