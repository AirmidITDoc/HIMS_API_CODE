using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwServicePriceList
    {
        public long GroupId { get; set; }
        public string? GroupName { get; set; }
        public long ClassId { get; set; }
        public string? ClassName { get; set; }
        public long ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public decimal? ClassRate { get; set; }
    }
}
