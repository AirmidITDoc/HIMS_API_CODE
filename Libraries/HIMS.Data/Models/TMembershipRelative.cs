using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMembershipRelative
    {
        public long RelativeId { get; set; }
        public long MembershipId { get; set; }
        public long PrefixId { get; set; }
        public long RelationId { get; set; }
        public string? RelativeName { get; set; }
        public long? RelationNameId { get; set; }
        public string? RelativeMobile { get; set; }
        public string? RelativeAddress { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? PrefixName { get; set; }

        public virtual TMembershipRegistration Membership { get; set; } = null!;
    }
}
