using FluentValidation;
using HIMS.API.Models.IPPatient;

namespace HIMS.API.Models.OPPatient
{
    public class PathlogySampleCollectionModel
    {
        public long PathReportId { get; set; }
        public string? PathDate { get; set; }
        public string? PathTime { get; set; }
        public bool? IsSampleCollection { get; set; }
        public string? SampleNo { get; set; }

    }
    public class PathlogySampleCollectionModelValidator : AbstractValidator<PathlogySampleCollectionModel>
    {
        public PathlogySampleCollectionModelValidator()
        {
            RuleFor(x => x.PathDate).NotNull().NotEmpty().WithMessage("PathDate is required");
            RuleFor(x => x.PathTime).NotNull().NotEmpty().WithMessage("PathTime is required");

        }
    }
}
