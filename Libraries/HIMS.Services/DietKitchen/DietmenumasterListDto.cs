namespace HIMS.Services.DietKitchen
{
    public class DietmenumasterListDto
    {
        public long DietMenuId { get; set; }
        public string DietMenuCode { get; set; } = null!;
        public string DietMenuName { get; set; } = null!;
        public long MealTypeId { get; set; }
        public long DietTypeId { get; set; }
        public string? Texture { get; set; }
        public string? Calories { get; set; }
        public string? Protein { get; set; }
        public long MenuDetId { get; set; }
        public long FoodItemId { get; set; }
        public int? Quantity { get; set; }
        public long? UnitId { get; set; }
        public int? SequenceNo { get; set; }
    }
}