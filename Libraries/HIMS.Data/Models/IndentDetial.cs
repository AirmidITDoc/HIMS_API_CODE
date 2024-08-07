using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class IndentDetial
    {
        public long IndentId { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }
        public double? Qty { get; set; }
    }
}
