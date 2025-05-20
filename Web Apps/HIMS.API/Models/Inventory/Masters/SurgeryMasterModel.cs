using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class SurgeryMasterModel
    {
        public long SurgeryId { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public string? SurgeryName { get; set; }
        public long? DepartmentId { get; set; }
        public decimal? SurgeryAmount { get; set; }
        public long? SiteDescId { get; set; }
        public long? OttemplateId { get; set; }
    }
}