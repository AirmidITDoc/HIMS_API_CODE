using FluentValidation;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.Data.Models;

namespace HIMS.API.Models.OPPatient
{
    public class DischargeCancellationModel
    {
        public long AdmissionId { get; set; }
        //public long? WardId { get; set; }
        //public long? BedId { get; set; }
        
    }
    public class DischargeCancellationModelValidator : AbstractValidator<DischargeCancellationModel>
    {
        public DischargeCancellationModelValidator()
        {
            RuleFor(x => x.AdmissionId).NotNull().NotEmpty().WithMessage("AdmissionId is required");

        }
    }
}
