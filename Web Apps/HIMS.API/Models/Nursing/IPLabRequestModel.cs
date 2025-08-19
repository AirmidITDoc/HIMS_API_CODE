using FluentValidation;
using HIMS.API.Models.OPPatient;

namespace HIMS.API.Models.Nursing
{
    public class IPLabRequestModel
    {
        public int RequestId { get; set; }
        public DateTime? ReqDate { get; set; }
        public string? ReqTime { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? IsAddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public string? IsCancelledTime { get; set; }
        public byte? IsType { get; set; }
        public bool? IsOnFileTest { get; set; }
        public List<TDLabRequestModel> TDlabRequests { get; set; }
    }

    public class IPLabRequestModelValidator : AbstractValidator<IPLabRequestModel>
    {
        public IPLabRequestModelValidator()
        {
                RuleFor(x => x.ReqDate).NotNull().NotEmpty().WithMessage("ReqDate is required");
                RuleFor(x => x.ReqTime).NotNull().NotEmpty().WithMessage("ReqTime is required");
           
        }
    }

    public class TDLabRequestModel
    {
        public int ReqDetId { get; set; }
        public int? RequestId { get; set; }
        public int? ServiceId { get; set; }
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
            RuleFor(x => x.AddedByDate).NotNull().NotEmpty().WithMessage("AddedByDate is required");
            RuleFor(x => x.AddedByTime).NotNull().NotEmpty().WithMessage("AddedByTime is required");


        }
    }
    public class LabRequestCancel
    {
        public long RequestId { get; set; }


    }
}
