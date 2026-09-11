using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPatIcdcdeH
    {
        public long Hid { get; set; }
        public DateTime? ReqDate { get; set; }
        public DateTime? ReqTime { get; set; }
        public byte? OP_IP_Type { get; set; }
        public long? OP_IP_Id { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
