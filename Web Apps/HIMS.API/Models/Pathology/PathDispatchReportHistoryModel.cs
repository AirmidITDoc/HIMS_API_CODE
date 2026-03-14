using FluentValidation;
using HIMS.API.Models.Masters;
using HIMS.Data.Models;

namespace HIMS.API.Models.Pathology
{
    public class PathDispatchReportHistoryModel
    {
        public long DispatchId { get; set; }
        public long? LabPatientId { get; set; }
        public long? UnitId { get; set; }
        public long? DispatchModeId { get; set; }
        public string? Comments { get; set; }
        public long? DispatchBy { get; set; }
        public DateTime? DispatchOn { get; set; }
        public long? RefDocId { get; set; }

        public List<TPathDispatchReportHistoryDetailModel> TPathDispatchReportHistoryDetails { get; set; }
    }
    public class PathDispatchReportHistoryModelValidator : AbstractValidator<PathDispatchReportHistoryModel>
    {
        public PathDispatchReportHistoryModelValidator()
        {
            RuleFor(x => x.LabPatientId).NotNull().NotEmpty().WithMessage("LabPatientId  is required");
            RuleFor(x => x.DispatchModeId).NotNull().NotEmpty().WithMessage("DispatchModeId  is required");

        }
    }
    public  class TPathDispatchReportHistoryDetailModel
    {
        public long DispatchDetailId { get; set; }
        public long? DispatchId { get; set; }
        public long? TestId { get; set; }
        
    }

}
