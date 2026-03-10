using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPathDispatchReportHistoryDetail
    {
        public long DispatchDetailId { get; set; }
        public long? DispatchId { get; set; }
        public long? TestId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TPathDispatchReportHistory? Dispatch { get; set; }
    }
}
