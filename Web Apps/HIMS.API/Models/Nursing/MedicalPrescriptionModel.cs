﻿using FluentValidation;

namespace HIMS.API.Models.Nursing
{
    public class MedicalPrescriptionModel
    {
        public long MedicalRecoredId { get; set; }
        public long? AdmissionId { get; set; }
        public DateTime? RoundVisitDate { get; set; }
        public string? RoundVisitTime { get; set; }
        public bool? InHouseFlag { get; set; }
        public List<PrescriptionModel> TIpPrescriptions { get; set; }

    }
    public class MedicalPrescriptionModelValidator : AbstractValidator<MedicalPrescriptionModel>
    {
        public MedicalPrescriptionModelValidator()
        {
            RuleFor(x => x.RoundVisitDate).NotNull().NotEmpty().WithMessage("RoundVisitDate Date is required");
            RuleFor(x => x.RoundVisitTime).NotNull().NotEmpty().WithMessage("RoundVisitTime Time is required");

        }
    }
    public class PrescriptionModel
    {
        public long IppreId { get; set; }
        public long? IpmedId { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpdIpdType { get; set; }
        public DateTime? Pdate { get; set; }
        public string? Ptime { get; set; }
        public long? ClassId { get; set; }
        public long? GenericId { get; set; }
        public long? DrugId { get; set; }
        public long? DoseId { get; set; }
        public long? Days { get; set; }
        public double? QtyPerDay { get; set; }
        public double? TotalQty { get; set; }
        public string? Remark { get; set; }
        public bool? IsClosed { get; set; }
        public long? IsAddBy { get; set; }
        public long? StoreId { get; set; }
        public long? WardId { get; set; }
    }
    public class PrescriptionModelModelValidator : AbstractValidator<PrescriptionModel>
    {
        public PrescriptionModelModelValidator()
        {
            RuleFor(x => x.Pdate).NotNull().NotEmpty().WithMessage("Pdate Date is required");
            RuleFor(x => x.Ptime).NotNull().NotEmpty().WithMessage("Ptime Time is required");

        }
    }
}
