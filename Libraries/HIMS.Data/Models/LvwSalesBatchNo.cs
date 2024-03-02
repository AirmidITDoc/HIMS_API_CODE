using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwSalesBatchNo
    {
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public long? OpIpId { get; set; }
        public long? StoreId { get; set; }
        public long? OpIpType { get; set; }
    }
}
