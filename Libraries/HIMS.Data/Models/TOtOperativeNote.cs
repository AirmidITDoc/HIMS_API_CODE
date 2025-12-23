using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtOperativeNote
    {
        public long OperativeNotesId { get; set; }
        public long? OtreservationId { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public string? OperativeNotes { get; set; }
        public long? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
    }
}
