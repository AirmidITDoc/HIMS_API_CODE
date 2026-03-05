using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPrdetail
    {
        public long PrdetId { get; set; }
        public long Prid { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public long? ItemId { get; set; }
        public double? Qty { get; set; }
        public long? PrrequestHeaderId { get; set; }
        public long? PrrequestDetId { get; set; }

        public virtual TPrheader Pr { get; set; } = null!;
    }
}
