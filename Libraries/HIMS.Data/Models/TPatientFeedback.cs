namespace HIMS.Data.Models
{
    public partial class TPatientFeedback
    {
        public long PatientFeedbackId { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? DepartmentId { get; set; }
        public long? FeedbackQuestionId { get; set; }
        public long? FeedbackRating { get; set; }
        public string? FeedbackComments { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
