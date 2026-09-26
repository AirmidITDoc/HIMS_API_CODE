using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpEmrhistory
    {
        public TIpEmrhistory()
        {
            TIpEmrdiagnosisInfos = new HashSet<TIpEmrdiagnosisInfo>();
            TIpEmrdignosisHistories = new HashSet<TIpEmrdignosisHistory>();
        }

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
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TIpEmrfamilyMedicalHistory TIpEmrfamilyMedicalHistory { get; set; } = null!;
        public virtual ICollection<TIpEmrdiagnosisInfo> TIpEmrdiagnosisInfos { get; set; }
        public virtual ICollection<TIpEmrdignosisHistory> TIpEmrdignosisHistories { get; set; }
    }
}
