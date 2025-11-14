using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.Administration
{
    public class WhatsAppModel
    {
        public string MobileNumber { get; set; }
        public string Smsstring { get; set; }
        public bool IsSent { get; set; }
        public string? Smstype { get; set; }
        public string? Smsflag { get; set; }
        public DateTime? Smsdate { get; set; }
        public long? TranNo { get; set; }
        public int TemplateId { get; set; }
        public string? Smsurl { get; set; }
        public string? FilePath { get; set; }
        public byte? SourceType { get; set; }
        public long? CreatedBy { get; set; }
        public long SmsoutGoingId { get; set; }
        public int BillId { get; set; }
    }


    public class WhatsAppModelValidator : AbstractValidator<TWhatsAppSmsOutgoing>
    {
        public WhatsAppModelValidator()
        {
            RuleFor(x => x.SmsoutGoingId).NotNull().NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.MobileNumber).NotNull().NotEmpty().WithMessage("Mobile Number is required");


        }
    }
    public class EamilModel
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ToEmail { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public int Status { get; set; }
        public int Retry { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentLink { get; set; }
        public int Id { get; set; }
        public int TranNo { get; set; }
        public string EmailType { get; set; }

    }

    public class EamilModelValidator : AbstractValidator<TMailOutgoing>
    {
        public EamilModelValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.FromEmail).NotNull().NotEmpty().WithMessage("Email Id is required");


        }
    }

}

