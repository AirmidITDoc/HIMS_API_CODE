using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.OTManagement
{
    public class OTCheckListModel
    {
        public long OtcheckListId { get; set; }
        public long Opipid { get; set; }
        public byte Opiptype { get; set; }
        public long CheckListDescriptionId { get; set; }
        public string CheckListDescription { get; set; } = null!;
        public bool Status { get; set; }
        public string? Remark { get; set; }


    }
    public class OTCheckListModelValidator : CommonDynamicValidator<OTCheckListModel>
    {
        public OTCheckListModelValidator()
        {

            RuleFor(x => x.Opipid).NotNull().NotEmpty().WithMessage("Opipid is required");
            RuleFor(x => x.CheckListDescriptionId).NotNull().NotEmpty().WithMessage("CheckListDescriptionId is required");
            RuleFor(x => x.CheckListDescription).NotNull().NotEmpty().WithMessage("CheckListDescription is required");
            RuleFor(x => x.Opiptype).NotNull().NotEmpty().WithMessage("Opiptype is required");

        }
    }
}
