using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TLabAppointment
    {
        public long LabAppId { get; set; }
        public DateTime? AppDate { get; set; }
        public DateTime? AppTime { get; set; }
        public string? SeqNo { get; set; }
        public long? PrefixId { get; set; }
        public long? GenderId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? MobileNo { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public string? Address { get; set; }
        public long? DoctorId { get; set; }
        public long? CategoryId { get; set; }
        public DateTime? LabAppDate { get; set; }
        public DateTime? LabAppTime { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? LabPatRegId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
