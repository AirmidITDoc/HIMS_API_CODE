namespace HIMS.Data.Models
{
    public partial class TCompanyApprovalDetail
    {
        public long Id { get; set; }
        public long? AdmissionId { get; set; }
        public decimal? EstimateAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public string? Alentry { get; set; }
        public DateTime? DateApproved { get; set; }
        public string? Comments { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
