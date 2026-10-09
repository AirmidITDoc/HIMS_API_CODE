using FluentValidation;

namespace HIMS.API.Models.Transaction
{
    public class MSmsModuleWiseConfigurationModel
    {
        public int SmsConfigId { get; set; }
        public int MenuId { get; set; }
        public string ModuleName { get; set; } = null!;
        public bool IsWhatsApp { get; set; }
        public string? WhatsAppFormat { get; set; }
        public bool IsEmail { get; set; }
        public string? EmailFormat { get; set; }
        public bool IsSms { get; set; }
        public string? SmsFormat { get; set; }
       
    }
    public class MSmsModuleWiseConfigurationModelValidator : AbstractValidator<MSmsModuleWiseConfigurationModel>
    {
        public MSmsModuleWiseConfigurationModelValidator()
        {
            RuleFor(x => x.MenuId).NotNull().NotEmpty().WithMessage("PdfModeName  is required");
        ///    RuleFor(x => x.ModuleName).NotNull().NotEmpty().WithMessage("FieldName  is required");

        }
    }
}
