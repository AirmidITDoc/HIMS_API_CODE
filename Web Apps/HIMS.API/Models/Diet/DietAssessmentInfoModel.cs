using FluentValidation;
using System;

namespace HIMS.API.Models.Diet
{
    public class DietAssessmentInfoModel
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
    }

    public class DietAssessmentInfoModelValidator : AbstractValidator<DietAssessmentInfoModel>
    {
        public DietAssessmentInfoModelValidator()
        {
            RuleFor(x => x.Opipid).NotNull().NotEmpty().WithMessage("Opipid is required");
            RuleFor(x => x.AssessmentDate).NotNull().NotEmpty().WithMessage("AssessmentDate is required");
        }
    }
}