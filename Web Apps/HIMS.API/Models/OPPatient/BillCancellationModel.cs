using FluentValidation;

namespace HIMS.API.Models.OPPatient
{
    public class OpBillCancellationModel
    {
        public long BillNo { get; set; }
        public long IsCancelledBy { get; set; }
        public DateTime IsCancelledDatetime { get; set; }
        public string? CancelReason { get; set; }



    }
    public class OpBillCancellationModelValidator : AbstractValidator<OpBillCancellationModel>
    {
        public OpBillCancellationModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            RuleFor(x => x.IsCancelledDatetime).NotNull().NotEmpty().WithMessage("IsCancelledDatetime is required");
            RuleFor(x => x.CancelReason).NotNull().NotEmpty().WithMessage("CancelReason is required");


        }
    }

}
