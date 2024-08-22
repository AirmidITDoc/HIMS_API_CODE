using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Masters
{
    public class TestMasterModel
    {

        public long TestId { get; set; }
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
        public long? UpdatedBy { get; set; }
        public long? ServiceId { get; set; }
        public long? IsTemplateTest { get; set; }
        public string? CreatedDate { get; set; }
        public string? TestTime { get; set; }
         public List<PathTemplateDetailModel> MPathTemplateDetail { get; set; }
        
         }
        public class TestMasterModelValidator : AbstractValidator<TestMasterModel>
         {
        public TestMasterModelValidator()
        {
            RuleFor(x => x.TestName).NotNull().NotEmpty().WithMessage("TestName  is required");
            RuleFor(x => x.PrintTestName).NotNull().NotEmpty().WithMessage("PrintTestName  is required");
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage("CategoryId  is required");
            RuleFor(x => x.IsSubTest).NotNull().NotEmpty().WithMessage("IsSubTest  is required");
            RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage("ServiceId  is required");


        }
    }

    public class PathTemplateDetailModel
    {
        public long PtemplateId { get; set; }
        public long? TestId { get; set; }
        public long? TemplateId { get; set; }
    }

    public class PathTemplateDetailModelValidator : AbstractValidator<PathTemplateDetailModel>
    {
        public PathTemplateDetailModelValidator()
        {
           
            RuleFor(x => x.TestId).NotNull().NotEmpty().WithMessage("TestId is required");
            RuleFor(x => x.TemplateId).NotNull().NotEmpty().WithMessage("TemplateId is required");
        }

    }
}

