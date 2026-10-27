using FluentValidation;
using System;
using System.Collections.Generic;

namespace HIMS.API.Models.Diet
{
    public class DietPlanScheduleModel
    {
        public long DietPlanId { get; set; }
        public long Opipid { get; set; }
        public long Opiptype { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanEndDate { get; set; }
        public DateTime PlanDate { get; set; }
        public long DietTypeId { get; set; }
        public long DietMealTypeId { get; set; }
        public long DietMenuId { get; set; }
        public string? Instruction { get; set; }
        public string? Comment { get; set; }
        public bool? DraftOrFinal { get; set; }
        public List<DietPlanScheduleDetailModel> TDietPlanScheduleDetails { get; set; }
    }

    public class DietPlanScheduleDetailModel
    {
        public long DietPlanDetId { get; set; }
        public long DietPlanId { get; set; }
        public long Opipid { get; set; }
        public long Opiptype { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanEndDate { get; set; }
        public DateTime PlanDate { get; set; }
        public long DietTypeId { get; set; }
        public long DietMealTypeId { get; set; }
        public long DietMenuId { get; set; }
        public string? Instruction { get; set; }
        public string? Comment { get; set; }

    }


    public class DietPlanScheduleCancelModel
    {
        public long DietPlanId { get; set; }
        public long? IsCancelledBy { get; set; }
       
    }


    public class DietPlanScheduleDetailsCancelModel
    {
        public long DietPlanDetId { get; set; }
        public long? IsCancelledBy { get; set; }
        
        public string? CancelledReason { get; set; }
    }

    public class DietPlanScheduleModelValidator : AbstractValidator<DietPlanScheduleModel>
    {
        public DietPlanScheduleModelValidator()
        {
            RuleFor(x => x.Opipid).NotNull().NotEmpty().WithMessage("Opipid is required");
            RuleFor(x => x.PlanStartDate).NotNull().NotEmpty().WithMessage("PlanStartDate is required");
        }
    }
}