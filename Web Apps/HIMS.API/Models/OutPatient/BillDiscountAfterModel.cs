namespace HIMS.API.Models.OutPatient
{
    public class BillDiscountAfterModel
    {
        public long? @BillNo { get; set; }
        public double? NetPayableAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public double? CompDiscAmt { get; set; }
        public double? BalanceAmt { get; set; }
        public long? ConcessionReasonId { get; set; }



    }
}