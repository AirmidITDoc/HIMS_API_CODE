using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIndentDetail
    {
        public long IndentDetailsId { get; set; }
        public long? IndentId { get; set; }
        public long? ItemId { get; set; }
        public double? Qty { get; set; }
        public bool? IsClosed { get; set; }
        public long? IndQty { get; set; }
        public long? IssQty { get; set; }
    }
}
