using FluentValidation;

namespace HIMS.API.Models.Administration
{
    public class PackageDetailsModel
    {
        public long PackageId { get; set; }
        public bool? IsPackageType { get; set; }
        public long? ServiceId { get; set; }
        public long? PackageServiceId { get; set; }
        public int? QtyLimit { get; set; }

        public decimal? Price { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }


    }
    public class PackageDetailsModel1
    {
        public long? ServiceId { get; set; }

    }
    public class PackageDetailsModelValidator : AbstractValidator<PackageDetailsModel>
    {
        public PackageDetailsModelValidator()
        {
            RuleFor(x => x.PackageServiceId).NotNull().NotEmpty().WithMessage("PackageServiceId id is required");
        }
    }
    public class PackageDetModel
    {

        public List<PackageDetailsModel> packageDetail { get; set; }
        public long? PackageTotalDays { get; set; }
        public long? PackageIcudays { get; set; }
        public decimal? PackageMedicineAmount { get; set; }
        public decimal? PackageConsumableAmount { get; set; }

    }


}
