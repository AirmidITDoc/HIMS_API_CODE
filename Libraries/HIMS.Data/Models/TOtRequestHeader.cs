using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtRequestHeader
    {
        public TOtRequestHeader()
        {
            TOtRequestAttendingDetails = new HashSet<TOtRequestAttendingDetail>();
            TOtRequestDiagnoses = new HashSet<TOtRequestDiagnosis>();
            TOtRequestSurgeryDetails = new HashSet<TOtRequestSurgeryDetail>();
        }

        public long OtrequestId { get; set; }
        public DateTime? OtrequestDate { get; set; }
        public DateTime? OtrequestTime { get; set; }
        public string? OtrequestNo { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public string? BloodGroup { get; set; }
        public long? CategoryType { get; set; }
        public long? Ottable { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public DateTime? EstimateTime { get; set; }
        public string? Diagnosis { get; set; }
        public string? Comments { get; set; }
        public bool? RequestType { get; set; }
        public bool? Pacrequired { get; set; }
        public bool? EquipmentsRequired { get; set; }
        public bool? ClearanceMedical { get; set; }
        public bool? ClearanceFinancial { get; set; }
        public bool? Infective { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public string? Reason { get; set; }

        public virtual ICollection<TOtRequestAttendingDetail> TOtRequestAttendingDetails { get; set; }
        public virtual ICollection<TOtRequestDiagnosis> TOtRequestDiagnoses { get; set; }
        public virtual ICollection<TOtRequestSurgeryDetail> TOtRequestSurgeryDetails { get; set; }
    }
}
