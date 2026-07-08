using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class VwPatientRegistrationInfo
    {
        public long RegId { get; set; }
        public string? RegNo { get; set; }
        public string? RegPrefix { get; set; }
        public string? RegDate { get; set; }
        public string? RegTime { get; set; }
        public string? PatientName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? AgeGender { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public string? DateofBirth { get; set; }
        public string? Age { get; set; }
        public string? GenderName { get; set; }
        public string? AadharCardNo { get; set; }
        public string? EmailId { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
