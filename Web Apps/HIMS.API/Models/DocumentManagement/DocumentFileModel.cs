using FluentValidation;
using HIMS.API.Models.Common;
using HIMS.Data.Models;

namespace HIMS.API.Models.DocumentManagement
{
    public class DocumentFileModel
    {
        public long Id { get; set; }
        public long AdmissionId { get; set; }
        public long DocCatId { get; set; }
        public IFormFile? Document { get; set; }
        public long DocAdmissionId { get; set; }
        public string OrgFileName { get; set; } = null!;
        public string SavedFileName { get; set; } = null!;
        public string? FileTags { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DocNo { get; set; } = null!;
        public bool IsDelete {  get; set; }
    }
    public class DocumentFileModelValidator : AbstractValidator<DocumentFileModel>
    {
        public DocumentFileModelValidator()
        {
            //RuleFor(x => x.Document).NotNull().WithMessage("Document is required").When(x => x.Id == 0);
            RuleFor(x => x.OrgFileName).NotNull().NotEmpty().WithMessage("DocName is required");
        }
    }
}
