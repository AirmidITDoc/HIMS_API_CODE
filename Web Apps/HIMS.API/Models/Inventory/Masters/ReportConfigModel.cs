using FluentValidation;

namespace HIMS.API.Models.Inventory.Masters
{
    public class ReportConfigModel
    {
        public long ReportId { get; set; }
        public string? ReportSection { get; set; }
        public string? ReportName { get; set; }
        public long? Parentid { get; set; }
        public string? ReportMode { get; set; }
        public string? ReportTitle { get; set; }
        public string? ReportHeader { get; set; }
        public string? ReportColumn { get; set; }
        public string? ReportTotalField { get; set; }
        public string? ReportGroupByLabel { get; set; }
        public string? summaryLabel { get; set; }
        public string? ReportHeaderFile { get; set; }
        public string? ReportBodyFile { get; set; }
        public string? ReportFolderName { get; set; }
        public string? ReportFileName { get; set; }
        public string? ReportSpname { get; set; }
        public string? ReportPageOrientation { get; set; }
        public string? ReportColumnWidths { get; set; }
        public string? ReportPageSize { get; set; }
        public string? ReportFilter { get; set; }
        public long? MenuId { get; set; }
    }
    public class ReportConfigModelModelValidator : AbstractValidator<ReportConfigModel>
    {
        public ReportConfigModelModelValidator()
        {
            RuleFor(x => x.ReportSection).NotNull().NotEmpty().WithMessage("ReportSection is required");
            RuleFor(x => x.ReportName).NotNull().NotEmpty().WithMessage("ReportName is required");

        }
    }

}
