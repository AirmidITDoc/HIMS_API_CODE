using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class RadiologyTestModel
    {
        public long TestId { get; set; }
        public string? TestName { get; set; }
        public string? PrintTestName { get; set; }
        public long? CategoryId { get; set; }
        public long? ServiceId { get; set; }
        public List<RadiologyTemplateDetailModel> MRadiologyTemplateDetails { get; set; }
    }
     public class RadiologyTestModelValidator : AbstractValidator<RadiologyTestModel>
    {
        public RadiologyTestModelValidator()
        {
            RuleFor(x => x.TestName).NotNull().NotEmpty().WithMessage("TestName is required");
            RuleFor(x => x.PrintTestName).NotNull().NotEmpty().WithMessage("PrintTestName is required");
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage("CategoryId is required");
            RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage("ServiceId is required");
            
        }
    }
    public class RadiologyTemplateDetailModel
    {
        public long PtemplateId { get; set; }
        public long? TestId { get; set; }
        public long? TemplateId { get; set; }
    }
    public class RadiologyTemplateDetailModelValidator : AbstractValidator<RadiologyTemplateDetailModel>
    {
        public RadiologyTemplateDetailModelValidator()
        {
            RuleFor(x => x.TestId).NotNull().NotEmpty().WithMessage("TestId is required");
            RuleFor(x => x.TemplateId).NotNull().NotEmpty().WithMessage("TemplateId is required");
        }
    }
    public class CancelRadiologyTest
    {
        public long TestId { get; set; }
    }
    public class TRadiologyReportModel
    {
        public long RadReportId { get; set; }
        public DateTime? ReportDate { get; set; }
        public string? ReportTime { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public long? RadResultDr1 { get; set; }
        public long? RadResultDr2 { get; set; }
        public long? RadResultDr3 { get; set; }
        public string? SuggestionNotes { get; set; }
        public long? AdmVisitDoctorId { get; set; }
        public long? RefDoctorId { get; set; }
        public string? ResultEntry { get; set; }

    }
    public class TRadiologyReportModelValidator : AbstractValidator<TRadiologyReportModel>
    {
        public TRadiologyReportModelValidator()
        {
            RuleFor(x => x.ReportDate).NotNull().NotEmpty().WithMessage("ReportDate is required");
            RuleFor(x => x.ReportTime).NotNull().NotEmpty().WithMessage("ReportTime is required");
        }
    }
}
