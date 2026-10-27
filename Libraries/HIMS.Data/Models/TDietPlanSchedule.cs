using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDietPlanSchedule
    {
        public TDietPlanSchedule()
        {
            TDietPlanScheduleDetails = new HashSet<TDietPlanScheduleDetail>();
        }

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
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }

        public virtual ICollection<TDietPlanScheduleDetail> TDietPlanScheduleDetails { get; set; }
    }
}
