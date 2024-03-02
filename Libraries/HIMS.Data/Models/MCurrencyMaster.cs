using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MCurrencyMaster
    {
        public long CurrencyId { get; set; }
        public string? CurrencyName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
