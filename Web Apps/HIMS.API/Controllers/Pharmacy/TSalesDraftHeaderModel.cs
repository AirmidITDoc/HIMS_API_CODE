using FluentValidation;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;

namespace HIMS.API.Models.Pharmacy
{
    public class TSalesDraftHeaderModel
    {
        public long DsalesId { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long? OpIpId { get; set; }
        public long? OpIpType { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public long? ConcessionReasonId { get; set; }
        public long? ConcessionAuthorizationId { get; set; }
        public bool? IsSellted { get; set; }
        public bool? IsPrint { get; set; }
        public long? UnitId { get; set; }
        public long? AddedBy { get; set; }
        public string? ExternalPatientName { get; set; }
        public string? DoctorName { get; set; }
        public long? StoreId { get; set; }
        public string? CreditReason { get; set; }
        public long? CreditReasonId { get; set; }
        public bool? IsClosed { get; set; }
        public long? IsPrescription { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public string? ExtMobileNo { get; set; }
        public string? ExtAddress { get; set; }

    }
    public class TSalesDraftHeaderModelValidator : AbstractValidator<TSalesDraftHeaderModel>
    {
        public TSalesDraftHeaderModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");
            RuleFor(x => x.OpIpId).NotNull().NotEmpty().WithMessage("OpIpId is required");
        }
    }

    public class TSalesDraftDetModel
    {
        public long? DsalesId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? UnitMrp { get; set; }
        public float? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? VatPer { get; set; }
        public decimal? VatAmount { get; set; }
        public double? DiscPer { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? LandedPrice { get; set; }
        public decimal? TotalLandedAmount { get; set; }
        public decimal? PurRateWf { get; set; }
        public decimal? PurTotAmt { get; set; }
    }

    public class TSalesDraftDetModelValidator : AbstractValidator<TSalesDraftDetModel>
    {
        public TSalesDraftDetModelValidator()
        {
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId is required");
            RuleFor(x => x.UnitMrp).NotNull().NotEmpty().WithMessage("UnitMrp is required");
        }
    }

    public class SalesDraftHeadersModel
    {
        public TSalesDraftHeaderModel SalesDraft { get; set; }
        public List<TSalesDraftDetModel> SalesDraftDet { get; set; }
    }
}