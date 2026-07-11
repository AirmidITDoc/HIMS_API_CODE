using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMembershipChild
    {
        public long ChildId { get; set; }
        public long MembershipId { get; set; }
        public long PrefixId { get; set; }
        public string ChildName { get; set; } = null!;
        public string? ChildMobile { get; set; }
        public string? ChildAddress { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? PrefixName { get; set; }

        public virtual TMembershipRegistration Membership { get; set; } = null!;
    }
}
