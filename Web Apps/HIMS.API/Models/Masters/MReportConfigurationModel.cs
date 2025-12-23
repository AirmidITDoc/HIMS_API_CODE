using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public partial class MReportConfigurationModel
    {
        public long ReportId { get; set; }
        public string? ReportSection { get; set; }
        public string? ReportName { get; set; }
    }
    public class MReportConfigurationModelValidator : AbstractValidator<MReportConfigurationModel>
    {
        public MReportConfigurationModelValidator()
        {
            RuleFor(x => x.ReportSection).NotNull().NotEmpty().WithMessage("ReportSection is required");
            RuleFor(x => x.ReportName).NotNull().NotEmpty().WithMessage("ReportName is required");

        }
    }
}
