using FluentValidation;

namespace HIMS.API.Models.Nursing
{
    public class NursingVitalsDeleteModel
    {
        public long VitalId { get; set; }
        public bool? IsActive { get; set; }
        public string? Reason { get; set; }
    }
}