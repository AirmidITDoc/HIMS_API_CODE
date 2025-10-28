using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCardMemberDet
    {
        public long MemDetId { get; set; }
        public long? MemberHeaderId { get; set; }
        public long? RegId { get; set; }
        public long? Opper { get; set; }
        public long? Ipper { get; set; }
        public long? PathPer { get; set; }
        public long? RadPer { get; set; }
        public long? PharPer { get; set; }
        public byte? IsholderOrIsMember { get; set; }
        public long? RelationshipId { get; set; }
    }
}
