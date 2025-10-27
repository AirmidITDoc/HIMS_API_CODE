using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MMarketingHospitalMaster
    {
        public long HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public string? HospitalAddress { get; set; }
        public string? Location { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Radius { get; set; }
        public long? CityId { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactMobileNo { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorMobileNo { get; set; }
        public long? BedCategoryId { get; set; }
        public string? BedCategoryName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
