namespace HIMS.Data.DTO.Inventory
{
    public class CompanyMasterListDto
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
        public string? CityName { get; set; }
        public string? TariffName { get; set; }
        public string? TypeName { get; set; }
        public decimal DayWiseCredit { get; set; }


    }
    public class ServiceTariffWiseListDto
    {
        public long ServiceId { get; set; }
        public long? GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? ServiceName { get; set; }
        public string? TariffName { get; set; }
        public long? ClassId { get; set; }
        public string? ClassName { get; set; }
        public decimal? ClassRate { get; set; }
        public decimal? DiscountAmount { get; set; }
        public double? DiscountPercentage { get; set; }


    }
    public class ServiceCompanyTariffWiseListDto
    {
        public long ServiceId { get; set; }
        public long? GroupId { get; set; }
        public string? GroupName { get; set; }
        public long? SubGroupId { get; set; }
        public string? SubGroupName { get; set; }
        public string? ServiceShortDesc { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceCode { get; set; }
        public string? ServicePrintName { get; set; }
        public string? TariffName { get; set; }
        public string? CompanyCode { get; set; }
        public string? CompanyServicePrint { get; set; }
        public bool? IsInclusionOrExclusion { get; set; }




    }

}
