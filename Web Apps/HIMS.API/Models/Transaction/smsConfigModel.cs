using FluentValidation;

namespace HIMS.API.Models.Transaction
{
    public class smsConfigModel
    {
        public string? Url { get; set; }
        public string? Keys { get; set; }
        public string? Campaign { get; set; }
        public long? Routeid { get; set; }
        public string? SenderId { get; set; }
        public string? UserName { get; set; }
        public string? Spassword { get; set; }
        public string? StorageLocLink { get; set; }
        public string? ConType { get; set; }
    }
    public class smsConfigModelValidator : AbstractValidator<smsConfigModel>
    {
        public smsConfigModelValidator()
        {
            RuleFor(x => x.Keys).NotNull().NotEmpty().WithMessage("Keys Date is required");
            RuleFor(x => x.Campaign).NotNull().NotEmpty().WithMessage("Campaign Time is required");

        }
    }
    public  class EmailConfigurationModel
    {
        public int Id { get; set; }
        public string? DisplayName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MailServerSmtp { get; set; }
        public short? SmtpPort { get; set; }
        public int? ServerTimeout { get; set; }
        public bool? SmtpRequiredAuthentication { get; set; }
        public bool? RequiredSquiredPasswordAuthentication { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public bool? SmtpSsl { get; set; }
    }
    public  class TMailOutgoingModel
    {
        public long Id { get; set; }
        public string? FromEmail { get; set; }
        public string? FromName { get; set; }
        public string? ToEmail { get; set; }
        public string? Cc { get; set; }
        public string? Bcc { get; set; }
        public string? MailSubject { get; set; }
        public string? MailBody { get; set; }
        public string? AttachmentName { get; set; }
        public string? AttachmentLink { get; set; }
        public long? TranNo { get; set; }
        public long? PatientId { get; set; }
        public string? EmailType { get; set; }
        public DateTime? EmailDate { get; set; }
        public int? Status { get; set; }
        public DateTime? LastTry { get; set; }
        public string? LastResponse { get; set; }
        public int? Retry { get; set; }
        public DateTime? LastSendingTry { get; set; }
       
    }
    public  class TWhatsAppSmsOutgoingModel
    {
        public long SmsoutGoingId { get; set; }
        public byte? SourceType { get; set; }
        public string? MobileNumber { get; set; }
        public string? Smsstring { get; set; }
        public string? Smstype { get; set; }
        public string? Smsflag { get; set; }
        public DateTime? Smsdate { get; set; }
        public long? PatientId { get; set; }
        public long? TranNo { get; set; }
        public long? TemplateId { get; set; }
        public string? Smsurl { get; set; }
        public string? FilePath { get; set; }
        public bool? IsSent { get; set; }
        public int? Status { get; set; }
        public DateTime? LastTry { get; set; }
        public string? LastResponse { get; set; }
        public int? Retry { get; set; }
      
    }

}

