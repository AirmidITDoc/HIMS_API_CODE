using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CompanyExecutiveInfoModel
    {
        public long Id { get; set; }
        public long? CompanyId { get; set; }
        public long? EmployeId { get; set; }
    }
    public class CompanyExecutiveInfoModelValidator : AbstractValidator<CompanyExecutiveInfoModel>
    {
        public CompanyExecutiveInfoModelValidator()
        {
            RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId is required");
            RuleFor(x => x.EmployeId).NotNull().NotEmpty().WithMessage("EmployeId is required");
           
        }
    }

}
