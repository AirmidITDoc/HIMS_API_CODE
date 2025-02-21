using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Inventory
{
    public class LoginManagerModel
    {
        public long UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public long? RoleId { get; set; }
        public long? StoreId { get; set; }
        public bool? IsDoctorType { get; set; }
        public long? DoctorId { get; set; }
        public bool? IsPoverify { get; set; }
        public bool? IsGrnverify { get; set; }
        public bool? IsCollection { get; set; }
        public bool? IsBedStatus { get; set; }
        public bool? IsCurrentStk { get; set; }
        public bool? IsPatientInfo { get; set; }
        public bool? IsDateInterval { get; set; }
        public int? IsDateIntervalDays { get; set; }
        public string? MailId { get; set; }
        public string? MailDomain { get; set; }
        public bool? LoginStatus { get; set; }
        public bool? AddChargeIsDelete { get; set; }
        public bool? IsIndentVerify { get; set; }
        public bool? IsPoinchargeVerify { get; set; }
        public bool? IsRefDocEditOpt { get; set; }
        public bool? IsInchIndVfy { get; set; }
        public long? WebRoleId { get; set; }
        public string? UserToken { get; set; }
        public int? PharExtOpt { get; set; }
        public int? PharOpopt { get; set; }
        public int? PharIpopt { get; set; }
        public long? UnitId { get; set; }
        public long? MobileNo { get; set; }
        public int? IsDiscApply { get; set; }
        public int? DiscApplyPer { get; set; }
    }
    public class LoginManagerModelValidator : AbstractValidator<LoginManagerModel>
    {
        public LoginManagerModelValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName Type is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName Type is required");
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("UserName Type is required");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password Type is required");
            RuleFor(x => x.RoleId).NotNull().NotEmpty().WithMessage("RoleId Type is required");
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId Type is required");
            RuleFor(x => x.IsDoctorType).NotNull().NotEmpty().WithMessage("IsDoctorType Type is required");
            RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithMessage("DoctorId Type is required");
            RuleFor(x => x.IsPoverify).NotNull().NotEmpty().WithMessage("IsPoverify Type is required");
            RuleFor(x => x.IsCollection).NotNull().NotEmpty().WithMessage("IsCollection Type is required");
            RuleFor(x => x.IsBedStatus).NotNull().NotEmpty().WithMessage("IsBedStatus Type is required");
            RuleFor(x => x.MailId).NotNull().NotEmpty().WithMessage("MailId Type is required");
            RuleFor(x => x.MailDomain).NotNull().NotEmpty().WithMessage("MailDomain Type is required");
            RuleFor(x => x.LoginStatus).NotNull().NotEmpty().WithMessage("LoginStatus Type is required");
           
        }
    }
    public class loginCancel
    {
        public long UserId { get; set; }

    }
    public class ChangePassword
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
