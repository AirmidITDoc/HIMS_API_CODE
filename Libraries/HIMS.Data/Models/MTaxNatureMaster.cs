using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MTaxNatureMaster
    {
        public long Id { get; set; }
        public string? TaxNature { get; set; }
        public long? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
