using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Masters
{
    public class TestMasterModel
    {

        public long TestId { get; set; }
        public string? TestName { get; set; }
         public string? PrintTestName { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public string? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string? TestTime { get; set; }
        public long? IsTemplateTest { get; set; }



        public List<PathTemplateDetailModel> MPathTemplateDetail { get; set; }
        
         }

    public class TestMasterModelValidator : AbstractValidator<TestMasterModel>
    {
        public TestMasterModelValidator()
        {
            RuleFor(x => x.TestName).NotNull().NotEmpty().WithMessage("TestName  is required");

            RuleFor(x => x.PrintTestName).NotNull().NotEmpty().WithMessage("PrintTestName  is required");



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
            RuleFor(x => x.PtemplateId).NotNull().NotEmpty().WithMessage("PtemplateId is required");
            RuleFor(x => x.TestId).NotNull().NotEmpty().WithMessage("TestId is required");
            RuleFor(x => x.TemplateId).NotNull().NotEmpty().WithMessage("TemplateId is required");



        }

       

    }
}

