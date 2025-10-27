using FluentValidation;

namespace HIMS.API.Models.Login
{
    public class TProcessOtpModel
    {
        public long MsgId { get; set; }
        public long? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? Message { get; set; }
        public string? Otpno { get; set; }
        public bool? IsVerified { get; set; }

    }
    public class TProcessOtpModelValidator : AbstractValidator<TProcessOtpModel>
    {
        public TProcessOtpModelValidator()
        {
            RuleFor(x => x.MobileNo).NotNull().NotEmpty().WithMessage("MobileNo  is required");
            RuleFor(x => x.Message).NotNull().NotEmpty().WithMessage("Message  is required");
            RuleFor(x => x.EmailId).NotNull().NotEmpty().WithMessage("EmailId  is required");
            RuleFor(x => x.Otpno).NotNull().NotEmpty().WithMessage("Otpno  is required");


        }
    }
    public class TProcessOtpUpdateModel
    {
        public long MsgId { get; set; }
        public bool? IsVerified { get; set; }

    }
}
