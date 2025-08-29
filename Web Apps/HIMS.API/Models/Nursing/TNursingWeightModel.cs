using FluentValidation;

namespace HIMS.API.Models.Nursing
{
    public class TNursingWeightModel
    {
        public long PatWeightId { get; set; }
        public DateTime? PatWeightDate { get; set; }
        public string? PatWeightTime { get; set; }
        public long? AdmissionId { get; set; }
        public int? PatWeightValue { get; set; }
    }
}