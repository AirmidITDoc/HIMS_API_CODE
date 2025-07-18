using FluentValidation;
using System.ComponentModel;

namespace HIMS.API.Models.Common
{
    public class FileModel
    {
        public long Id { get; set; }
        public long? RefId { get; set; }
        public PageNames RefType { get; set; }
        public IFormFile Document { get; set; }
        public string? DocName { get; set; }
        public string? DocSavedName { get; set; }
        public bool? IsDelete { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class FileModelValidator : AbstractValidator<FileModel>
    {
        public FileModelValidator()
        {
            RuleFor(x => x.Document).NotNull().WithMessage("Document is required");
            RuleFor(x => x.DocName).NotNull().NotEmpty().WithMessage("DocName is required");
        }
    }
    public enum PageNames
    {
        [Description("Doctors\\Files")]
        Doctor = 1
    }
}
