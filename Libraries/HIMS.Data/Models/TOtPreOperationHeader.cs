using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtPreOperationHeader
    {
        public TOtPreOperationHeader()
        {
            TOtPreOperationAttendingDetails = new HashSet<TOtPreOperationAttendingDetail>();
            TOtPreOperationCathlabDiagnoses = new HashSet<TOtPreOperationCathlabDiagnosis>();
            TOtPreOperationDiagnoses = new HashSet<TOtPreOperationDiagnosis>();
            TOtPreOperationSurgeryDetails = new HashSet<TOtPreOperationSurgeryDetail>();
        }

        public long OtpreOperationId { get; set; }
        public long? OtreservationId { get; set; }
        public DateTime? OtpreOperationDate { get; set; }
        public DateTime? OtpreOperationTime { get; set; }
        public string? OtpreOperationNo { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public long? CategoryType { get; set; }
        public long? Ottable { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public int? Duration { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public int? BloodArranged { get; set; }
        public int? Pacrequired { get; set; }
        public int? EquipmentsRequired { get; set; }
        public int? Infective { get; set; }
        public bool? ClearanceMedical { get; set; }
        public bool? ClearanceFinancial { get; set; }
        public long? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual ICollection<TOtPreOperationAttendingDetail> TOtPreOperationAttendingDetails { get; set; }
        public virtual ICollection<TOtPreOperationCathlabDiagnosis> TOtPreOperationCathlabDiagnoses { get; set; }
        public virtual ICollection<TOtPreOperationDiagnosis> TOtPreOperationDiagnoses { get; set; }
        public virtual ICollection<TOtPreOperationSurgeryDetail> TOtPreOperationSurgeryDetails { get; set; }
    }
}
