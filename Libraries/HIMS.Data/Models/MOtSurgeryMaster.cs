using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MOtSurgeryMaster
    {
        public long SurgeryId { get; set; }
        public string? SurgeryCode { get; set; }
        public string? ShortName { get; set; }
        public string? SurgeryName { get; set; }
        public long? DepartmentId { get; set; }
        public long? SubSpecialty { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public long? SurgeryTypeId { get; set; }
        public decimal? SurgeryAmount { get; set; }
        public long? SiteDescId { get; set; }
        public long? OttemplateId { get; set; }
        public long? ServiceId { get; set; }
        public long? ExpectedSurgeryTime { get; set; }
        public long? PreparationTime { get; set; }
        public long? CleaningTurnaroundTime { get; set; }
        public long? TotalDuration { get; set; }
        public bool? PreAnaesthesiaClearance { get; set; }
        public bool? SurgicalConsentRequired { get; set; }
        public bool? BloodArrangementRequired { get; set; }
        public long? GradeLevel { get; set; }
        public long? PreferredOtroom { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
