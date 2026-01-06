using FluentValidation;
using HIMS.API.Models.Masters;
using HIMS.Data.Models;
using static HIMS.API.Models.GastrologyMasterModel.MSubQuestionMasterModel;

namespace HIMS.API.Models.GastrologyMasterModel
{
    public class QuestionMasterModel
    {
        public long QuestionId { get; set; }
        public string? QuestionName { get; set; }
        public string? ShortCutValues { get; set; }
        //public List<MSubQuestionMasterModel> MSubQuestionMasters { get; set; }
        //public List<MSubQuestionValuesMasterModel> MSubQuestionValuesMasters { get; set; }
    }
    public class QuestionMasterModelValidator : AbstractValidator<QuestionMasterModel>
    {
        public QuestionMasterModelValidator()
        {
            RuleFor(x => x.QuestionName).NotNull().NotEmpty().WithMessage("QuestionName is required");
            RuleFor(x => x.ShortCutValues).NotNull().NotEmpty().WithMessage("ShortCutValues is required");


        }
    }
    public class MSubQuestionMasterModel
    {
        public long SubQuestionId { get; set; }
        public long? QuestionId { get; set; }
        public string? SubQuestionName { get; set; }
        public long? SequenceNo { get; set; }
        public string? ResultValues { get; set; }
        public List<MSubQuestionValuesMasterModel> MSubQuestionValuesMasters { get; set; }


        public class MSubQuestionMasterModelValidator : AbstractValidator<MSubQuestionMasterModel>
        {
            public MSubQuestionMasterModelValidator()
            {
                RuleFor(x => x.SubQuestionName).NotNull().NotEmpty().WithMessage("SubQuestionName is required");
                RuleFor(x => x.ResultValues).NotNull().NotEmpty().WithMessage("ResultValues is required");


            }
        }
        public class MSubQuestionValuesMasterModel
        {
            public long SubQuestionValId { get; set; }
            public long? SubQuestionId { get; set; }
            public string? SubQuestionValName { get; set; }
            public long? SequenceNo { get; set; }
            public string? ResultValues { get; set; }
            public string? ShortcutValues { get; set; }

            public class MSubQuestionValuesMasterModelValidator : AbstractValidator<MSubQuestionValuesMasterModel>
            {
                public MSubQuestionValuesMasterModelValidator()
                {
                    RuleFor(x => x.SubQuestionValName).NotNull().NotEmpty().WithMessage("SubQuestionValName is required");
                    RuleFor(x => x.ResultValues).NotNull().NotEmpty().WithMessage("ResultValues is required");
                    RuleFor(x => x.ShortcutValues).NotNull().NotEmpty().WithMessage("ShortcutValues is required");
                }
            }
        }
    }
}

