using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class PrescriptionOPTemplateModel
    {
        public long PresId { get; set; }
        public string? PresTemplateName { get; set; }
        public bool? IsActive { get; set; }
        public byte? OpIpType { get; set; }
        public string? TemplateCategory { get; set; }
        public long? IsAddBy { get; set; }
        public long? IsUpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        //public List<PresTemplateDModel> MPresTemplateDs { get; set; }
    }
    public class PrescriptionOPTemplateModelValidator : AbstractValidator<PrescriptionOPTemplateModel>
    {
        public PrescriptionOPTemplateModelValidator()
        {
            RuleFor(x => x.PresTemplateName).NotNull().NotEmpty().WithMessage("PresTemplateName is required");

        }
    }
    public class PresTemplateDModel
    {
        public long? PresId { get; set; }
        public string? Date { get; set; }
        public long? ClassId { get; set; }
        public long? GenericId { get; set; }
        public long? DrugId { get; set; }
        public long? DoseId { get; set; }
        public long? Days { get; set; }
        public long? InstructionId { get; set; }
        public double? QtyPerDay { get; set; }
        public double? TotalQty { get; set; }
        public string? Instruction { get; set; }
        public string? Remark { get; set; }
        public bool? IsEnglishOrIsMarathi { get; set; }
    }
    public class PresTemplateDModelValidator : AbstractValidator<PresTemplateDModel>
    {
        public PresTemplateDModelValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date is required");


        }
    }
    public class PreTemplateModel
    {
        public PrescriptionOPTemplateModel PrescriptionOPTemplate { get; set; }
        public List<PresTemplateDModel> PresTemplate { get; set; }

    }

}
