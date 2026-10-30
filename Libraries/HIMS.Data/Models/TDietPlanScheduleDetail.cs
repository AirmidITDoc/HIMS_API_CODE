using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDietPlanScheduleDetail
    {
        public long DietPlanDetId { get; set; }
        public long? DietPlanId { get; set; }
        public long Opipid { get; set; }
        public long Opiptype { get; set; }
        public DateTime PlanDate { get; set; }
        public long DietTypeId { get; set; }
        public long DietMealTypeId { get; set; }
        public long DietMenuId { get; set; }
        public string? Instruction { get; set; }
        public string? Comment { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Status { get; set; }
        public string? OrderNo { get; set; }
        public bool? IsOrder { get; set; }
        public long? IsOrderBy { get; set; }
        public DateTime? IsOrderDateTime { get; set; }
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

        public virtual TDietPlanSchedule? DietPlan { get; set; }
    }
}
