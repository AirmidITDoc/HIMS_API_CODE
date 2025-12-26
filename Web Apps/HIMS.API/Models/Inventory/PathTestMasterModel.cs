using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class PathTestMasterModel
    {
        public string? TestName { get; set; }
        public string? PrintTestName { get; set; }
        public long? CategoryId { get; set; }
        public bool? IsSubTest { get; set; }
        public string? TechniqueName { get; set; }
        public string? MachineName { get; set; }
        public string? SuggestionNote { get; set; }
        public string? FootNote { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? ServiceId { get; set; }
        public long? IsTemplateTest { get; set; }
        public bool? IsCategoryPrint { get; set; }
        public bool? IsPrintTestName { get; set; }
        public string? TestTime { get; set; }
        public DateTime? TestDate { get; set; }
        public int? CreatedBy { get; set; }
        public int TestId { get; set; }

    }
    public class PathTestMasterModelValidator : AbstractValidator<PathTestMasterModel>
    {
        public PathTestMasterModelValidator()
        {
            RuleFor(x => x.TestName).NotNull().NotEmpty().WithMessage("TestName is required");
            RuleFor(x => x.PrintTestName).NotNull().NotEmpty().WithMessage("PrintTestName  is required");
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage(" CategoryId required");
            RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage(" ServiceId required");
        }
    }

    public class PathTemplateDetailModel
    {

        public long TestId { get; set; }
        public long TemplateId { get; set; }
    }
    public class PathTemplateDetailModelValidator : AbstractValidator<PathTemplateDetailModel>
    {
        public PathTemplateDetailModelValidator()
        {
            RuleFor(x => x.TestId).NotNull().NotEmpty().WithMessage("TestId  is required");
            RuleFor(x => x.TemplateId).NotNull().NotEmpty().WithMessage(" TemplateId required");

        }
    }
    public class PathTestDetailModel
    {
        public long? TestId { get; set; }
        public long? SubTestId { get; set; }
        public long? ParameterId { get; set; }
    }
    public class PathTestDetailModelModelValidator : AbstractValidator<PathTestDetailModel>
    {
        public PathTestDetailModelModelValidator()
        {
            RuleFor(x => x.SubTestId).NotNull().NotEmpty().WithMessage("SubTestId required");
            RuleFor(x => x.ParameterId).NotNull().NotEmpty().WithMessage("ParameterId is required");
        }
    }
    public class PathTestDetDelete
    {
        public long TestId { get; set; }
    }
    public class TestMasterModel
    {
        public PathTestMasterModel PathTest { get; set; }
        public List<PathTemplateDetailModel> PathTemplateDetail { get; set; }
        public List<PathTestDetailModel> PathTestDetail { get; set; }

    }
    public class PathTestMasterModel1
    {
        public int TestId { get; set; }
        public string? TestName { get; set; }
        public string? PrintTestName { get; set; }
        public long? CategoryId { get; set; }
        public bool? IsSubTest { get; set; }
        public string? TechniqueName { get; set; }
        public string? MachineName { get; set; }
        public string? SuggestionNote { get; set; }
        public string? FootNote { get; set; }
        public bool? IsActive { get; set; }
        public long? UpdatedBy { get; set; }
        public long? ServiceId { get; set; }
        public long? IsTemplateTest { get; set; }
        public bool? IsCategoryPrint { get; set; }
        public bool? IsPrintTestName { get; set; }
        public string? TestTime { get; set; }
        public DateTime? TestDate { get; set; }
        public int? ModifiedBy { get; set; }

    }

    public class TestMasterUpdate
    {
        public PathTestMasterModel1 PathTest { get; set; }
        public List<PathTemplateDetailModel> PathTemplateDetail { get; set; }
        public List<PathTestDetailModel> PathTestDetail { get; set; }
    }

}

