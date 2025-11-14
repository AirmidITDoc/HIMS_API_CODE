using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtInOperationHeader
    {
        public TOtInOperationHeader()
        {
            TOtInOperationAttendingDetails = new HashSet<TOtInOperationAttendingDetail>();
            TOtInOperationDiagnoses = new HashSet<TOtInOperationDiagnosis>();
            TOtInOperationPostOperDiagnoses = new HashSet<TOtInOperationPostOperDiagnosis>();
            TOtInOperationSurgeryDetails = new HashSet<TOtInOperationSurgeryDetail>();
        }

        public long OtinOperationId { get; set; }
        public long? OtreservationId { get; set; }
        public DateTime? OtinOperationDate { get; set; }
        public DateTime? OtinOperationTime { get; set; }
        public string? OtinOperationNo { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public long? CategoryType { get; set; }
        public long? Ottable { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public int? Duration { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public string? BloodLoss { get; set; }
        public long? AnesthesiaType { get; set; }
        public DateTime? TheaterInDate { get; set; }
        public DateTime? TheaterInTime { get; set; }
        public DateTime? TheaterOutData { get; set; }
        public DateTime? TheaterOutTime { get; set; }
        public int? BloodArranged { get; set; }
        public int? Pacrequired { get; set; }
        public int? EquipmentsRequired { get; set; }
        public int? Infective { get; set; }
        public int? ClearanceMedical { get; set; }
        public int? ClearanceFinancial { get; set; }
        public int? IntraOpeChangeInSurgeryPlan { get; set; }
        public int? MopCount { get; set; }
        public string? StepsOfProc { get; set; }
        public string? ClosureNotes { get; set; }
        public string? OperativeFindingsNotes { get; set; }
        public string? PostOperativeNotes { get; set; }
        public string? ConditionOfPatientNotes { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TOtInOperationAttendingDetail> TOtInOperationAttendingDetails { get; set; }
        public virtual ICollection<TOtInOperationDiagnosis> TOtInOperationDiagnoses { get; set; }
        public virtual ICollection<TOtInOperationPostOperDiagnosis> TOtInOperationPostOperDiagnoses { get; set; }
        public virtual ICollection<TOtInOperationSurgeryDetail> TOtInOperationSurgeryDetails { get; set; }
    }
}
