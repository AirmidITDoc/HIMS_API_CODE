using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Administration
{
    public class TExpenseModel
    {
        public long ExpId { get; set; }
        public DateTime? ExpDate { get; set; }
        public string? ExpTime { get; set; }
        public byte? ExpType { get; set; }
        public decimal? ExpAmount { get; set; }
        public string? PersonName { get; set; }
        public string? Narration { get; set; }
        public long? IsAddedby { get; set; }
        public long? IsUpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public string? VoucharNo { get; set; }
        public long? ExpHeadId { get; set; }
    }

    public class TExpenseModellValidator : AbstractValidator<TExpenseModel>
    {
        public TExpenseModellValidator()
        {
            RuleFor(x => x.ExpType).NotNull().NotEmpty().WithMessage("ExpType id is required");
            RuleFor(x => x.PersonName).NotNull().NotEmpty().WithMessage("PersonName`  is required");
        }
    }
   
}

    