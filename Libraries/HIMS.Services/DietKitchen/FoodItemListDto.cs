namespace HIMS.Data.DTO.DietKitchen
{
    public class FoodItemListDto
    {
        public long FoodItemId { get; set; }
        public string FoodCode { get; set; } = null!;
        public string FoodName { get; set; } = null!;
        public long FoodCategoryId { get; set; }
        public string? LocalName { get; set; }
        public long Unit { get; set; }
        public bool? IsVegetarian { get; set; }
        public bool? Active { get; set; }
        public string? FoodCategoryName { get; set; }
        public string? Value { get; set; }
    }
}