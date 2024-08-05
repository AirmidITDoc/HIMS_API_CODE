using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class VisitDetail
    {
        public long VisitId { get; set; }
        public long? RegId { get; set; }
        public string? VisitDate { get; set; }
        public string? VisitTime { get; set; }
        public long? UnitId { get; set; }
        public long? PatientTypeId { get; set; }
        public long? ConsultantDocId { get; set; }
        public long? RefDocId { get; set; }
        public string? Opdno { get; set; }
        public long? TariffId { get; set; }
        public long? CompanyId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? IsCancelledBy { get; set; }
        public bool? IsCancelled { get; set; }
        public string? IsCancelledDate { get; set; }
        public long? ClassId { get; set; }
        public long? DepartmentId { get; set; }
        public long? PatientOldNew { get; set; }
        public long? FirstFollowupVisit { get; set; }
        public long? AppPurposeId { get; set; }
        public string? FollowupDate { get; set; }
        public bool? IsMark { get; set; }
        public string? Comments { get; set; }
        public bool? IsXray { get; set; }
        public byte? CrossConsulFlag { get; set; }
        public long? PhoneAppId { get; set; }
    }
}
