namespace HIMS.Data.DTO.Administration
{
    public class PaymentModeDto
    {
        public string? PatientName { get; set; }
        public long PaymentId { get; set; }
        public string PayDate { get; set; }
        public string? RegNo { get; set; }
        public decimal? TotalAmt { get; set; }
        public decimal? PaidAmount { get; set; }
        public string? PaymentTime { get; set; }
        public string? PbillNo { get; set; }
        public string? ReceiptNo { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public string? ChequeDate { get; set; }
        public decimal? CardPayAmount { get; set; }
        public string? CardNo { get; set; }
        public string? CardBankName { get; set; }
        public string? CardDate { get; set; }
        public string? UserName { get; set; }
        public string? Remark { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public decimal? NEFTPayAmount { get; set; }






    }
}
