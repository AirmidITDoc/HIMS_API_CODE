using System;

namespace HIMS.Services.DietKitchen
{
    public class DietPlanScheduleListDto
    {
        public long DietPlanId { get; set; }
        public long Opipid { get; set; }
        public long Opiptype { get; set; }
        public bool? DraftorFinal { get; set; }
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
        public string? Acceptuser { get; set; }
        public string? Progressuser { get; set; }
        public string? Deliveruser { get; set; }
        public string? Cancleuser { get; set; }
        public bool? IsAccepted { get; set; }
        public bool? IsInProgress { get; set; }
        public bool? IsDelivered { get; set; }
        public bool? IsCancelled { get; set; }
        public string? CancelledReason { get; set; }
        public int? TotalCount { get; set; }
        public int? AcceptedCount { get; set; }
        public int? InProgressCount { get; set; }
        public int? DeliveredCount { get; set; }
        public int? CancelledCount { get; set; }
        public DateTime? LastAcceptedDateTime { get; set; }
        public DateTime? LastDeliveredDateTime { get; set; }
        public string? AllAccepted { get; set; }
        public string? AllDelivered { get; set; }



    }

    public class DietPlanDetailListDto
    {
        public long DietPlanId { get; set; }
        public long OPIPID { get; set; }
        public DateTime PlanDate { get; set; }
        public string? DietName { get; set; }
        public string? MealName { get; set; }
        public string? DietMenuName { get; set; }
        public string? Instruction { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Status { get; set; }
        public bool? IsAccepted { get; set; }
        public long? IsInProgressBy { get; set; }
        public bool? IsDelivered { get; set; }
        public bool? IsCancelled { get; set; }
        public string? CancelledReason { get; set; }
        public string? Acceptuser { get; set; }
        public string? Progressuser { get; set; }
        public string? Deliveruser { get; set; }
        public string? Cancleuser { get; set; }
        public string? CreatedUser { get; set; }
        public long? MealId { get; set; }
        public long? DietTypeId { get; set; }
        public long? DietMealTypeId { get; set; }
        public long? DietMenuId { get; set; }

    }
    public class DietMainlListDto
    {
        public DateTime? PlanDate { get; set; }
        public long? DietPlanId { get; set; }
        public int? PatientWiseCount { get; set; }
        public string? Status { get; set; }
    }
    public class DietMainSublListDto
    {
        public long? DietMealTypeId { get; set; }
        public long? DietPlanId { get; set; }
        public DateTime? PlanDate { get; set; }
        public string? MealName { get; set; }
        public DateTime? DefaultTime { get; set; }
        public int? PatientWiseCount { get; set; }
        public string? Status { get; set; }
        
    }


}