namespace HIMS.Data.Models
{
    public partial class MCompanyServiceAssignMaster
    {
        public long CompanyAssignId { get; set; }
        public long? CompanyId { get; set; }
        public long? ServiceId { get; set; }
        public int? ServicePrice { get; set; }
        public int? ServiceQty { get; set; }
        public int? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
