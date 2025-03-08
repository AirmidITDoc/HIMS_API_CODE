using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Nursing
{
    public class NursingNoteListDto
    {
        public long DocNoteId { get; set; }
        public long AdmID { get; set; }
        public DateTime TDate { get; set; }
        public DateTime TTime { get; set; }
        public string NursingNotes { get; set; }
        public long IsAddedBy { get; set; }
        public DateTime VTDate { get; set; }

        public string PatientName { get; set; }
        public string GenderName { get; set; }
        public long Age { get; set; }

        public long RegNo { get; set; }
    }
}
