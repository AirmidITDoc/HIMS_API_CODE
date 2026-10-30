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
        public string? DietName { get; set; }
        public long DietMealTypeId { get; set; }
        public long DietMenuId { get; set; }
        public string? Instruction { get; set; }
        public string? Comment { get; set; }
        public bool? IsCancelled { get; set; }
        public string? CancelledUser { get; set; }
        public string? CreatedByName { get; set; }
        public long? AdmissionId { get; set; }
        public string? AdmissionTime { get; set; }
        public string? Patient { get; set; }
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
    public class DietSchedulePatientListDto
    {
        public long DietPlanId { get; set; }
        public long OPIPID { get; set; }
        public long DietMealTypeId { get; set; }
        public DateTime PlanDate { get; set; }
        public string? MealName { get; set; }
        public DateTime? DefaultTime { get; set; }
        public DateTime? OrderCutoffTime { get; set; }
        public DateTime? PreparationStartTime { get; set; }
        public DateTime? DispatchTime { get; set; }
        public string? PatientName { get; set; }
        public string? Status { get; set; }
        public bool? IsAccepted { get; set; }
        public long? IsAcceptedBy { get; set; }
        public DateTime? IsAcceptedDateTime { get; set; }
        public bool? IsInProgress { get; set; }
        public long? IsInProgressBy { get; set; }
        public DateTime? IsInProgressDateTime { get; set; }
        public bool? IsDelivered { get; set; }
        public long? IsDeliveredBy { get; set; }
        public DateTime? IsDeliveredDateTime { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public string? CancelledReason { get; set; }
    }
    public class DietPlanScheduleStatusUpdateModel
    {
        public long DietPlanIds { get; set; }
        public string Flag { get; set; }
    }


}