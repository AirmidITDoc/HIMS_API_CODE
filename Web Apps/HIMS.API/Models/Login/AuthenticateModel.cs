using FluentValidation;

namespace HIMS.Api.Models.Login
{
    #pragma warning disable
    public class AuthenticateModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CaptchaCode { get; set; }
        public string CaptchaToken { get; set; }
    }
    public class AuthenticateWith2FAModel
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }

    public class ADAuthenticateModel
    {
        public string Token { get; set; }
    }
    public class RefreshAuthenticateModel
    {
        public string UserToken { get; set; }
    }
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
        public string CaptchaCode { get; set; }
        public string CaptchaToken { get; set; }
    }
    public class AuthenticateModelValidator : AbstractValidator<AuthenticateModel>
    {
        public AuthenticateModelValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("User Name is required.");
            //RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required");
        }
    }
    public class ForgotPasswordModelValidator : AbstractValidator<ForgotPasswordModel>
    {
        public ForgotPasswordModelValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Username is required"); //.EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.CaptchaCode).NotNull().NotEmpty().WithMessage("Captcha code is required.");
            RuleFor(x => x.CaptchaToken).NotNull().NotEmpty().WithMessage("Captcha token is required.");
        }
    }
}