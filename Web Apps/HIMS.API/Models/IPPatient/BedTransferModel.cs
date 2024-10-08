﻿using FluentValidation;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OutPatient;

namespace HIMS.API.Models.IPPatient
{
    public class BedTransferModel
    {
        public long TransferId { get; set; }
        public long? AdmissionId { get; set; }
        public DateTime? FromDate { get; set; }
        public string? FromTime { get; set; }
        public long? FromWardId { get; set; }
        public long? FromBedId { get; set; }
        public long? FromClassId { get; set; }
        public DateTime? ToDate { get; set; }
        public string? ToTime { get; set; }
        public long? ToWardId { get; set; }
        public long? ToBedId { get; set; }
        public long? ToClassId { get; set; }
        public string? Remark { get; set; }
        public long? AddedBy { get; set; }
        public long? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
}
 public class BedTransferModelValidator : AbstractValidator<BedTransferModel>
    {
        public BedTransferModelValidator()
        {
            RuleFor(x => x.FromDate).NotNull().NotEmpty().WithMessage("FromDate is required");
            RuleFor(x => x.FromTime).NotNull().NotEmpty().WithMessage("FromTime is required");
            RuleFor(x => x.FromWardId).NotNull().NotEmpty().WithMessage("FromWardId is required");
            RuleFor(x => x.FromClassId).NotNull().NotEmpty().WithMessage("FromClassId is required");
            RuleFor(x => x.ToDate).NotNull().NotEmpty().WithMessage("ToDate is required");
            RuleFor(x => x.ToTime).NotNull().NotEmpty().WithMessage("ToTime is required");
        }
    }
    public  class BedmasterModel1
    {
        public long BedId { get; set; }
        public string? BedName { get; set; }
        public long? RoomId { get; set; }
        public bool? IsAvailible { get; set; }
        public bool? IsActive { get; set; }
    }
    public class BedmasterModel1Validator : AbstractValidator<BedmasterModel1>
    {
        public BedmasterModel1Validator()
        {
            RuleFor(x => x.BedName).NotNull().NotEmpty().WithMessage("BedName is required");
            RuleFor(x => x.RoomId).NotNull().NotEmpty().WithMessage("RoomId is required");
        }
    }
}
