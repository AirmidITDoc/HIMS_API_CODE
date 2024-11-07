using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class PathTemplateResultModel
    {
        public long PathReportTemplateDetId { get; set; }
        public long? PathReportId { get; set; }
        public long? PathTemplateId { get; set; }
        public string? PathTemplateDetailsResult { get; set; }
        public long? TestId { get; set; }
        public string? TemplateResultInHtml { get; set; }
        public List<TPathologyReportHeaderModell> TPathologyReportHeaders { get; set; }
    }
    public class PathTemplateResultModelValidator : AbstractValidator<PathTemplateResultModel>
    {
        public PathTemplateResultModelValidator()
        {
            RuleFor(x => x.PathTemplateId).NotNull().NotEmpty().WithMessage("PathTemplateId is required");
            RuleFor(x => x.PathTemplateDetailsResult).NotNull().NotEmpty().WithMessage("PathTemplateDetailsResult  is required");
            RuleFor(x => x.TestId).NotNull().NotEmpty().WithMessage(" TestId required");
            RuleFor(x => x.TemplateResultInHtml).NotNull().NotEmpty().WithMessage("TemplateResultInHtml is required");
          
        }
    }

    public class TPathologyReportHeaderModell
    {
        public long PathReportId { get; set; }
        //public DateTime? PathDate { get; set; }
        //public DateTime? PathTime { get; set; }
        //public long? OpdIpdType { get; set; }
        //public long? OpdIpdId { get; set; }
        //public long? PathTestId { get; set; }
        public long? PathResultDr1 { get; set; }
        public long? PathResultDr2 { get; set; }
        public long? PathResultDr3 { get; set; }
        //public long? IsCancelled { get; set; }
        //public long? IsCancelledBy { get; set; }
        //public DateTime? IsCancelledDate { get; set; }
        //public long? AddedBy { get; set; }
        //public long? UpdatedBy { get; set; }
        //public long? ChargeId { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime? ReportTime { get; set; }
        //public string? SampleNo { get; set; }
        //public DateTime? SampleCollectionTime { get; set; }
        //public bool? IsSampleCollection { get; set; }
        public long? IsTemplateTest { get; set; }
        //public bool? TestType { get; set; }
        public string? SuggestionNotes { get; set; }
        public long? AdmVisitDoctorId { get; set; }
        public long? RefDoctorId { get; set; }
    }
    public class TPathologyReportHeaderModellValidator : AbstractValidator<TPathologyReportHeaderModell>
    {
        public TPathologyReportHeaderModellValidator()
        {
            RuleFor(x => x.ReportDate).NotNull().NotEmpty().WithMessage("ReportDate is required");
            RuleFor(x => x.ReportTime).NotNull().NotEmpty().WithMessage("ReportTime  is required");

        }
    }

}
