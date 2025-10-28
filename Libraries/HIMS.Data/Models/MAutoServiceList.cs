namespace HIMS.Data.Models
{
    public partial class MAutoServiceList
    {
        public long SysId { get; set; }
        public long? ServiceId { get; set; }
        public bool? IsAutoBedCharges { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
