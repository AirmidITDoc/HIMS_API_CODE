using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class PrescriptionMasterModel
    {
        public long PrecriptionId { get; set; }

        public String? Date { get; set; }
        public String? Ptime { get; set; }
       

        public long? IsAddBy { get; set; }

       



    }


    public class PrescriptionMasterModelValidator : AbstractValidator<PrescriptionMasterModel>
    {
        public PrescriptionMasterModelValidator()
        {
            RuleFor(x => x.PrecriptionId).NotNull().NotEmpty().WithMessage("PrecriptionId  is required");
       

        }
    }
}
