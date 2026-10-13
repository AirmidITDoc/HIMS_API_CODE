using System;

namespace HIMS.Data.DTO.DietKitchen
{
    public class DietAssessmentInfoDtoList
    {
        public long DietAssessmentId { get; set; }
        public DateTime AssessmentDate { get; set; }
        public DateTime AssessmentTime { get; set; }
        public long AssessmentById { get; set; }
        public long FoodPerferenceId { get; set; }
        public long? NutritionStatus { get; set; }
        public string? Allergies { get; set; }
        public string? SpecialInstructions { get; set; }
        public string? DietitionNotes { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? Bmi { get; set; }
        public string? PatientName { get; set; }
        public string? AgeGender { get; set; }
        public byte OPIPType { get; set; }
        public long OPIPId { get; set; }
        public long? NutritionRiskId { get; set; }
        public int? AgeYear { get; set; }
        public int? AgeMonth { get; set; }
        public int? AgeDay { get; set; }
    }
}