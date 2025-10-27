namespace HIMS.Data.Models
{
    public partial class MGenericMaster
    {
        public long GenericId { get; set; }
        public string? GenericName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
