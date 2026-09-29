using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpEmrVital
    {
        public long IpemrVitalId { get; set; }
        public long IpemrId { get; set; }
        public long Opipid { get; set; }
        public long Opiptype { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? Bmi { get; set; }
        public string? Bsl { get; set; }
        public string? Spo2 { get; set; }
        public string? Temp { get; set; }
        public string? Pulse { get; set; }
        public string? Bp { get; set; }
        public long Createdby { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TIpEmrhistory Ipemr { get; set; } = null!;
    }
}
