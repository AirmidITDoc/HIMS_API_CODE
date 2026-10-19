using System;

namespace HIMS.Services.DietKitchen
{
    public class DietPlanScheduleListDto
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
        public string? CreatedByName { get; set; }
        public long? AdmissionId { get; set; }
        public long? RegId { get; set; }
        public string? RegNo { get; set; }
        public string? AdmissionTime { get; set; }
        public string? Patient { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? DietName { get; set; }
        public string? DietMenuName { get; set; }
    }

    public class DietPlanDetailListDto
    {
        public long DietPlanId { get; set; }
        public long OPIPID { get; set; }
        public long OPIPType { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanEndDate { get; set; }
        public DateTime PlanDate { get; set; }
        public string? DietName { get; set; }
        public string? MealName { get; set; }
        public string? Instruction { get; set; }
        public string? Comment { get; set; }
        public long DietTypeId { get; set; }
        public long DietMealTypeId { get; set; }
        public long DietMenuId { get; set; }
        public string? DietMenuName { get; set; }
    }
}