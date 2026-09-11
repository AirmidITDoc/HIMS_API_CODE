using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPatIcdcdeH
    {
        public long Hid { get; set; }
        public DateTime? ReqDate { get; set; }
        public DateTime? ReqTime { get; set; }
        public byte? OpIpType { get; set; }
        public long? OpIpId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
