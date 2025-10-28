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
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public string? CityName { get; set; }
    }
}
