using FluentValidation;
using HIMS.Data.Models;

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
        public string? SuggestionNote { get; set; }
        public string? FootNote { get; set; }
        public bool? IsActive { get; set; }
        public long? ServiceId { get; set; }
        public long? IsTemplateTest { get; set; }
        public string? TestTime { get; set; }
        public string? TestDate { get; set; }

        public List<PathTemplateDetailModel>? MPathTemplateDetails { get; set; }
        public List<PathTestDetailModel>? MPathTestDetailMasters { get; set; }
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
        public long PtemplateId { get; set; }
        public long TestId { get; set; }
        public long  TemplateId { get; set; }
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
        public long TestDetId { get; set; }
        public long? TestId { get; set; }
        public long? SubTestId { get; set; }
        public long? ParameterId { get; set; }
    }
    public class PathTestDetailModelModelValidator : AbstractValidator<PathTestDetailModel>
    {
        public PathTestDetailModelModelValidator()
        {
            RuleFor(x => x.TestId).NotNull().NotEmpty().WithMessage("TestId  is required");
            RuleFor(x => x.SubTestId).NotNull().NotEmpty().WithMessage("SubTestId required");
            RuleFor(x => x.ParameterId).NotNull().NotEmpty().WithMessage("ParameterId is required");
        }
    }
        public class PathTestDetDelete
        {
            public long TestId { get; set; }
        }
}

