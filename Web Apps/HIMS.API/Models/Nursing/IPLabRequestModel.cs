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
        public List<TDLabRequestModel> TDlabRequests { get; set; }
    }

    public class IPLabRequestModelValidator : AbstractValidator<IPLabRequestModel>
    {
        public IPLabRequestModelValidator()
        {
                RuleFor(x => x.OpIpId).NotNull().NotEmpty().WithMessage("OpIpId is required");
                RuleFor(x => x.OpIpType).NotNull().NotEmpty().WithMessage("OpIpType is required");
           
        }
    }

    public class TDLabRequestModel
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

    public class TDLabRequestModelValidator : AbstractValidator<TDLabRequestModel>
    {
        public TDLabRequestModelValidator()
        {
            RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage("ServiceId is required");
  
        }
    }
}
