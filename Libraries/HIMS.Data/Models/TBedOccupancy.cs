using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TBedOccupancy
    {
        public long BedOccId { get; set; }
        public DateTime? BedOccupancyDate { get; set; }
        public DateTime? BedOccupancyTime { get; set; }
        public int? TotalBedCount { get; set; }
        public int? TotalOccupiedCount { get; set; }
        public int? TotalAvailableCount { get; set; }
        public double? OccupancyAvg { get; set; }
        public long? WardId { get; set; }
    }
}
