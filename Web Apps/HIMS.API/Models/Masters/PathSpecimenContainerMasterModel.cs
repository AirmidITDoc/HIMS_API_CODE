using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.Masters
{
    public class PathSpecimenContainerMasterModel
    {
        public long SpecimenContainerId { get; set; }
        public string? ContainerType { get; set; }
        public long? UnitId { get; set; }
    }
    public class PathSpecimenContainerMasterModelValidator : AbstractValidator<MPathSpecimenContainerMaster>
    {
        public PathSpecimenContainerMasterModelValidator()
        {
            RuleFor(x => x.ContainerType).NotNull().NotEmpty().WithMessage("ContainerType is required");
        }
    }
}
