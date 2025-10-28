namespace HIMS.Data.DTO.OPPatient
{
    public class PackageDetListDto
    {
        public string? PacakgeServiceName { get; set; }
        public string? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public string? PackageServiceId { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public double? Price { get; set; }
        public long? PackageId { get; set; }
        public int DoctorId { get; set; }
        public string? DoctorName { get; set; }


    }
}
