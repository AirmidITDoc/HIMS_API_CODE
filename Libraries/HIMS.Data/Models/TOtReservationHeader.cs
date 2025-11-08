using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtReservationHeader
    {
        public TOtReservationHeader()
        {
            TOtReservationAttendingDetails = new HashSet<TOtReservationAttendingDetail>();
            TOtReservationDiagnoses = new HashSet<TOtReservationDiagnosis>();
            TOtReservationSurgeryDetails = new HashSet<TOtReservationSurgeryDetail>();
        }

        public long OtreservationId { get; set; }
        public DateTime? OtreservationDate { get; set; }
        public DateTime? OtreservationTime { get; set; }
        public string? OtreservationNo { get; set; }
        public long? OtrequestId { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public string? BloodGroup { get; set; }
        public long? CategoryType { get; set; }
        public long? Ottable { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public DateTime? EstimateTime { get; set; }
        public string? Diagnosis { get; set; }
        public string? Comments { get; set; }
        public bool? ReservationType { get; set; }
        public bool? Pacrequired { get; set; }
        public bool? EquipmentsRequired { get; set; }
        public bool? ClearanceMedical { get; set; }
        public bool? ClearanceFinancial { get; set; }
        public bool? Infective { get; set; }
        public long? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }

        public virtual ICollection<TOtReservationAttendingDetail> TOtReservationAttendingDetails { get; set; }
        public virtual ICollection<TOtReservationDiagnosis> TOtReservationDiagnoses { get; set; }
        public virtual ICollection<TOtReservationSurgeryDetail> TOtReservationSurgeryDetails { get; set; }
    }
}
