namespace HIMS.Data.DTO.IPPatient
{
    public class IPBillListDto
    {
        public long BillNo { get; set; }
        public string OPDIPDID { get; set; }
        public DateTime? BillDate { get; set; }
        public string? PbillNo { get; set; }
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? PatientAge { get; set; }
        public string? MobileNo { get; set; }
        public string? AadharCardNo { get; set; }
        public string? AdmissionDate { get; set; }
        public long? RegId { get; set; }
        public string? DoctorName { get; set; }
        public string? RefDoctorName { get; set; }
        public string? PatientType { get; set; }
        public string? RoomName { get; set; }
        public string? BedName { get; set; }
        public DateTime? BillTime { get; set; }
        public string? IPDNo { get; set; }
        public string? HospitalName { get; set; }
        public string? TariffName { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? AdmissionTime { get; set; }
        public string? DepartmentName { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? CompDiscAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public string? AdvanceUsedAmount { get; set; }
        public long? IsCancelled { get; set; }
        public long OPDIPDType { get; set; }
        public long PaidAmt { get; set; }
        public decimal BalanceAmt { get; set; }
        public decimal CashPay { get; set; }
        public decimal ChequePay { get; set; }
        public decimal CardPay { get; set; }
        public decimal AdvUsedPay { get; set; }
        public decimal? OnlinePay { get; set; }
        public long PayCount { get; set; }
        public decimal? RefundAmount { get; set; }
        public long RefundCount { get; set; }
        public string? CashCounterName { get; set; }
        public long? InterimOrFinal { get; set; }
        public string? BillPrefix { get; set; }
        public string? BillMonth { get; set; }
        public string? BillYear { get; set; }
        public string? PrintBillNo { get; set; }
        public string? UserName { get; set; }
        public long? PatientTypeId { get; set; }
        public decimal? GovtApprovedAmt { get; set; }
        public decimal? CompanyApprovedAmt { get; set; }
        public string? GovtCompanyName { get; set; }
        public string? CompanyApprovedName { get; set; }
        public long? GovtCompanyId { get; set; }
        public string? GovtRefNo { get; set; }
        public long? CompanyApprovedId { get; set; }
        public string? CompRefNo { get; set; }

    }
}
