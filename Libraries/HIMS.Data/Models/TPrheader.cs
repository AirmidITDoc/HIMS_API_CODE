using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPrheader
    {
        public TPrheader()
        {
            TPrdetails = new HashSet<TPrdetail>();
        }

        public long Prid { get; set; }
        public string? Prno { get; set; }
        public DateTime? Prdate { get; set; }
        public DateTime? Prtime { get; set; }
        public long? UnitId { get; set; }
        public bool? Priority { get; set; }
        public long? StoreId { get; set; }
        public string? Comments { get; set; }
        public bool? Isclosed { get; set; }
        public bool? Isverify { get; set; }
        public long? IsVerifyById { get; set; }
        public DateTime? IsVerifyDateTime { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TPrdetail> TPrdetails { get; set; }
    }
}
