namespace HIMS.Services.DietKitchen
{
    public class DietmenumasterListDto
    {
        public long DietMenuId { get; set; }
        public string? DietMenuCode { get; set; }
        public string? DietMenuName { get; set; }
        public long MealTypeId { get; set; }
        public long DietTypeId { get; set; }
        public string? Texture { get; set; }
        public string? Calories { get; set; }
        public string? Protein { get; set; }
        public string? MealTypeCode { get; set; }
        public string? MealName { get; set; }
        public string? DietCode { get; set; }
        public string? DietName { get; set; }
        public string? CreatedDate { get; set; }
        public string? DietMenuDetails { get; set; }


    }
}