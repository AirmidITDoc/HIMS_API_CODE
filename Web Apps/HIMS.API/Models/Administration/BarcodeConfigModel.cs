using FluentValidation;

namespace HIMS.API.Models.Administration
{
    public class BarcodeConfigModel
    {
        public long Id { get; set; }
        public string TemplateCode { get; set; } = null!;
        public string Width { get; set; } = null!;
        public string Height { get; set; } = null!;
        public string TemplateBody { get; set; } = null!;
        public string? Padding { get; set; }
        public string? Margin { get; set; }
        public bool IsActive { get; set; }
        public string BarcodeData { get; set; }
    }
    public class BarcodeConfigModelValidator : AbstractValidator<BarcodeConfigModel>
    {
        public BarcodeConfigModelValidator()
        {
            RuleFor(x => x.TemplateBody).NotNull().NotEmpty().WithMessage("Template Body is required");
            RuleFor(x => x.TemplateCode).NotNull().NotEmpty().WithMessage("Template code is required");
            RuleFor(x => x.Width).NotNull().NotEmpty().WithMessage("Width is required");
            RuleFor(x => x.Height).NotNull().NotEmpty().WithMessage("Height is required");
        }
    }
}
