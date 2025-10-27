using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Administration
{
    public class TExpenseModel
    {
        public long ExpId { get; set; }
        public DateTime? ExpDate { get; set; }
        public string? ExpTime { get; set; }
        public long? ExpHeadId { get; set; }
        public long? ExpCategoryId { get; set; }
        public byte? ExpType { get; set; }
        //public string? SequenceNo { get; set; }
        public string? VoucharNo { get; set; }
        public decimal? ExpAmount { get; set; }
        public string? PersonName { get; set; }
        public string? Narration { get; set; }
        public string? Utrno { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? CancelledDate { get; set; }

    }

    public class TExpenseModellValidator : AbstractValidator<TExpenseModel>
    {
        public TExpenseModellValidator()
        {
            RuleFor(x => x.ExpDate).NotNull().NotEmpty().WithMessage("ExpDate is required");
            RuleFor(x => x.ExpTime).NotNull().NotEmpty().WithMessage("ExpTime is required");
           

        }
    }
   
}

    