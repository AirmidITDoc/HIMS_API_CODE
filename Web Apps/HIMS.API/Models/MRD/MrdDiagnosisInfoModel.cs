using FluentValidation;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.MRD
{
    public class MrdDiagnosisInfoModel
    {
        public long IpdiagId { get; set; }
        public long? AdmId { get; set; }
        public bool? IsSync { get; set; }
        public int? CreatedBy { get; set; }


        //public List<MrdDiagnosisInfoDetailModel> TIpMrdDiagnosisInfoDetails { get; set; }
    }
    public class MrdDiagnosisInfoModelValidator : AbstractValidator<MrdDiagnosisInfoModel>
    {
        public MrdDiagnosisInfoModelValidator()
        {
             RuleFor(x => x.AdmId).NotNull().NotEmpty().WithMessage("AdmId is required");
      

        }
    }
    public class MrdDiagnosisInfoDetailModel
    {

        //public long IpdiagDetId { get; set; }
        public long IpdiagId { get; set; }
        public long AdmId { get; set; }
        public string Diagnosis { get; set; } = null!;
        public string Icdcode { get; set; } = null!;
        public string Diagnosisinformation { get; set; } = null!;
        public string FlagCode { get; set; } = null!;
        public int? CreatedBy { get; set; }


    }
    public class MrdDiagnosisInfoDetailModelValidator : AbstractValidator<MrdDiagnosisInfoDetailModel>
    {
        public MrdDiagnosisInfoDetailModelValidator()
        {
            RuleFor(x => x.Diagnosis).NotNull().NotEmpty().WithMessage("Diagnosis is required");
            RuleFor(x => x.Icdcode).NotNull().NotEmpty().WithMessage("Icdcode is required");
            RuleFor(x => x.Diagnosisinformation).NotNull().NotEmpty().WithMessage("Diagnosisinformation is required");


        }
    }
    public class MrdDiagnosisInfo
    {
        public MrdDiagnosisInfoModel MrdDiagnosisInfoHeader { get; set; }
        public List<MrdDiagnosisInfoDetailModel> MrdDiagnosisInfoDetail { get; set; }

    }
    public class MrdDiagnosisInfoUpdate
    {
        public MrdDiagnosisInfoUpdateModel MrdDiagnosisInfoHeader { get; set; }
        public List<MrdDiagnosisInfoDetailModel> MrdDiagnosisInfoDetail { get; set; }

    }
    public class MrdDiagnosisInfoUpdateModel
    {
        public long IpdiagId { get; set; }
        public long? AdmId { get; set; }
        public bool? IsSync { get; set; }
        public int? ModifiedBy { get; set; }


    }
}
