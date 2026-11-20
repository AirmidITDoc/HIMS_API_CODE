using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class BillTemplateModel
    {
        public long OtbtempId { get; set; }
        public string OtBillTemplateName { get; set; } = null!;
        public List<BillTemplateDetailsModel> MOtBillTemplateDetails { get; set; }

    }
    public class BillTemplateModelValidator : CommonDynamicValidator<BillTemplateModel>
    {
        public BillTemplateModelValidator()
        {
            RuleFor(x => x.OtBillTemplateName).NotNull().NotEmpty().WithMessage("OtBillTemplateName is required");
        }
    }
        public class BillTemplateDetailsModel
        {
          public long OtbtempDetId { get; set; }
          public long? OtbtempId { get; set; }
          public long ServiceId { get; set; }
          public double? Percentage { get; set; }
        }
    public class BillTemplateDetailsModelValidator : CommonDynamicValidator<BillTemplateDetailsModel>
    {
        public BillTemplateDetailsModelValidator()
        {
            RuleFor(x => x.OtbtempId).NotNull().WithMessage("OtbtempId is required");
            RuleFor(x => x.ServiceId).NotNull().WithMessage("ServiceId is required");
            RuleFor(x => x.Percentage).NotNull().WithMessage("Percentage is required");

        }
    }
}

