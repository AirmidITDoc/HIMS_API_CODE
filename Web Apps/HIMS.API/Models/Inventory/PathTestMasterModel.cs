using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class PathTestMasterModel
    {
        public int TestId { get; set; }
        public string? TestName { get; set; }
        public string? PrintTestName { get; set; }
        public long? CategoryId { get; set; }
        public bool? IsSubTest { get; set; }
        public string? TechniqueName { get; set; }
        public string? MachineName { get; set; }
        public string ?SuggestionNote { get; set; }
        public string? FootNote { get; set; }
        public bool? IsDeleted { get; set; }
        public long ?ServiceId { get; set; }
        public bool? IsTemplateTest { get; set; }
        public string? TestTime { get; set; }
        public string? TestDate { get; set; }


        public List<PathTemplateDetailModel> MPathTemplateDetail { get; set; }


    }
    public class PathTestMasterModelValidator : AbstractValidator<PathTestMasterModel>
    {
        public PathTestMasterModelValidator()
        {
            RuleFor(x => x.TestName).NotNull().NotEmpty().WithMessage("TestName is required");
            RuleFor(x => x.PrintTestName).NotNull().NotEmpty().WithMessage("PrintTestName  is required");
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage(" CategoryId required");
            RuleFor(x => x.TechniqueName).NotNull().NotEmpty().WithMessage("TechniqueName is required");
            RuleFor(x => x.SuggestionNote).NotNull().NotEmpty().WithMessage("SuggestionNote is required");
            RuleFor(x => x.FootNote).NotNull().NotEmpty().WithMessage("FootNote  is required");
            RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage(" ServiceId required");
        }
    }

    public class PathTemplateDetailModel
    {
        public long PtemplateId { get; set; }
        public long TestId { get; set; }
        public long  TemplateId { get; set; }

    }
    public class PathTemplateDetailModelValidator : AbstractValidator<PathTemplateDetailModel>
    {
        public PathTemplateDetailModelValidator()
        {
            RuleFor(x => x.PtemplateId).NotNull().NotEmpty().WithMessage("PtemplateId is required");
            RuleFor(x => x.TestId).NotNull().NotEmpty().WithMessage("TestId  is required");
            RuleFor(x => x.TemplateId).NotNull().NotEmpty().WithMessage(" TemplateId required");
            
        }
    }
}
