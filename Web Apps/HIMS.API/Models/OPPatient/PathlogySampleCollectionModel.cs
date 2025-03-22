using FluentValidation;
using HIMS.API.Models.IPPatient;

namespace HIMS.API.Models.OPPatient
{
    public class PathlogySampleCollectionModel
    {
        public long PathReportId { get; set; }
        //public DateTime? PathDate { get; set; }
        public string? SampleCollectionTime { get; set; }
        public bool? IsSampleCollection { get; set; }
        public long? SampleNo { get; set; }

    }
    public class PathlogySampleCollectionModelValidator : AbstractValidator<PathlogySampleCollectionModel>
    {
        public PathlogySampleCollectionModelValidator()
        {
            RuleFor(x => x.PathReportId).NotNull().NotEmpty().WithMessage("PathReportId is required");
            RuleFor(x => x.SampleCollectionTime).NotNull().NotEmpty().WithMessage("PathTime is required");

        }
    }

    public class PathlogySampleCollectionsModel
    {
        public List<PathlogySampleCollectionModel> PathlogySampleCollection { get; set; }
      
    }
}
