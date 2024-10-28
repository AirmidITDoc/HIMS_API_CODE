using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Nursing
{
    public class LabRequestModel
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
        public string? IsCancelledTime { get; set; }
        //public byte? IsType { get; set; }
        public bool? IsOnFileTest { get; set; }
        public List<TDlabRequestModel> TDlabRequests { get; set; }
    }
    public class LabRequestModelValidator : AbstractValidator<LabRequestModel>
    {
        public LabRequestModelValidator()
        {
            RuleFor(x => x.ReqDate).NotNull().NotEmpty().WithMessage("ReqDate Date is required");
            RuleFor(x => x.ReqTime).NotNull().NotEmpty().WithMessage("ReqTime Time is required");

        }
    }
    public class TDlabRequestModel
    {
        public long ReqDetId { get; set; }
        public long? RequestId { get; set; }
        public long? ServiceId { get; set; }
        public decimal? Price { get; set; }
        public bool? IsStatus { get; set; }
        public bool? IsOnFileTest { get; set; }
    }
    public class TDlabRequestModelValidator : AbstractValidator<TDlabRequestModel>
    {
        public TDlabRequestModelValidator()
        {
            RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage("ServiceId Date is required");
            RuleFor(x => x.Price).NotNull().NotEmpty().WithMessage("Price Time is required");

        }
    }
}
