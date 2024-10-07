using FluentValidation;
using HIMS.API.Models.OPPatient;

namespace HIMS.API.Models.Nursing
{
    public class IPLabRequestModel
    {
        public long RequestId { get; set; }
        public DateTime? ReqDate { get; set; }
        public string? ReqTime { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? IsAddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public DateTime? IsCancelledTime { get; set; }
        public byte? IsType { get; set; }
        public bool? IsOnFileTest { get; set; }
    }

    public class IPLabRequestModelValidator : AbstractValidator<IPLabRequestModel>
    {
        public IPLabRequestModelValidator()
        {
            //    RuleFor(x => x.OPDIPDID).NotNull().NotEmpty().WithMessage("OPDIPDID is required");
            //    RuleFor(x => x.TotalAmt).NotNull().NotEmpty().WithMessage("TotalAmt is required");
            //    RuleFor(x => x.NetPayableAmt).NotNull().NotEmpty().WithMessage("NetPayableAmt is required");
            //    RuleFor(x => x.UnitId).NotNull().NotEmpty().WithMessage("UnitId is required");
            //    RuleFor(x => x.TariffId).NotNull().NotEmpty().WithMessage("TariffId is required");
        }
    }

    public class TDLabRequest
    {
        public long ReqDetId { get; set; }
        public long? RequestId { get; set; }
        public long? ServiceId { get; set; }
        public decimal? Price { get; set; }
        public bool? IsStatus { get; set; }
        public long? AddedBillingId { get; set; }
        public DateTime? AddedByDate { get; set; }
        public string? AddedByTime { get; set; }
        public long? CharId { get; set; }
        public bool? IsTestCompted { get; set; }
        public bool? IsOnFileTest { get; set; }
    }

    public class TDLabRequestModelValidator : AbstractValidator<TDLabRequest>
    {
        public TDLabRequestModelValidator()
        {
            //RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            //RuleFor(x => x.ChargesId).NotNull().NotEmpty().WithMessage("ChargesId is required");
        }
    }
}
