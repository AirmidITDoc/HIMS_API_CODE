using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIndentHeader
    {
        public long IndentId { get; set; }
        public string? IndentNo { get; set; }
        public DateTime? IndentDate { get; set; }
        public DateTime? IndentTime { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public long? Addedby { get; set; }
        public long? Isdeleted { get; set; }
        public bool? Isverify { get; set; }
        public bool? Isclosed { get; set; }
        public bool? IsInchargeVerify { get; set; }
        public long? IsInchargeVerifyId { get; set; }
        public DateTime? IsInchargeVerifyDate { get; set; }
    }
}
