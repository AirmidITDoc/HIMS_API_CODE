using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwDoctorMasterList
    {
        public long DoctorId { get; set; }
        public long PrefixId { get; set; }
        public string? PrefixName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Pin { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public long? GenderId { get; set; }
        public string? GenderName { get; set; }
        public string? Education { get; set; }
        public bool? IsConsultant { get; set; }
        public bool? IsRefDoc { get; set; }
        public bool? IsActive { get; set; }
        public string DoctorType { get; set; } = null!;
        public long DoctorTypeId { get; set; }
        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}
