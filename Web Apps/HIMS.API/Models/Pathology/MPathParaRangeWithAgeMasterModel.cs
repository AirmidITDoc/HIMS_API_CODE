using FluentValidation;

namespace HIMS.API.Models.Pathology
{
    public class MPathParaRangeWithAgeMasterModel
    {
        public long PathparaRangeId { get; set; }
        public long? ParaId { get; set; }
        public long? SexId { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string? AgeType { get; set; }
        public string? MinValue { get; set; }
        public string? MaxValue { get; set; }

    }
    public class MPathParaRangeWithAgeMasterModelValidator : AbstractValidator<MPathParaRangeWithAgeMasterModel>
    {
        public MPathParaRangeWithAgeMasterModelValidator()
        {
            RuleFor(x => x.ParaId).NotNull().NotEmpty().WithMessage("ParaId is required");
            RuleFor(x => x.SexId).NotNull().NotEmpty().WithMessage("SexId is required");
            RuleFor(x => x.MinAge).NotNull().NotEmpty().WithMessage(" MinAge is required");
            
        }
    }
}
