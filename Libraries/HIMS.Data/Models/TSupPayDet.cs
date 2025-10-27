namespace HIMS.Data.Models
{
    public partial class TSupPayDet
    {
        public long SupTranId { get; set; }
        public long SupPayId { get; set; }
        public long SupGrnId { get; set; }

        public virtual TGrnsupPayment SupPay { get; set; } = null!;
    }
}
