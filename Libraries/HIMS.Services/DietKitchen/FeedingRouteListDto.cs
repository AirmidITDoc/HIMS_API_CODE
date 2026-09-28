namespace HIMS.Data.DTO.DietKitchen
{
    public class FeedingRouteListDto
    {
        public long FeedingRouteId { get; set; }
        public string FeedingRouteCode { get; set; } = null!;
        public string FeedingRouteName { get; set; } = null!;
        public string? Description { get; set; }
        public long? DietTypesId { get; set; }
        public bool? Active { get; set; }
        public string? DietName { get; set; } // Stored procedure madhla join kelela column
    }
}