using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TLabPatientAddress
    {
        public long AddressId { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public long? UnitId { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
