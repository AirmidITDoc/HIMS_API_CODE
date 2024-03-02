using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TServiceDetail20170504
    {
        public long ServiceDetailId { get; set; }
        public long? ServiceId { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public decimal? ClassRate { get; set; }
    }
}
