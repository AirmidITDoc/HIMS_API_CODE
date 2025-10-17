using FluentValidation;
using System.ComponentModel;

namespace HIMS.API.Models.Common
{
    public class FileModel
    {
        public long Id { get; set; }
        public int SrNo { get; set; }
        public long? RefId { get; set; }
        public PageNames RefType { get; set; }
        public IFormFile? Document { get; set; }
        public string? DocName { get; set; }
        public string? DocSavedName { get; set; }
        public string? Base64 { get; set; }
        public bool? IsDelete { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class FileModelValidator : AbstractValidator<FileModel>
    {
        public FileModelValidator()
        {
            //RuleFor(x => x.Document).NotNull().WithMessage("Document is required").When(x => x.Id == 0);
            RuleFor(x => x.DocName).NotNull().NotEmpty().WithMessage("DocName is required");
        }
    }
    public enum PageNames
    {
        [Description("Doctors\\Files")]
        Doctor = 1,
        [Description("Doctors\\Signature")]
        Doctor_Signature = 2,
        [Description("Patient\\Photo")]
        Patient_Photo = 3,
        [Description("Patient\\Signature")]
        Patient_Signature = 4,
        [Description("Patient\\Files")]
        Patient_Files = 5,
        [Description("Patient\\PathFiles")]
        Patient_PathFiles = 6,
        [Description("Hospital\\Logo")]
        Logo = 7,
        [Description("User\\Signature")]
        User_Signature = 8
    }
}
