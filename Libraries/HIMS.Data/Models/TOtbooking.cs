using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtbooking
    {
        public long OtbookingId { get; set; }
        public DateTime? TranDate { get; set; }
        public DateTime? TranTime { get; set; }
        public long? OpIpId { get; set; }
        public bool? OpIpType { get; set; }
        public DateTime? Opdate { get; set; }
        public DateTime? Optime { get; set; }
        public int? Duration { get; set; }
        public long? OttableId { get; set; }
        public long? SurgeonId { get; set; }
        public long? SurgeonId1 { get; set; }
        public long? AnestheticsDr { get; set; }
        public long? AnestheticsDr1 { get; set; }
        public string? Surgeryname { get; set; }
        public long? ProcedureId { get; set; }
        public string? AnesthType { get; set; }
        public bool? UnBooking { get; set; }
        public string? Instruction { get; set; }
        public long? IsAddedBy { get; set; }
        public long? IsUpdatedBy { get; set; }
        public long? OttypeId { get; set; }
    }
}
