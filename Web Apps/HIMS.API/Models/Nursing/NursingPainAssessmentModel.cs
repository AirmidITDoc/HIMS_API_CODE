namespace HIMS.API.Models.Nursing
{
    public class NursingPainAssessmentModel
    {
        public long PainAssessmentId { get; set; }
        public DateTime? PainAssessmentDate { get; set; }
        public string? PainAssessmentTime { get; set; }
        public long? AdmissionId { get; set; }
        public int? PainAssessementValue { get; set; }
    }
}