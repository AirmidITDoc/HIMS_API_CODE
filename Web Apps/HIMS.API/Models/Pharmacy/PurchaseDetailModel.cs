﻿using FluentValidation;
using System.Text.Json.Serialization;

namespace HIMS.API.Models.Pharmacy
{
    public class PurchaseDetailModel
    {
        public long PurchaseId { get; set; }
        public long? ItemId { get; set; }
        public long? Uomid { get; set; }
        public double? Qty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? DiscPer { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? VatPer { get; set; }
        public decimal? GrandTotalAmount { get; set; }
        public decimal? Mrp { get; set; }
        public string? Specification { get; set; }
        public decimal? Cgstper { get; set; }
        public decimal? Cgstamt { get; set; }
        public decimal? Sgstper { get; set; }
        public decimal? Sgstamt { get; set; }
        public decimal? Igstper { get; set; }
        public decimal? Igstamt { get; set; }
        public decimal? DefRate { get; set; }
        public float? VendDiscPer { get; set; }
        public decimal? VendDiscAmt { get; set; }
    }
    public class PurchaseDetailModellValidator : AbstractValidator<PurchaseDetailModel>
    {
        public PurchaseDetailModellValidator()
        {
            RuleFor(x => x.Uomid).NotNull().NotEmpty().WithMessage("UMO is required");
            RuleFor(x => x.Qty).NotNull().NotEmpty().WithMessage("Qty is required");
            RuleFor(x => x.Rate).NotNull().NotEmpty().WithMessage("Rate is required");
            RuleFor(x => x.Mrp).NotNull().NotEmpty().WithMessage("MRP is required");
            RuleFor(x => x.GrandTotalAmount).NotNull().NotEmpty().WithMessage("Grand Total Amount is required");
        }
    }

    public class PurchaseVerifyModel
    {
        public long PurchaseId { get; set; }
        public long IsVerifiedId { get; set; }
   
    }
    public class PurchaseVerifyModelValidator : AbstractValidator<PurchaseVerifyModel>
    {
        public PurchaseVerifyModelValidator()
        {
            RuleFor(x => x.PurchaseId).NotNull().NotEmpty().WithMessage("Purchase id is required");
            RuleFor(x => x.IsVerifiedId).NotNull().NotEmpty().WithMessage("Verified id is required");
        }
    }
}
