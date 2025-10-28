namespace HIMS.Data.Models
{
    public partial class VPaymentBillwisesumAmount
    {
        public long? BillNo { get; set; }
        public decimal? CashPay { get; set; }
        public decimal? ChequePay { get; set; }
        public decimal? CardPay { get; set; }
        public decimal? AdvUsedPay { get; set; }
        public decimal? Neftpay { get; set; }
        public decimal? PayTmpay { get; set; }
        public double? Tdsamount { get; set; }
        public int? PayCount { get; set; }
    }
}
