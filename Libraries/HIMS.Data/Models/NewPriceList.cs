using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class NewPriceList
    {
        public long? ServiceDetailId { get; set; }
        public string? ServiceName { get; set; }
        public string? ClassName { get; set; }
        public decimal? ClassRate { get; set; }
        public string? GroupName { get; set; }
        public decimal? NewPrice { get; set; }
    }
}
