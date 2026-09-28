using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Nursing.IPEMR
{
    public class EMRModel
    {
        public long IpdEmrId { get; set; }
        public long Opipid { get; set; }
        public long Opiptype { get; set; }
        public string? SocialHabits { get; set; }
        public long? BloodGroup { get; set; }
        public string? MedicalHistory { get; set; }
        public string? FamilyMedicalHistory { get; set; }
        public string? Provisional { get; set; }
        public string? FinalDiagnosis { get; set; }
        public string? ChiefComplaints { get; set; }
        public string? Examination { get; set; }
        public string? CurrentMedications { get; set; }
        public string? NormalAllergy { get; set; }
        public string? DrugAllergy { get; set; }
        public string? AllergyRemark { get; set; }


        public List<EMRDiagnosisInfoModel> TIpEmrdiagnosisInfos { get; set; }
        public List<EMRDignosisHistoryModel> TIpEmrdignosisHistories { get; set; }
        public List<EMRFamilyMedicalHistoryModel> TIpEmrfamilyMedicalHistories { get; set; }


    }

    public class EMRDiagnosisInfoModel
    {
        public long IpemrdiagnId { get; set; }
        public long Ipemrid { get; set; }
        public long AdmId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Icdcode { get; set; }
        public string? Diagnosisinformation { get; set; }
        public string? FlagCode { get; set; }
    }

    public class EMRDignosisHistoryModel
    {
        public long EmrdignId { get; set; }
        public long Ipemrid { get; set; }
        public long AdmissionId { get; set; }
        public string? DescriptionName { get; set; }
        public string DescriptionType { get; set; } = null!;
        public string? Icdcode { get; set; }
        public string? DiagnosisName { get; set; }
        public string? DiagnosisInfo { get; set; }
    }

    public class EMRFamilyMedicalHistoryModel
    {
        public long FhistId { get; set; }
        public long IpEmrId { get; set; }
        public long? RegId { get; set; }
        public byte? OpIpType { get; set; }
        public long AdmissionId { get; set; }
        public long RelationshipId { get; set; }
        public string MemberName { get; set; } = null!;
        public long Age { get; set; }
        public string? ClinicalHistory { get; set; }
        public long? Duration { get; set; }
        public long GenderId { get; set; }
        public string? Summary { get; set; }
        public bool Status { get; set; }
    }
}