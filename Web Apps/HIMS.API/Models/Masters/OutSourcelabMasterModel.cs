using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class OutSourcelabMasterModel
    {
        public long OutSourceId { get; set; }
        public string? OutSourceLabName { get; set; }
        public string? ContactPersonName { get; set; }
        public long? MobileNo { get; set; }
        public string? Address { get; set; }
    }
    public class OutSourcelabMasterModelValidator : AbstractValidator<OutSourcelabMasterModel>
    {
        public OutSourcelabMasterModelValidator()
        {
            RuleFor(x => x.OutSourceLabName).NotNull().NotEmpty().WithMessage("OutSourceLabName  is required");
            RuleFor(x => x.ContactPersonName).NotNull().NotEmpty().WithMessage("ContactPersonName  is required");
            RuleFor(x => x.MobileNo).NotNull().NotEmpty().WithMessage("MobileNo  is required");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address  is required");
        }
    }
}
