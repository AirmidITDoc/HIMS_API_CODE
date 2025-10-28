using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class CompanyMaster
    {
        public long CompanyId { get; set; }
        public long? CompTypeId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyShortName { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public string? PinNo { get; set; }
        public string? PhoneNo { get; set; }
        public string? FaxNo { get; set; }
        public long? TraiffId { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactNumber { get; set; }
        public string? EmailId { get; set; }
        public string? Website { get; set; }
        public long? PaymodeOfPayId { get; set; }
        public int? CreditDays { get; set; }
        public string? PanNo { get; set; }
        public string? Tanno { get; set; }
        public string? Gstin { get; set; }
        public double? AdminCharges { get; set; }
        public string? LoginWebsiteUser { get; set; }
        public string? LoginWebsitePassword { get; set; }
        public bool? IsSubCompany { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
