using System.Drawing.Printing;
using FluentValidation;
using HIMS.API.Models.IPPatient;

namespace HIMS.API.Models.OutPatient
{
    public class TPrescriptionModel
    {

      
        public long? OpdIpdIp { get; set; }
        public byte? OpdIpdType { get; set; }
        public DateTime? Date { get; set; }
        public string? Ptime { get; set; }
        public long? ClassId { get; set; }
        public long? GenericId { get; set; }
        public long? DrugId { get; set; }
        public long? DoseId { get; set; }
        public long? Days { get; set; }
        public string? Instruction { get; set; }
        public string? Remark { get; set; }
        public long? DoseOption2 { get; set; }
        public long? DaysOption2 { get; set; }
        public long? DoseOption3 { get; set; }
        public long? DaysOption3 { get; set; }
        public long? InstructionId { get; set; }
        public double? QtyPerDay { get; set; }
        public double? TotalQty { get; set; }
        public bool? IsClosed { get; set; }
        public bool? IsEnglishOrIsMarathi { get; set; }
        public string? ChiefComplaint { get; set; }
        public string? Diagnosis { get; set; }
        public string? Examination { get; set; }
        public string? Height { get; set; }
        public string? Pweight { get; set; }
        public string? Bmi { get; set; }
        public string? Bsl { get; set; }
        public string? SpO2 { get; set; }
        public string? Temp { get; set; }
        public string? Pulse { get; set; }
        public string? Bp { get; set; }
        public long? StoreId { get; set; }
        public long? PatientReferDocId { get; set; }
        public string? Advice { get; set; }
        public long? IsAddBy { get; set; }
   


        public class RTPrescriptionModelValidator : AbstractValidator<TPrescriptionModel>
        {
            public RTPrescriptionModelValidator()
            {
                RuleFor(x => x.OpdIpdIp).NotNull().NotEmpty().WithMessage("OpdIpdIp is required");
                RuleFor(x => x.OpdIpdType).NotNull().NotEmpty().WithMessage("OpdIpdType is required");
                RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");
                RuleFor(x => x.GenericId).NotNull().NotEmpty().WithMessage("GenericId is required");

            }
        }
        public class TOPRequestListModel
        {

            public long? OpIpId { get; set; }
            public long? ServiceId { get; set; }
        }
        public class TOPRequestListModelValidator : AbstractValidator<TOPRequestListModel>
        {
            public TOPRequestListModelValidator()
            {
                RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage("ServiceId is required");


            }
        }
        public class MOPCasepaperDignosisMasterModel
        {

            public long? VisitId { get; set; }
            public string DescriptionType { get; set; }
            public string DescriptionName { get; set; }
        }
        public class MOPCasepaperDignosisMasterModelValidator : AbstractValidator<MOPCasepaperDignosisMasterModel>
        {
            public MOPCasepaperDignosisMasterModelValidator()
            {
                RuleFor(x => x.DescriptionType).NotNull().NotEmpty().WithMessage("DescriptionType is required");
                RuleFor(x => x.DescriptionName).NotNull().NotEmpty().WithMessage("DescriptionName is required");

            }
        }

        public class ModelTPrescription
        {
            public TPrescriptionModel TPrescription { get; set; }
            public List<TOPRequestListModel> TOPRequestList { get; set; }
            public List<MOPCasepaperDignosisMasterModel> MOPCasepaperDignosisMaster { get; set; }


        }
        public class UpdatePrescriptionModel
        {
            public long PrecriptionId { get; set; }
            public long DoseId { get; set; }

        }
        public class UpdatePrescription
        {
            public long PrecriptionId { get; set; }
            public long GenericId { get; set; }

        }

    }
}