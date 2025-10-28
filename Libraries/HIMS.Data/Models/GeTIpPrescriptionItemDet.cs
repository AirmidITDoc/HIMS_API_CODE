using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GeTIpPrescriptionItemDet
    {
        public long? OpIpId { get; set; }
        public long ItemId { get; set; }
        public double? QtyPerDay { get; set; }
        public double? BalQty { get; set; }
        public bool? IsBatchRequired { get; set; }
        public string? ItemName { get; set; }
        public long? IpmedId { get; set; }
    }
}
