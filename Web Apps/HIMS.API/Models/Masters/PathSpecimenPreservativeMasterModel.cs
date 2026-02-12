using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.Masters
{
    public class PathSpecimenPreservativeMasterModel
    {
        public long SpecimenPreservativeId { get; set; }
        public string? PreservativeUsed { get; set; }
        public long? UnitId { get; set; }
    }
    public class PathSpecimenPreservativeMasterModellValidator : AbstractValidator<MPathSpecimenPreservativeMaster>
    {
        public PathSpecimenPreservativeMasterModellValidator()
        {
            RuleFor(x => x.PreservativeUsed).NotNull().NotEmpty().WithMessage("PreservativeUsed is required");
        }
    }
}

