using System;

namespace HIMS.Data.DTO.DietKitchen
{
    public class PatientAssessmentListDto
    {
        public long DietAssessmentId { get; set; }
        public string? AssessmentDate { get; set; }
        public string? AssessmentTime { get; set; }
        public long AssessmentById { get; set; }
        public string? AssessByName { get; set; }
        public long FoodPerferenceId { get; set; }
        public long? NutritionStatus { get; set; }
        public string? Allergies { get; set; }
        public string? SpecialInstructions { get; set; }
        public string? DietitionNotes { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? Bmi { get; set; }
        public long? AdmissionId { get; set; }
        public long? RegId { get; set; }
        public string? RegNo { get; set; }
        public string? AdmissionTime { get; set; }
        public string? PatientName { get; set; }
        public string? AgeGender { get; set; }
        public byte OPIPType { get; set; }
        public long OPIPId { get; set; }
        public long? NutritionRiskId { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? FoodPreferenceName { get; set; }
        public string? NutritionStatusName { get; set; }

    }
}