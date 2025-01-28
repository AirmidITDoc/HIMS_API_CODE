using FluentValidation;

namespace HIMS.API.Models.Inventory.Masters
{
    public class MReportConfigModel 
    {
        public long ReportId { get; set; }
        public string? ReportSection { get; set; }
        public string? ReportName { get; set; }
        public long? Parentid { get; set; }
        public string? ReportTitle { get; set; }
        public string? ReportHeader { get; set; }
        public string? ReportColumn { get; set; }
        public string? ReportHeaderFile { get; set; }
        public string? ReportBodyFile { get; set; }
        public string? ReportFolderName { get; set; }
        public string? ReportFileName { get; set; }
        public string? ReportSpname { get; set; }
        public string? ReportPageOrientation { get; set; }
        public string? ReportPageSize { get; set; }
     //  public DateTime? CreatedOn { get; set; }
  //      public long? UpdateBy { get; set; }
 //       public DateTime? UpdatedOn { get; set; }

    }
    public class MReportConfigModelModelValidator : AbstractValidator<MReportConfigModel>
    {
        public MReportConfigModelModelValidator()
        {
            RuleFor(x => x.ReportSection).NotNull().NotEmpty().WithMessage("ReportSection is required");
            RuleFor(x => x.ReportName).NotNull().NotEmpty().WithMessage("ReportName is required");  

        }
    }
}
