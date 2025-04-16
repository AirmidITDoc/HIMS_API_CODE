using FluentValidation;
using HIMS.API.Models.Administration;
using HIMS.Data.Models;

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
            //   RuleFor(x => x.IsCancelledBy).NotNull().NotEmpty().WithMessage("IsCancelledBy id is required");
            //  RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");
        }
    }
}