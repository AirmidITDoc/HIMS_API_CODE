using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDlabRequest
    {
        public long ReqDetId { get; set; }
        public long? RequestId { get; set; }
        public long? ServiceId { get; set; }
        public decimal? Price { get; set; }
        public bool? IsStatus { get; set; }
        public long? AddedBillingId { get; set; }
        public DateTime? AddedByDate { get; set; }
        public DateTime? AddedByTime { get; set; }
        public long? CharId { get; set; }
        public bool? IsTestCompted { get; set; }
        public bool? IsOnFileTest { get; set; }
    }
}
