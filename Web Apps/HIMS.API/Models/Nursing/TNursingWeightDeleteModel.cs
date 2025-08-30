using FluentValidation;

namespace HIMS.API.Models.Nursing
{
    public class TNursingWeightDeleteModel
    {
        public long? PatWeightId { get; set; }
        public bool? IsActive { get; set; }
        public string? Reason { get; set; }
    }
}