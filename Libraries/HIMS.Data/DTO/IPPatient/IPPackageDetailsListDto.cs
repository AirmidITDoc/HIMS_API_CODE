namespace HIMS.Data.DTO.IPPatient
{
    public class IPPackageDetailsListDto
    {

        public long? ChargesId { get; set; }
        public string? PacakgeServiceName { get; set; }
        public long ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public long? PackageServiceId { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public double? ConcessionPercentage { get; set; }
        public decimal? ConcessionAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public long? PackageId { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public long? DoctorId { get; set; }
        public long? IsPackage { get; set; }
        public string? DoctorName { get; set; }
    }
}
