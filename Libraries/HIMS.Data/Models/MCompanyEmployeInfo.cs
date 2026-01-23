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
    }
}
