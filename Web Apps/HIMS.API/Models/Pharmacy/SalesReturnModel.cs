using DocumentFormat.OpenXml.Wordprocessing;
using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Pharmacy
{
    public class SalesReturnModel
    {
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long? SalesId { get; set; }
        public long? OpIpId { get; set; }
        public long? OpIpType { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public bool? IsSellted { get; set; }
        public bool? IsPrint { get; set; }
        public bool? IsFree { get; set; }
        public long? UnitId { get; set; }
        public long? AddedBy { get; set; }
        public long? StoreId { get; set; }
        public string? Narration { get; set; }
        public long SalesReturnId { get; set; }
        public List<SalesReturnDetailsModel> TSalesReturnDetails { get; set; }
        public List<CurrentStockModels> TCurrentStock { get; set; }



    }
    public class SalesReturnModelValidator : AbstractValidator<SalesReturnModel>
    {
        public SalesReturnModelValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date  is required");
            RuleFor(x => x.Time).NotNull().NotEmpty().WithMessage("Time Time is required");

        }
    }
    public class SalesReturnDetailsModel
    {
        public long SalesReturnDetId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? UnitMrp { get; set; }
        public double? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? VatPer { get; set; }
        public decimal? VatAmount { get; set; }
        public double? DiscPer { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? LandedPrice { get; set; }
        public decimal? TotalLandedAmount { get; set; }
        public decimal? PurRate { get; set; }
        public decimal? PurTot { get; set; }
        public long? SalesId { get; set; }
        public long? SalesDetId { get; set; }
        public byte? IsCashOrCredit { get; set; }
        public float? Cgstper { get; set; }
        public decimal? Cgstamt { get; set; }
        public float? Sgstper { get; set; }
        public decimal? Sgstamt { get; set; }
        public float? Igstper { get; set; }
        public decimal? Igstamt { get; set; }
        public long? StkId { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? Mrptotal { get; set; }

    }
    public class CurrentStockModels
    {
        public long? ItemId { get; set; }
        public float? IssueQty { get; set; }
        public long? StoreId { get; set; }
        public long? IstkId { get; set; }


    }
}
