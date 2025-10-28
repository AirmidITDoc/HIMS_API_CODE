namespace HIMS.Data.DTO.IPPatient
{
    public class PathRadServiceListDto
    {
        public string? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public decimal Price { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public bool? IsActive { get; set; }
        public long? TariffId { get; set; }

        public bool? CreditedtoDoctor { get; set; }


    }
}
