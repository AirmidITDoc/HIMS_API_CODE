using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LCurstkItemWiseBalQty
    {
        public long? ItemId { get; set; }
        public long? StoreId { get; set; }
        public double? BalQty { get; set; }
    }
}
