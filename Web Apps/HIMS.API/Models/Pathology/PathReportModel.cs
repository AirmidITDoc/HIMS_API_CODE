using FluentValidation;

namespace HIMS.API.Models.Pathology
{
    public class PathReportModel
    {

        public long PathReportID { get; set; }

    }
    public class PathReportModelValidator : AbstractValidator<PathReportModel>
    {
        public PathReportModelValidator()
        {
             RuleFor(x => x.PathReportID).NotNull().NotEmpty().WithMessage("PathReportID  is required");
        }
    }
}