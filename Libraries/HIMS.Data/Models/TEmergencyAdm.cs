using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TEmergencyAdm
    {
        public long EmgId { get; set; }
        public long? RegId { get; set; }
        public DateTime? EmgDate { get; set; }
        public DateTime? EmgTime { get; set; }
        public string? SeqNo { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public int? AgeYear { get; set; }
        public int? AgeMonth { get; set; }
        public int? AgeDay { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public string? Comment { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
