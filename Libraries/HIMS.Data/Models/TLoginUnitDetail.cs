namespace HIMS.Data.Models
{
    public partial class TLoginUnitDetail
    {
        public long LoginUnitDetId { get; set; }
        public long? LoginId { get; set; }
        public long? UnitId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual LoginManager? Login { get; set; }
    }
}
