using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MTaxNatureMaster
    {
        public long Id { get; set; }
        public string? TaxNature { get; set; }
        public long? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
