﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ServiceDetail
    {
        public long ServiceDetailId { get; set; }
        public long? ServiceId { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public decimal? ClassRate { get; set; }
        public decimal? DiscountAmount { get; set; }
        public double? DiscountPercentage { get; set; }

        public virtual ServiceMaster? Service { get; set; }
    }
}
