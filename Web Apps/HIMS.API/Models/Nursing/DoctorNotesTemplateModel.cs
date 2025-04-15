using FluentValidation;

namespace HIMS.API.Models.Nursing
{
    public class DoctorNotesTemplateModel
    {
        public long DocNoteTempId { get; set; }
        public string? DocsTempName { get; set; }
        public string? TemplateDesc { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
    public class DoctorNotesTemplateModelValidator : AbstractValidator<DoctorNotesTemplateModel>
    {
        public DoctorNotesTemplateModelValidator()
        {
            RuleFor(x => x.DocsTempName).NotNull().NotEmpty().WithMessage("DocsTempName is required");
            RuleFor(x => x.TemplateDesc).NotNull().NotEmpty().WithMessage("TemplateDesc is required");

        }
    }
}
