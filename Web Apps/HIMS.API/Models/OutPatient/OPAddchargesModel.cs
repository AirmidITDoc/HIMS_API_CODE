namespace HIMS.API.Models.OutPatient
{
    public class OPAddchargesModel
    {
        public long ChargesId { get; set; }
        public string? ChargesDate { get; set; }
        public byte? OPD_IPD_Type { get; set; }
        public long? OPD_IPD_Id { get; set; }
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
        public int? IsCancelledBy { get; set; }
        public string? IsCancelledDate { get; set; }
        public int? IsPathology { get; set; }
        public int? IsRadiology { get; set; }
     
        public int? IsPackage { get; set; }
        public int? IsSelfOrCompanyService { get; set; }
        public long? PackageId { get; set; }
        public string? ChargeTime { get; set; }
        public long? PackageMainChargeID { get; set; }
        public long? ClassId { get; set; }
      
    }
}
