using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpPrescription
    {
        public long IppreId { get; set; }
        public long? IpmedId { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpdIpdType { get; set; }
        public DateTime? Pdate { get; set; }
        public DateTime? Ptime { get; set; }
        public long? ClassId { get; set; }
        public long? GenericId { get; set; }
        public long? DrugId { get; set; }
        public long? DoseId { get; set; }
        public long? Days { get; set; }
        public double? QtyPerDay { get; set; }
        public double? TotalQty { get; set; }
        public string? Remark { get; set; }
        public bool? IsClosed { get; set; }
        public long? IsAddBy { get; set; }
        public long? StoreId { get; set; }
        public long? WardId { get; set; }
    }
}
