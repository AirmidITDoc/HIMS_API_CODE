using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.FeedBack
{
    public class PatientFeedbackModel
    {
        public long PatientFeedbackId { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? DepartmentId { get; set; }
        public long? FeedbackQuestionId { get; set; }
        public long? FeedbackRating { get; set; }
        public string? FeedbackComments { get; set; }

    }
    public class PatientFeedbackModelValidator : AbstractValidator<PatientFeedbackModel>
    {
        public PatientFeedbackModelValidator()
        {
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("DepartmentId  is required");
            RuleFor(x => x.FeedbackComments).NotNull().NotEmpty().WithMessage("FeedbackComments  is required");
            RuleFor(x => x.FeedbackRating).NotNull().NotEmpty().WithMessage("FeedbackRating  is required");


        }
    }
}
