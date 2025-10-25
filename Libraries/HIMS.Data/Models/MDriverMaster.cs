using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDriverMaster
    {
        public long DriverId { get; set; }
        public string DriverName { get; set; } = null!;
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? JoinDate { get; set; }
        public long? Experience { get; set; }
        public string? LicenceNo { get; set; }
    }
}
