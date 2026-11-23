namespace HIMS.API.Models.Canteen

{
    public class CanteenModel
    {
        public class CanteenDietPlanScheduleStatusUpdateModel
        {
            public DateTime PlanDate { get; set; }
            public long DietPlanDetId { get; set; }
            public long DietMealTypeId { get; set; }
            public string Flag { get; set; }
        }
        public class CanteenDietPlanDateStatusUpdateModel
        {
            public DateTime PlanDate { get; set; }
            public string Flag { get; set; }
        }
    }
}
