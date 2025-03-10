﻿using FluentValidation;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.IPPatient
{
    public class TIPmedicalRecordModel
    {
        public long MedicalRecoredId { get; set; }
        public long? AdmissionId { get; set; }
        public DateTime? RoundVisitDate { get; set; }
        public DateTime? RoundVisitTime { get; set; }
        public bool? InHouseFlag { get; set; }
        public List<IpPrescriptionModel> TIpPrescription { get; set; }

    }
    public class TIPmedicalRecordModelValidator : AbstractValidator<TIPmedicalRecordModel>
    
    {
        public TIPmedicalRecordModelValidator()
        {
            RuleFor(x => x.RoundVisitDate).NotNull().NotEmpty().WithMessage("RoundVisitDate is required");
            RuleFor(x => x.RoundVisitTime).NotNull().NotEmpty().WithMessage("RoundVisitTime is required");

        }
    }
    public  class IpPrescriptionModel
    {
        public long IppreId { get; set; }
        public long? IpmedId { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpdIpdType { get; set; }
        public DateTime? Pdate { get; set; }
        public DateTime? Ptime { get; set; }
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
    public class IpPrescriptionModelValidator : AbstractValidator<IpPrescriptionModel>

    {
        public IpPrescriptionModelValidator()
        {
            RuleFor(x => x.Pdate).NotNull().NotEmpty().WithMessage("Pdate is required");
            RuleFor(x => x.Ptime).NotNull().NotEmpty().WithMessage("Ptime is required");

        }
    }
}
