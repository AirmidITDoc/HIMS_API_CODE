namespace HIMS.Data.Models
{
    public partial class MRelationshipMaster
    {
        public long RelationshipId { get; set; }
        public string? RelationshipName { get; set; }
        public long? AddBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
