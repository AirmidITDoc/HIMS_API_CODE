namespace HIMS.Data.Models
{
    public partial class MisBillDateWiseBillPaymentSummary
    {
        public DateTime? BillDate { get; set; }
        public int? BillDay { get; set; }
        public int? BillWeek { get; set; }
        public int? BillMonth { get; set; }
        public int? BillQuarter { get; set; }
        public int? BillYear { get; set; }
        public string OpdIpd { get; set; } = null!;
        public int? BBilledPatientCount { get; set; }
        public decimal? BTotalBillAmountSum { get; set; }
        public decimal? BTotalConcessionAmountSum { get; set; }
        public decimal? BTotalDiscountAmountSum { get; set; }
        public decimal? BNetBillAmountSum { get; set; }
        public decimal? BTotalBalanceAmountSum { get; set; }
        public decimal? PCashPayAmountSum { get; set; }
        public decimal? PChequePayAmountSum { get; set; }
        public decimal? PDebitCardPayAmountSum { get; set; }
        public decimal? PCreditCardPayAmountSum { get; set; }
        public decimal? PUpipayAmountSum { get; set; }
        public decimal? POnlinePayAmountSum { get; set; }
        public decimal? PTotalPaidAmountSum { get; set; }
        public decimal? PConcessionSum { get; set; }
        public decimal? PRefundAmountSum { get; set; }
        public decimal? PPatientAdvanceAmountSum { get; set; }
        public decimal? PBillBalanceAmountSum { get; set; }
        public decimal? PBadDebitAmountSum { get; set; }
        public string? RecordInsertedBy { get; set; }
        public string? RecordUpdatedBy { get; set; }
        public DateTime? RecordInsertedOn { get; set; }
        public DateTime? RecordUpdatedOn { get; set; }
    }
}
