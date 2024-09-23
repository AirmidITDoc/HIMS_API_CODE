namespace HIMS.API.Models.OutPatient
{
    public class OPAddchargesModel
    {
        public long ChargeID { get; set; }
        public DateTime? ChargesDate { get; set; }

        public string? ChargeTime { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public long? ServiceId { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public double? ConcessionPercentage { get; set; }
        public decimal? ConcessionAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public long? DoctorId { get; set; }
        public double? DocPercentage { get; set; }
        public double? DocAmt { get; set; }
        public double? HospitalAmt { get; set; }
        public bool? IsGenerated { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }

        public long? IsPackage { get; set; }
        public long? IsSelfOrCompanyService { get; set; }
        public long? PackageId { get; set; }
        public long? PackageMainChargeID { get; set; }
        public long? ClassId { get; set; }



    }
}
