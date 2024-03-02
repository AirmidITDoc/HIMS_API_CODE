using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCardMemberDetGrp
    {
        public long MemDetId { get; set; }
        public long? MemberHeaderId { get; set; }
        public long? RegId { get; set; }
        public long? GroupId { get; set; }
        public long? SubGroupId { get; set; }
        public float? Percentage { get; set; }
        public byte? IsholderOrIsMember { get; set; }
        public long? RelationshipId { get; set; }
    }
}
