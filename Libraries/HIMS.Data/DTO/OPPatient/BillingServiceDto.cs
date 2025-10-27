namespace HIMS.Data.DTO.OPPatient
{
    public class BillingServiceDto
    {
        public long ServiceId { get; set; }
        public long? GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? ServiceShortDesc { get; set; }
        public string? ServiceName { get; set; }
        public double? Price { get; set; }
        public bool? IsEditable { get; set; }
        public bool? CreditedtoDoctor { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public bool? IsActive { get; set; }
        public long? PrintOrder { get; set; }
        public long? TariffId { get; set; }
        public string? TariffName { get; set; }
        public long? IsPackage { get; set; }
        public bool? IsEmergency { get; set; }
        public decimal? EmgAmt { get; set; }
        public double? EmgPer { get; set; }
        public long? DoctorId { get; set; }
        public bool? IsDocEditable { get; set; }
        public bool? IsServiceTaxApplicable { get; set; }
        public int? IsApplicableFor { get; set; }
        public long? SubGroupid { get; set; }
        public DateTime? EmgStartTime { get; set; }
        public DateTime? EmgEndTime { get; set; }
        public bool? IsPathOutSource { get; set; }
        public bool? IsRadOutSource { get; set; }
        public bool? IsDiscount { get; set; }
        public bool? IsProcedure { get; set; }
        public long? PackageTotalDays { get; set; }
        public long? PackageIcudays { get; set; }
        public decimal? PackageMedicineAmount { get; set; }
        public decimal? PackageConsumableAmount { get; set; }

    }

    public class BillingServiceListDto
    {
        public long ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public decimal? Price { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public long? TariffId { get; set; }
    }
    public class BillingServiceNewDto
    {
        public List<BillingServiceNew> Data { get; set; }
        public List<BillingServiceColumns> Columns { get; set; }
        public int TariffId { get; set; }
    }

    public class BillingServiceNew
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public List<BillingServiceColumnValue> ColumnValues { get; set; }
    }
    public class BillingServiceColumns
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
    }
    public class BillingServiceColumnValue
    {
        public int ClassId { get; set; }
        public int ClassValue { get; set; }
    }
}
