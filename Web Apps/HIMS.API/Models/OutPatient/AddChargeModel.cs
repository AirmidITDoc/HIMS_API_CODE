﻿using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.OutPatient
{
    public class AddChargesModel
    {
        public long ChargesId { get; set; }
        public DateTime? ChargesDate { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public long? ServiceId { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public double? ConcessionPercentage { get; set; }
        public decimal? ConcessionAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public long? DoctorId { get; set; }
        public double? DocPercentage { get; set; }
        public double? DocAmt { get; set; }
        public double? HospitalAmt { get; set; }
        public bool? IsGenerated { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public long? IsDoctorShareGenerated { get; set; }
        public byte? IsInterimBillFlag { get; set; }
        public long? IsPackage { get; set; }
        public long? IsSelfOrCompanyService { get; set; }
        public long? PackageId { get; set; }
        public DateTime? ChargesTime { get; set; }
        public long? PackageMainChargeId { get; set; }
        public long? ClassId { get; set; }
        public decimal? RefundAmount { get; set; }
        public decimal? CPrice { get; set; }
        public float? CQty { get; set; }
        public decimal? CTotalAmount { get; set; }
        public bool? IsComServ { get; set; }
        public bool? IsPrintCompSer { get; set; }
        public string? ServiceName { get; set; }
        public decimal? ChPrice { get; set; }
        public float? ChQty { get; set; }
        public decimal? ChTotalAmount { get; set; }
        public bool? IsBillableCharity { get; set; }
        public long? SalesId { get; set; }
        public long? BillNo { get; set; }
        public int? IsHospMrk { get; set; }

    }

    public class AddChargesModelValidator : AbstractValidator<AddChargesModel>
    {
        public AddChargesModelValidator()
        {
            RuleFor(x => x.OpdIpdId).NotNull().NotEmpty().WithMessage("OpdIpdId is required");
           
        }
    }


  
}
