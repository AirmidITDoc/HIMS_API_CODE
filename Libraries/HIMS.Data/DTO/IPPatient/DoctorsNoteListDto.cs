using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class DoctorsNoteListDto
    {
        public long DoctNoteId { get; set; }
        public long? AdmId { get; set; }
        public string Tdate { get; set; }
        public string Ttime { get; set; }
        public string? DoctorsNotes { get; set; }
        public long? IsAddedBy { get; set; }
        public string? VTDate { get; set; }
        public string? PatientName { get; set; }
        public string? GenderName { get; set; }
        public string? Age { get; set; }
        public string? RegNo { get; set; }
        public string? UserName { get; set; }

    }
}
