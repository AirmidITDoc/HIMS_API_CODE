namespace HIMS.Data.DTO.IPPatient
{
    public class IPBillForRefundListDto
    {
        public long? ChargesId { get; set; }
        public DateTime? ChargesDate { get; set; }
        public long? ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public decimal? NetAmount { get; set; }
        public long? DoctorId { get; set; }
        public decimal? BalAmt { get; set; }
        public string? ChargesDocName { get; set; }
        public string? OPD_IPD_Type { get; set; }
        public long? IsCancelled { get; set; }
        public string? RefundAmount { get; set; }
        public decimal? PreviousRefundAmt { get; set; }
        public decimal? BalanceAmount { get; set; }

    }
}
