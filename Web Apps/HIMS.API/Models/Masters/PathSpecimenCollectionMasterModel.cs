using FluentValidation;
using HIMS.API.Validator;
using HIMS.Data.Models;

namespace HIMS.API.Models.Masters
{
    public class PathSpecimenCollectionMasterModel
    {
        public long SpecimenCollectionId { get; set; }
        public string? CollectionMethod { get; set; }
        public long? UnitId { get; set; }
    }
    public class PathSpecimenCollectionMasterModelValidator : CommonDynamicValidator<MPathSpecimenCollectionMaster>
    {
        public PathSpecimenCollectionMasterModelValidator()
        {
            RuleFor(x => x.CollectionMethod).NotNull().NotEmpty().WithMessage("CollectionMethod is required");
        }
    }
}
