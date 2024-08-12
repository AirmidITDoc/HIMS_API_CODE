using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class TestMasterModel
    {

        public long TestId { get; set; }
        public string? TestName { get; set; }
        public string? PrintTestName { get; set; }
    }

    public class TestMasterModelValidator : AbstractValidator<TestMasterModel>
    {
        public TestMasterModelValidator()
        {
            RuleFor(x => x.TestName).NotNull().NotEmpty().WithMessage("Test Name is required");
        }
    }
}
