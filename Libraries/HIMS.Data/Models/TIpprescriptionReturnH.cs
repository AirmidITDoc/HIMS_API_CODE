using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpprescriptionReturnH
    {
        public long PresReId { get; set; }
        public string? PresNo { get; set; }
        public DateTime? PresDate { get; set; }
        public DateTime? PresTime { get; set; }
        public long? ToStoreId { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? Addedby { get; set; }
        public long? Isdeleted { get; set; }
        public bool? Isclosed { get; set; }
    }
}
