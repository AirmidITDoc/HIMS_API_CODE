namespace HIMS.Data.DTO.IPPatient
{
    public class PackageDetailsListDto
    {

        public string? PacakgeServiceName { get; set; }
        public long? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public long? PackageServiceId { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public double? Price { get; set; }
        public long? PackageId { get; set; }
        public long? Expr1 { get; set; }
    }
}
