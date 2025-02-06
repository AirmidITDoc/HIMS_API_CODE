using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class TDoctorPatientHandoverListDto
    {
        public long DocHandId { get; set; }
        public long AdmID { get; set; }
        public DateTime TDate { get; set; }
        public string VDate { get; set; }
        public string MTime { get; set; }
        public string ShiftInfo { get; set; }
        public string PatHand_I { get; set; }
        public string PatHand_S { get; set; }
        public string PatHand_B { get; set; }
        public string PatHand_A { get; set; }
        public string PatHand_R { get; set; }
        public string CreatedBy { get; set; }
        public string PatientName { get; set; }
        public string GenderName { get; set; }


    }
}
