using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.OutPatient
{
    public class GastrologyEMRModel
    {
        public long ClinicalQuesHeaderId { get; set; }
        public DateTime? ClinicalQuesDate { get; set; }
        public string? ClinicalQuesTime { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public long? QuestionId { get; set; }
        public string? QuestionName { get; set; }
        public List<ClinicalQuesDetailModel> ClinicalQuesDetails { get; set; }
        public class GastrologyEMRModelValidator : AbstractValidator<GastrologyEMRModel>
        {
            public GastrologyEMRModelValidator()
            {
                RuleFor(x => x.QuestionName).NotNull().NotEmpty().WithMessage("QuestionName is required");
                RuleFor(x => x.QuestionId).NotNull().NotEmpty().WithMessage("QuestionId is required");
              

            }
        }

    }
    public class ClinicalQuesDetailModel
    {
        public long ClinicalQuesDetId { get; set; }
        public long? ClinicalQuesHeaderId { get; set; }
        public long? SubQuesId { get; set; }
        public string? SubQuesName { get; set; }
        public string? ResultEntry { get; set; }
        public long? SeqNo { get; set; }
    }
    public class ClinicalQuesDetailModelValidator : AbstractValidator<ClinicalQuesDetailModel>
    {
        public ClinicalQuesDetailModelValidator()
        {
            RuleFor(x => x.SubQuesName).NotNull().NotEmpty().WithMessage("SubQuesName is required");
            RuleFor(x => x.ResultEntry).NotNull().NotEmpty().WithMessage("ResultEntry is required");
          

        }
    }
    public class ClinicalQuesHeaderCancel
    {
        public long? Opipid { get; set; }
        public long ClinicalQuesHeaderId { get; set; }

    }
}
