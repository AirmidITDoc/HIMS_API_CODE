using FluentValidation;
using HIMS.API.Models.IPPatient;

namespace HIMS.API.Models.Nursing
{
    public class CanteenRequestModel
    {
        public long ReqId { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public string? ReqNo { get; set; }
        public long? OpIpId { get; set; }
        public long? OpIpType { get; set; }
        public long? WardId { get; set; }
        public long? CashCounterId { get; set; }
        public bool? IsFree { get; set; }
        public long? UnitId { get; set; }
        public bool? IsBillGenerated { get; set; }
        public bool? IsPrint { get; set; }
        //public  List<TCanteenRequestDetailModel> canteenRequestDetails { get; set; }
    }
    public class CanteenRequestModelValidator : AbstractValidator<CanteenRequestModel>
    {
        public CanteenRequestModelValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date Date is required");
            RuleFor(x => x.Time).NotNull().NotEmpty().WithMessage("Time Time is required");

        }
    }
    public  class TCanteenRequestDetailModel
    { 
    public long ReqDetId { get; set; }
    public long? ReqId { get; set; }
    public long? ItemId { get; set; }
    public decimal? UnitMrp { get; set; }
    public double? Qty { get; set; }
    public decimal? TotalAmount { get; set; }
    public bool? IsBillGenerated { get; set; }
    public bool? IsCancelled { get; set; }
    }
    public class TCanteenRequestDetailModelValidator : AbstractValidator<TCanteenRequestDetailModel>
    {
        public TCanteenRequestDetailModelValidator()
        {
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId Date is required");
            RuleFor(x => x.UnitMrp).NotNull().NotEmpty().WithMessage("UnitMrp Time is required");

        } 
    }
        public class CanteenModel
        {
            public CanteenRequestModel CanteenR { get; set; }
            public TCanteenRequestDetailModel CanteenRequest { get; set; }
        }
    }

