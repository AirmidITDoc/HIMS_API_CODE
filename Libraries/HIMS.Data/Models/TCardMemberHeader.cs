using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCardMemberHeader
    {
        public long MemberId { get; set; }
        public DateTime? Mdate { get; set; }
        public DateTime? Mtime { get; set; }
        public string? MemberCardNo { get; set; }
        public DateTime? MemberCardValidateDate { get; set; }
        public long? CardCategory { get; set; }
        public long? IsAddedby { get; set; }
        public long? IsUpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
    }
}
