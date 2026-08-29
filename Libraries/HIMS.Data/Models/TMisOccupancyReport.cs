using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMisOccupancyReport
    {
        public long OccupancyId { get; set; }
        public long UnitId { get; set; }
        public long WardId { get; set; }
        public DateTime OccupancyDate { get; set; }
        public DateTime OccupancyTime { get; set; }
        public long TotalBeds { get; set; }
        public long OccupiedBeds { get; set; }
        public long AvailableBeds { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
