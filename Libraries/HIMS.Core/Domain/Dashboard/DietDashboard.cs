using System;
using System.Collections.Generic;

namespace HIMS.Core.Domain.Dashboard
{
    public class DietDashboard
    {
        public DietCountSummary CountSummary { get; set; }
        public List<DietOrdersByMealType> DietOrdersByMealTypes { get; set; }
        public List<DietOrdersByDietType> DietOrdersByDietTypes { get; set; }
        public List<DailyDietOrderCount> DailyDietOrderCounts { get; set; }
        public List<TodaysDietOrder> TodaysDietOrders { get; set; }
    }

    public class DietCountSummary
    {
        public int TotalPatients { get; set; }
        public int TotalDietOrders { get; set; }
        public int KitchenPendingOrders { get; set; }
        public int CancelledOrders { get; set; }
        public int MealsDispatched { get; set; }
        public int TotalDietMenus { get; set; }
        public int TotalAllergies { get; set; }
    }

    public class DietOrdersByMealType
    {
        public string MealName { get; set; }
        public int TotalOrders { get; set; }
    }

    public class DietOrdersByDietType
    {
        public string DietName { get; set; }
        public int TotalOrders { get; set; }
    }

    public class DailyDietOrderCount
    {
        public DateTime OrderDate { get; set; }
        public int TotalOrders { get; set; }
    }

    public class TodaysDietOrder
    {
        public long DietReqDetId { get; set; }
        public long DietReqId { get; set; }
        public DateTime OrderDate { get; set; }
        public long OPIPID { get; set; }
        public string MealName { get; set; }
        public string DietName { get; set; }
        public string AcceptanceStatus { get; set; }
        public string DeliveryStatus { get; set; }
        public string CancellationStatus { get; set; }
    }
}
