using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDoctorsNote
    {
        public long DoctNoteId { get; set; }
        public long? AdmId { get; set; }
        public DateTime? Tdate { get; set; }
        public DateTime? Ttime { get; set; }
        public string? DoctorsNotes { get; set; }
        public long? IsAddedBy { get; set; }
    }
}
