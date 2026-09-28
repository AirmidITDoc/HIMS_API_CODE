namespace HIMS.Data.DTO.DietKitchen
{
    public class AllergyListDto
    {
        public long FoodCategoryId { get; set; }
        public string? FoodCategoryName { get; set; }
        public long AllergyId { get; set; }
        public string AllergyCode { get; set; } = null!;
        public long SeverityId { get; set; }
        public string AllergyName { get; set; } = null!;
        public string? Reaction { get; set; }
        public bool? IsKitchenAlert { get; set; }
        public bool? Active { get; set; }
        public string? Value { get; set; }
    }
}