using FluentValidation;

namespace HIMS.API.Models.Pathology
{
    public class PathologyReportTemplateModel
    {
        public long PathReportTemplateDetId { get; set; }
        public long? PathReportId { get; set; }
        public long? PathTemplateId { get; set; }
        public string? PathTemplateDetailsResult { get; set; }
        public long? TestId { get; set; }
      
    }
    public class PathologyReportTemplateModelValidator : AbstractValidator<PathologyReportTemplateModel>
    {
        public PathologyReportTemplateModelValidator()
        {
            RuleFor(x => x.PathReportTemplateDetId).NotNull().NotEmpty().WithMessage("PathReportTemplateDetId is required");
            RuleFor(x => x.PathReportId).NotNull().NotEmpty().WithMessage("PathReportId is required");
            RuleFor(x => x.PathTemplateId).NotNull().NotEmpty().WithMessage("From StoreId is required");
            RuleFor(x => x.PathTemplateDetailsResult).NotNull().NotEmpty().WithMessage(" PathTemplateId is required");
            RuleFor(x => x.TestId).NotNull().NotEmpty().WithMessage(" TestId is required");
        }
    }
}
