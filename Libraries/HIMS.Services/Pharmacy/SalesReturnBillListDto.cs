namespace HIMS.Services.Pharmacy
{
    public class SalesReturnBillListDto
    {

        public long SalesReturnId { get; set; }
        public DateTime Date { get; set; }
        public string SalesReturnNo { get; set; }
        public string RegNo { get; set; }
        public string PatientName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public decimal CardPayAmount { get; set; }
        public long TransactionType { get; set; }
        public long OP_IP_Type { get; set; }
        public DateTime PaymentDate { get; set; }
        public string IsPrescription { get; set; }
        public string Label { get; set; }




    }
}
