using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TLabTransactionHistory
    {
        public long TranHistoryId { get; set; }
        public long? UnitId { get; set; }
        public string? HistoryNo { get; set; }
        public long? PatientId { get; set; }
        public string? TranLabel { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
