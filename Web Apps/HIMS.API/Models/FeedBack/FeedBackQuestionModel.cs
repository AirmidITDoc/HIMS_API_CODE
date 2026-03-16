using FluentValidation;

namespace HIMS.API.Models.FeedBack
{
    public class FeedBackQuestionModel
    {
        public long FeedbackId { get; set; }
        public string? FeedbackQuestion { get; set; }
        public string? FeedbackQuestionMarathi { get; set; }
        public long? DepartmentId { get; set; }
        public long? SequanceId { get; set; }

    }
    public class FeedBackQuestionModelValidator : AbstractValidator<FeedBackQuestionModel>
    {
        public FeedBackQuestionModelValidator()
        {
            RuleFor(x => x.FeedbackQuestion).NotNull().NotEmpty().WithMessage("FeedbackQuestion  is required");
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("DepartmentId  is required");


        }
    }
}
