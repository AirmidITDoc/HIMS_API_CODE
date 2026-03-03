using FluentValidation;

namespace HIMS.API.Models.Pathology
{
    public class ReportLogModel
    {
        public long LogId { get; set; }
        public long? Opipid { get; set; }
        public long? Opiptype { get; set; }
        public long? LogTypeId { get; set; }
        public string? LogTypeName { get; set; }
    }
    public class ReportLogModelValidator : AbstractValidator<ReportLogModel>
    {
        public ReportLogModelValidator()
        {
            RuleFor(x => x.LogTypeName).NotNull().NotEmpty().WithMessage("LogType is required");
        }
    }
}
