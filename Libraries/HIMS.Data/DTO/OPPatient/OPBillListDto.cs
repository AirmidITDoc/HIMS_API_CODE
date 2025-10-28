namespace HIMS.Data.DTO.OPPatient
{
    public class OPBillListDto
    {
        public string? PbillNo { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime BillTime { get; set; }
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? PatientAge { get; set; }
        public string? MobileNo { get; set; }
        public DateTime VisitDate { get; set; }
        public string? DoctorName { get; set; }
        public string? RefDoctorName { get; set; }
        public string? HospitalName { get; set; }
        public string? PatientType { get; set; }
        public string? TariffName { get; set; }
        public string? CompanyName { get; set; }
        public string? DepartmentName { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public long OPD_IPD_ID { get; set; }
        public long IsCancelled { get; set; }
        public byte OPD_IPD_Type { get; set; }
        public long PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public decimal? CashPay { get; set; }
        public decimal? ChequePay { get; set; }
        public decimal? CardPay { get; set; }
        public decimal? AdvUsedPay { get; set; }
        public decimal? OnlinePay { get; set; }
        public int? PayCount { get; set; }
        public decimal? RefundAmount { get; set; }
        public int? RefundCount { get; set; }
        public string? CashCounterName { get; set; }
        public string? BillNo { get; set; }
    }
}
