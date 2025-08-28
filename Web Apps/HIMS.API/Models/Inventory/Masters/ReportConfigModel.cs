using FluentValidation;
using static HIMS.API.Models.Inventory.Masters.ReportConfigModelModelValidator;

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
        public string? SummaryLabel { get; set; }
        public string? ReportHeaderFile { get; set; }
        public string? ReportBodyFile { get; set; }
        public string? ReportFolderName { get; set; }
        public string? ReportFileName { get; set; }
        public string? ReportSpname { get; set; }
        public string? ReportPageOrientation { get; set; }
        public string? ReportPageSize { get; set; }
        public string? ReportColumnWidths { get; set; }
        public string? ReportFilter { get; set; }
        public string? ReportSummary { get; set; }
        public bool? IsActive { get; set; }
        public long? MenuId { get; set; }
        public List<ReportConfigDetailsModel> MReportConfigDetails { get; set; }


    }
    public class ReportConfigModelModelValidator : AbstractValidator<ReportConfigModel>
    {
        public ReportConfigModelModelValidator()
        {
            RuleFor(x => x.ReportSection).NotNull().NotEmpty().WithMessage("ReportSection is required");
            RuleFor(x => x.ReportName).NotNull().NotEmpty().WithMessage("ReportName is required");
            RuleFor(x => x.ReportMode).NotNull().NotEmpty().WithMessage("ReportMode is required");
            RuleFor(x => x.ReportTitle).NotNull().NotEmpty().WithMessage("ReportTitle is required");
         //   RuleFor(x => x.ReportHeader).NotNull().NotEmpty().WithMessage("ReportHeader is required");


        }
        public class ReportConfigDetailsModel
        {
            public long ReportColId { get; set; }
            public long? ReportId { get; set; }
            public bool? IsDisplayColumn { get; set; }
            public string? ReportHeader { get; set; }
            public string? ReportColumn { get; set; }
            public string? ReportColumnWidth { get; set; }
            public string? ReportColumnAligment { get; set; }
            public string? ReportTotalField { get; set; }
            public string? ReportGroupByLabel { get; set; }
            public int? ReportGroupBySequenceNo { get; set; }
            public string? SummaryLabel { get; set; }
            public int? SummarySequenceNo { get; set; }
            public long? SequenceNo { get; set; }
            public string? ProcedureName { get; set; }
        }

    }

}
