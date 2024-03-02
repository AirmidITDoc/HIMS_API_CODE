using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDoctorShareHeaderBack
    {
        public long DocShareId { get; set; }
        public DateTime? Gdate { get; set; }
        public DateTime? Gtime { get; set; }
        public long? DoctorId { get; set; }
        public long? GroupId { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? Percentage { get; set; }
        public decimal? PerAmount { get; set; }
        public double? Tdspercentage { get; set; }
        public decimal? Tdsamount { get; set; }
        public decimal? NetPayableAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte? OpIpType { get; set; }
    }
}
