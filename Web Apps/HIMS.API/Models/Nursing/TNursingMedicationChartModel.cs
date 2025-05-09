using FluentValidation;
using HIMS.API.Models.OutPatient;

namespace HIMS.API.Models.Nursing
{
    public class TNursingMedicationChartModel
    {
        public long MedChartId { get; set; }
        public long AdmId { get; set; }
        public DateTime? Mdate { get; set; }
        public string? Mtime { get; set; }
        public long? DurgId { get; set; }
        public long? DoseId { get; set; }
        public string? Route { get; set; }
        public string? Freq { get; set; }
        public long? IsAddedBy { get; set; }
        public string? NurseName { get; set; }
        public bool? IsCancelled { get; set; }
        public string? DoseName { get; set; }

    }


    public class TNursingMedicationChartModelValidator : AbstractValidator<TNursingMedicationChartModel>
    {
        public TNursingMedicationChartModelValidator()
        {
            RuleFor(x => x.AdmId).NotNull().NotEmpty().WithMessage("AdmId is required");

        }
    }

    public class NursingMedicationChartModel
    {
        public List<TNursingMedicationChartModel> NursingMedicationChart { get; set; }
 

    }
}