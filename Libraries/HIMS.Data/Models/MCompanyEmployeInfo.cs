using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MCompanyEmployeInfo
    {
        public long ExecutiveId { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public long? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? Designation { get; set; }
        public long? EmpDepartment { get; set; }
        public DateTime? DateOfJoin { get; set; }
        public long? UnitId { get; set; }
        public long? AdharCardNo { get; set; }
        public long? Pfno { get; set; }
        public string? ExperienceYear { get; set; }
        public string? PreviousSalary { get; set; }
        public string? PreviousCompany { get; set; }
        public string? PreviousDesignation { get; set; }
        public bool? IsRepresentative { get; set; }
    }
}
