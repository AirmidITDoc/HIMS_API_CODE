using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.Pathology
{
    public class PathologyReportTemplateModel
    {
        public long? PathReportId { get; set; }
        public long? PathTemplateId { get; set; }
        public string? PathTemplateDetailsResult { get; set; }
        public string? TemplateResultInHTML { get; set; }
        public long? TestId { get; set; }
        public string? SuggestionNotes { get; set; }
        public long? PathResultDr1 { get; set; }

    }
    public class PathologyReportTemplateModelValidator : AbstractValidator<PathologyReportTemplateModel>
    {
        public PathologyReportTemplateModelValidator()
        {
            RuleFor(x => x.PathReportId).NotNull().NotEmpty().WithMessage("PathReportId is required");
            RuleFor(x => x.PathTemplateId).NotNull().NotEmpty().WithMessage("From StoreId is required");
            RuleFor(x => x.PathTemplateDetailsResult).NotNull().NotEmpty().WithMessage(" PathTemplateId is required");
            RuleFor(x => x.TestId).NotNull().NotEmpty().WithMessage(" TestId is required");
        }
    }

    public class PathologyReportHeadermodel
    {
        public long? PathReportID { get; set; }
        public DateTime? ReportDate { get; set; }
        public string? ReportTime { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public long? PathResultDr1 { get; set; }
        public long? PathResultDr2 { get; set; }
        public long? PathResultDr3 { get; set; }
        public long? IsTemplateTest { get; set; }
        public string? SuggestionNotes { get; set; }
        public long? AdmVisitDoctorID { get; set; }
        public long? RefDoctorID { get; set; }

    }
    public class PathologyReportHeadermodelValidator : AbstractValidator<PathologyReportHeadermodel>
    {
        public PathologyReportHeadermodelValidator()
        {
            RuleFor(x => x.IsTemplateTest).NotNull().NotEmpty().WithMessage("IsTemplateTest is required");

        }
    }

        public class PathologyTemplatesModel
        {
            public PathologyReportTemplateModel PathologyReportTemplate { get; set; }
        public PathologyReportHeadermodel PathologyReportHeader { get; set; }

    }
    
}
