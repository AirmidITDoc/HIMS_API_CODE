using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{
    public class GRNDetailModel
    {
        public long? GrndetId { get; set; }
        public long? Grnid { get; set; }
        public long? ItemId { get; set; }
        public long? Uomid { get; set; }
        public float? ReceiveQty { get; set; }
        public float? FreeQty { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalAmount { get; set; }
        public long? ConversionFactor { get; set; }
        public decimal? VatPercentage { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? DiscPercentage { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? OtherTax { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public float? TotalQty { get; set; }
        public long? Pono { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? PurUnitRate { get; set; }
        public decimal? PurUnitRateWf { get; set; }
        public decimal? Cgstper { get; set; }
        public decimal? Cgstamt { get; set; }
        public decimal? Sgstper { get; set; }
        public decimal? Sgstamt { get; set; }
        public decimal? Igstper { get; set; }
        public decimal? Igstamt { get; set; }
        public decimal? MrpStrip { get; set; }
        public long? StkId { get; set; }
        public float? DiscPerc2 { get; set; }
        public decimal? DiscAmt2 { get; set; }
        public class GRNDetailModelValidator : AbstractValidator<GRNDetailModel>
        {
            public GRNDetailModelValidator()
            {
                RuleFor(x => x.BatchNo).NotNull().NotEmpty().WithMessage("Batch No is required");
                RuleFor(x => x.BatchExpDate).NotNull().NotEmpty().WithMessage("Batch Exp Date is required");
                RuleFor(x => x.ReceiveQty).NotNull().NotEmpty().WithMessage("Receiv eQty is required");
                RuleFor(x => x.Mrp).NotNull().NotEmpty().WithMessage("MRP is required");
                RuleFor(x => x.Rate).NotNull().NotEmpty().WithMessage("Rate is required");
                RuleFor(x => x.DiscAmount).NotNull().NotEmpty().WithMessage("Discount Amount No is required");
            }
        }
    }

    public class GRNItemModel
    {
        public long? ItemId { get; set; }
        public float? Cgst { get; set; }
        public float? Sgst { get; set; }
        public float? Igst { get; set; }
        public string? Hsncode { get; set; }
    }

}
