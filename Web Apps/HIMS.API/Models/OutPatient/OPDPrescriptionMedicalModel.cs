﻿using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class OPDPrescriptionMedicalModel
    {
        public long PrecriptionId { get; set; }
        public long? OpdIpdIp { get; set; }
        public byte? OpdIpdType { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Ptime { get; set; }
        public long? ClassId { get; set; }
        public long? GenericId { get; set; }
        public long? DrugId { get; set; }
        public long? DoseId { get; set; }
        public long? Days { get; set; }
        public long? InstructionId { get; set; }
        public double? QtyPerDay { get; set; }
        public double? TotalQty { get; set; }
        public string? Instruction { get; set; }
        public string? Remark { get; set; }
        public bool? IsClosed { get; set; }
        public bool? IsEnglishOrIsMarathi { get; set; }
        public string? Pweight { get; set; }
        public string? Pulse { get; set; }
        public string? Bp { get; set; }
        public string? Bsl { get; set; }
        public string? ChiefComplaint { get; set; }
        public long? IsAddBy { get; set; }
        public string? SpO2 { get; set; }
        public long? StoreId { get; set; }
        public long? DoseOption2 { get; set; }
        public long? DaysOption2 { get; set; }
        public long? DoseOption3 { get; set; }
        public long? DaysOption3 { get; set; }
    }
    public class OPDPrescriptionModelValidator : AbstractValidator<OPDPrescriptionMedicalModel>

    {
        public OPDPrescriptionModelValidator()
        {
            RuleFor(x => x.OpdIpdIp).NotNull().NotEmpty().WithMessage("OpdIpdIp is required");
            RuleFor(x => x.OpdIpdType).NotNull().NotEmpty().WithMessage("OpdIpdType  is required");
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage(" Date required");
            RuleFor(x => x.Ptime).NotNull().NotEmpty().WithMessage("Ptime is required");
           
        }
    }
}
