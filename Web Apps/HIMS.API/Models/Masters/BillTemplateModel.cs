using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class BillTemplateModel
    {
        public long TemplateId { get; set; }
        public long ServiceId { get; set; }
        public double? Percentage { get; set; }
    }
    public class BillTemplateModelValidator : AbstractValidator<BillTemplateModel>
    {
        public BillTemplateModelValidator()
        {
            //RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage("ServiceId is required");
            //RuleFor(x => x.Percentage).NotNull().NotEmpty().WithMessage("Percentage is required");
           



        }
    }
}
