using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMembershipEmrgency
    {
        public long EmrgencyId { get; set; }
        public long? MembershipId { get; set; }
        public long? PrefixId { get; set; }
        public string? EmrgencyName { get; set; }
        public string? EmrgencyMobile { get; set; }
        public string? EmrgencyAddress { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TMembershipRegistration? Membership { get; set; }
    }
}
