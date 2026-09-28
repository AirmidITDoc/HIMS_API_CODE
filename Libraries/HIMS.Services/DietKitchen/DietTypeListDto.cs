namespace HIMS.Data.DTO.DietKitchen
{
    public class DietTypeListDto
    {
        public long DietCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public long DietTypeID { get; set; }
        public string DietCode { get; set; } = null!;
        public string DietName { get; set; } = null!;
        public string? ShortName { get; set; }
        public string? Description { get; set; }
        public int? DefaultCalories { get; set; }  
        public int? DefaultProtein { get; set; }   
        public int? DefaultFluid { get; set; }          
        public bool? Active { get; set; }
        public int? DisplayOrder { get; set; }
        public string? Remarks { get; set; }
    }
}