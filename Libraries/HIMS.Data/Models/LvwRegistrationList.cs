using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwRegistrationList
    {
        public long RegId { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? RegTime { get; set; }
        public long PrefixId { get; set; }
        public string? PrefixName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Age { get; set; }
        public long GenderId { get; set; }
        public string? GenderName { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? PatientName { get; set; }
        public string? Rdate { get; set; }
        public string? RegTimeDate { get; set; }
    }
}
