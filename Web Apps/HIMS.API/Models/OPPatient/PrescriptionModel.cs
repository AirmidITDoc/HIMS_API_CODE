using FluentValidation;

namespace HIMS.API.Models.OPPatient
{
    public class PrescriptionModel
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
        //public bool? IsClosed { get; set; }
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


        public List<VisitDetailsModel> VisitDetails { get; set; }


    }


    public class PrescriptionMasterModelValidator : AbstractValidator<PrescriptionModel>
    {
        public PrescriptionMasterModelValidator()
        {
            RuleFor(x => x.OpdIpdIp).NotNull().NotEmpty().WithMessage("OpdIpdIp  is required");
            RuleFor(x => x.OpdIpdType).NotNull().NotEmpty().WithMessage("OpdIpdType  is required");
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date  is required");
            RuleFor(x => x.Ptime).NotNull().NotEmpty().WithMessage("Ptime  is required");
            RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId  is required");
            RuleFor(x => x.GenericId).NotNull().NotEmpty().WithMessage("GenericId  is required");
            RuleFor(x => x.DrugId).NotNull().NotEmpty().WithMessage("DrugId  is required");
            RuleFor(x => x.DoseId).NotNull().NotEmpty().WithMessage("DoseId  is required");
        }
    }

    public class VisitDetailsModel
    {
        public long VisitId { get; set; }
       
        public DateTime? FollowupDate { get; set; }
      

    }


    public class VisitDetailsModelValidator : AbstractValidator<VisitDetailsModel>
    {
        public VisitDetailsModelValidator()
        {
           
            RuleFor(x => x.FollowupDate).NotNull().NotEmpty().WithMessage("FollowupDate  is required");
        }
    }
}
