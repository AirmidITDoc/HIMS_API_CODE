namespace HIMS.Data.DTO.OTManagment
{
    public class SiteDescriptionListDto
    {
        public long SiteDescId { get; set; }
        public string? SiteDescriptionName { get; set; }
        public long SurgeryCategoryId { get; set; }
        public bool? IsActive { get; set; }
        public string? SurgeryCategoryName { get; set; }
       
    }
}