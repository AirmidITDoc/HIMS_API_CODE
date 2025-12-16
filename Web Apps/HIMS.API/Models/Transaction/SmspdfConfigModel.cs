using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Transaction
{
    public class SmspdfConfigModel
    {
        public long Smsid { get; set; }
        public string? Type { get; set; }
        public string? PdfModeName { get; set; }
        public string? FieldName { get; set; }
        public bool? PasswordProtectedPdf { get; set; }
    }
    public class SmspdfConfigModelValidator : AbstractValidator<SmspdfConfigModel>
    {
        public SmspdfConfigModelValidator()
        {
            RuleFor(x => x.PdfModeName).NotNull().NotEmpty().WithMessage("PdfModeName  is required");
            RuleFor(x => x.FieldName).NotNull().NotEmpty().WithMessage("FieldName  is required");

        }
    }
}
