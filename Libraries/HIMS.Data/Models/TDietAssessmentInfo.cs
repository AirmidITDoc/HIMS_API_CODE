using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDietAssessmentInfo
    {
        public long DietAssessmentId { get; set; }
        public long Opipid { get; set; }
        public byte Opiptype { get; set; }
        public long? NutritionRiskId { get; set; }
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
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
    }
}
