namespace HIMS.Services.Pharmacy
{
    public class BrowseIPPharAdvanceReceiptListDto
    {
        public long RegID { get; set; }
        public string RegNo { get; set; }
        public string PatientName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public byte? IsDischarged { get; set; }
        public byte? IsBillGenerated { get; set; }
        public long AdvanceDetailID { get; set; }
        public string Date { get; set; }
        public long AdvanceId { get; set; }
        public string AdvanceNo { get; set; }
        public long TransactionID { get; set; }
        public long OPD_IPD_Id { get; set; }
        public long OPD_IPD_Type { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal UsedAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public long AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public string Reason { get; set; }
        public long PaymentId { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentTime { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public DateTime ChequeDate { get; set; }
        public decimal CardPayAmount { get; set; }
        public string CardNo { get; set; }
        public string CardBankName { get; set; }
        public DateTime CardDate { get; set; }
        public long TransactionType { get; set; }
        public string UserName { get; set; }
        public decimal OnlineAmount { get; set; }
        public decimal TDSAmount { get; set; }
        public decimal WFAmount { get; set; }





    }
}
