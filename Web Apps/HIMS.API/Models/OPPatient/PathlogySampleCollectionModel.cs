using FluentValidation;

namespace HIMS.API.Models.OPPatient
{
    public class PathlogySampleCollectionModel
    {
        public long PathReportId { get; set; }
        public string? SampleCollectionTime { get; set; }
        public bool? IsSampleCollection { get; set; }
        public long? SampleNo { get; set; }
        public long? SampleCollectedBy { get; set; }

    }
    public class PathlogySampleCollectionModelValidator : AbstractValidator<PathlogySampleCollectionModel>
    {
        public PathlogySampleCollectionModelValidator()
        {
            RuleFor(x => x.PathReportId).NotNull().NotEmpty().WithMessage("PathReportId is required");
            RuleFor(x => x.SampleCollectionTime).NotNull().NotEmpty().WithMessage("PathTime is required");
            RuleFor(x => x.SampleCollectedBy).NotNull().NotEmpty().WithMessage("SampleCollectedBy is required");

        }
    }

    public class PathlogySampleCollectionsModel
    {
        public List<PathlogySampleCollectionModel> PathlogySampleCollection { get; set; }

    }
}
