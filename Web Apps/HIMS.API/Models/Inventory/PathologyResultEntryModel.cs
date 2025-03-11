using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class PathologyResultEntryModel
    {
        public long PathReportDetId { get; set; }
        public long? PathReportId { get; set; }
        public long? CategoryId { get; set; }
        public long? TestId { get; set; }
        public long? SubTestId { get; set; }
        public long? ParameterId { get; set; }
        public string? ResultValue { get; set; }
        public long? UnitId { get; set; }
        public string? NormalRange { get; set; }
        public long? PrintOrder { get; set; }
        public long? PisNumeric { get; set; }
        public string? CategoryName { get; set; }
        public string? TestName { get; set; }
        public string? SubTestName { get; set; }
        public string? ParameterName { get; set; }
        public string? UnitName { get; set; }
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public string? SampleId { get; set; }
        public string? ParaBoldFlag { get; set; }
        public float? MinValue { get; set; }
        public float? MaxValue { get; set; }
    }
    public class PathologyResultEntryModelValidator : AbstractValidator<PathologyResultEntryModel>
    {
        public PathologyResultEntryModelValidator()
        {
            RuleFor(x => x.PathReportId).NotNull().NotEmpty().WithMessage("PathReportId is required");
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithMessage("CategoryId  is required");
            RuleFor(x => x.SubTestId).NotNull().NotEmpty().WithMessage(" SubTestId required");
            RuleFor(x => x.TestId).NotNull().NotEmpty().WithMessage("TestId is required");
          
        }
    }
    public class TPathologyReportHeaderModel
    {
        public long PathReportId { get; set; }
        public DateTime? ReportDate { get; set; }
        public string? ReportTime { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public long? PathResultDr1 { get; set; }
        public long? PathResultDr2 { get; set; }
        public long? PathResultDr3 { get; set; }
        public long? IsTemplateTest { get; set; }
        public string? SuggestionNotes { get; set; }
        public long? AdmVisitDoctorId { get; set; }
        public long? RefDoctorId { get; set; }
    }
    public class TPathologyReportHeaderModelValidator : AbstractValidator<TPathologyReportHeaderModel>
    {
        public TPathologyReportHeaderModelValidator()
        {
            RuleFor(x => x.ReportDate).NotNull().NotEmpty().WithMessage("ReportDate is required");
            RuleFor(x => x.ReportTime).NotNull().NotEmpty().WithMessage("ReportTime  is required");

        }
    }
    public class PathologyResultModel
    {
        public List<PathologyResultEntryModel> PathologyResult {  get; set; }
        public List<TPathologyReportHeaderModel> PathologyReport { get; set; }

    }

}
