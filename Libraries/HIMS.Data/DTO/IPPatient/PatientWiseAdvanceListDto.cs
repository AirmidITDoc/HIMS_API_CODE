namespace HIMS.Data.DTO.IPPatient
{
    public class PatientWiseAdvanceListDto
    {
        public long AdvanceDetailID { get; set; }
        public string Date { get; set; }
        public long AdvanceId { get; set; }
        public string AdvanceNo { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal UsedAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public long OPD_IPD_Id { get; set; }
        public string UserName { get; set; }
        public string PaymentDate { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public decimal OnlineAmount { get; set; }
        public long TransactionType { get; set; }
        public string Reason { get; set; }
        public string Time { get; set; }
        public string PaymentTime { get; set; }



    }
}
