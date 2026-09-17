using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.DietKitchen
{
    public class MealTypeMasterModel
    {
        public long MealId { get; set; }
        public string MealTypeCode { get; set; } = null!;
        public string MealName { get; set; } = null!;
        public string? DefaultTime { get; set; }
        public string? OrderCutoffTime { get; set; }
        public string? PreparationStartTime { get; set; }
        public string? DispatchTime { get; set; }
        public int? MealSequence { get; set; }


    }
}