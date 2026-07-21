using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class HSNCodeMasterModel
    {
        public long HsncodeId { get; set; }
        public string HsncodeName { get; set; } = null!;
        public double GstRate { get; set; }
        public long? UnitOfMeasureId { get; set; }
        public long? GstId { get; set; }
        public bool? IsActive { get; set; }
        public string UnitOfMeasure { get; set; } = null!;
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
    }
    public class HSNCodeMasterModelValidator : AbstractValidator<HSNCodeMasterModel>
    {
        public HSNCodeMasterModelValidator()
        {
            RuleFor(x => x.HsncodeName).NotNull().NotEmpty().WithMessage("HsncodeName  is required");
            RuleFor(x => x.GstRate).NotNull().NotEmpty().WithMessage("GstRate  is required");
            RuleFor(x => x.UnitOfMeasureId).NotNull().NotEmpty().WithMessage("UnitOfMeasureId  is required");
            RuleFor(x => x.UnitOfMeasure).NotNull().NotEmpty().WithMessage("UnitOfMeasure  is required");     
            RuleFor(x => x.EffectiveFrom).NotNull().NotEmpty().WithMessage("EffectiveFrom  is required");
            RuleFor(x => x.EffectiveTo).NotNull().NotEmpty().WithMessage("EffectiveTo  is required");


        }
    }
}
