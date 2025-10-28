using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIndentHeader
    {
        public TIndentHeader()
        {
            TIndentDetails = new HashSet<TIndentDetail>();
        }

        public long IndentId { get; set; }
        public string? IndentNo { get; set; }
        public DateTime? IndentDate { get; set; }
        public DateTime? IndentTime { get; set; }
        public long? UnitId { get; set; }
        public bool? Priority { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public string? Comments { get; set; }
        public bool? Isclosed { get; set; }
        public bool? Isverify { get; set; }
        public bool? IsInchargeVerify { get; set; }
        public long? IsInchargeVerifyId { get; set; }
        public DateTime? IsInchargeVerifyDate { get; set; }
        public bool? Isdeleted { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public long? Addedby { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TIndentDetail> TIndentDetails { get; set; }
    }
}
