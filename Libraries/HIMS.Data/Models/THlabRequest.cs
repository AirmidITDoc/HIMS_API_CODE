using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class THlabRequest
    {
        public long RequestId { get; set; }
        public DateTime? ReqDate { get; set; }
        public DateTime? ReqTime { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? IsAddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public DateTime? IsCancelledTime { get; set; }
        public byte? IsType { get; set; }
        public bool? IsOnFileTest { get; set; }
    }
}
