using System;
using System.Collections.Generic;

namespace HIMS.Data.DTO.Nursing.IPEMR
{
    // 1. Diagnosis Info List DTO
    public class IPEMRDiagnosisInfoListDto
    {
        public long IpemrdiagnId { get; set; }
        public long Ipemrid { get; set; }
        public long AdmId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Icdcode { get; set; }
        public string? Diagnosisinformation { get; set; }
        public string? FlagCode { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatedByname { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }


    }

    // 2. Diagnosis History List DTO
    public class IPEMRDignosisHistoryListDto
    {
        public long EmrdignId { get; set; }
        public long Ipemrid { get; set; }
        public long AdmissionId { get; set; }
        public string? DescriptionName { get; set; }
        public string DescriptionType { get; set; } = null!;
        public string? Icdcode { get; set; }
        public string? DiagnosisName { get; set; }
        public string? DiagnosisInfo { get; set; }
        public long CreatedBy { get; set; }
        public string? CreatedByName { get; set; }

        public DateTime CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }

    // 3. Family Medical History List DTO
    public class IPEMRFamilyMedicalHistoryListDto
    {
        public long FhistId { get; set; }
        public long IpEmrId { get; set; }
        public long? RegId { get; set; }
        public byte? OpIpType { get; set; }
        public long AdmissionId { get; set; }
        public long RelationshipId { get; set; }
        public string? RelationshipName { get; set; }
        public string? MemberName { get; set; }
        public long Age { get; set; }
        public string? ClinicalHistory { get; set; }
        public long? Duration { get; set; }
        public long GenderId { get; set; }
        public string? Summary { get; set; }
        public bool Status { get; set; }
        public long CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public string? ModifiedByName { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
    public class FamilyMedicalHistoryListDto
    {
        public long FhistId { get; set; }
        public long IpEmrId { get; set; }
        public long? RegId { get; set; }
        public byte? OpIpType { get; set; }
        public long AdmissionId { get; set; }
        public string? RelationshipName { get; set; }
        public string? MemberName { get; set; }
        public long Age { get; set; }
        public string? ClinicalHistory { get; set; }
        public long? Duration { get; set; }
        public string GenderName { get; set; }
        public string? Summary { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
