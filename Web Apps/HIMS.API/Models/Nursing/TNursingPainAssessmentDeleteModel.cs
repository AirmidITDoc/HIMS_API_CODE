namespace HIMS.API.Models.Nursing
{
    public class TNursingPainAssessmentDeleteModel
    {
        public long? PainAssessmentId { get; set; }
        public bool? IsActive { get; set; }
        public string? Reason { get; set; }
    }
}
