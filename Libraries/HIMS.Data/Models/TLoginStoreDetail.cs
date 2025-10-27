namespace HIMS.Data.Models
{
    public partial class TLoginStoreDetail
    {
        public long LoginStoreDetId { get; set; }
        public long? LoginId { get; set; }
        public long? StoreId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual LoginManager? Login { get; set; }
    }
}
