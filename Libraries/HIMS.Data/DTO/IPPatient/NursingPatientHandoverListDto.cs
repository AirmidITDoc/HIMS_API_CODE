using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class NursingPatientHandoverListDto
    {
        public long PatHandId { get; set; }
        public long AdmID { get; set; }
        public string? VDate { get; set; }
        public string? MTime { get; set; }
        public string? ShiftInfo { get; set; }
        public string? PatHandI { get; set; }
        public string? PatHandR { get; set; }
        public string? PatHandB { get; set; }
        public string? PatHandS { get; set; }
        public string CreatedBy { get; set; }
        public string? Comments { get; set; }
        public string? PatientName { get; set; }
        public string? GenderName { get; set; }
        public string? RegNo { get; set; }
        public string? AgeYear { get; set; }

    }
}
