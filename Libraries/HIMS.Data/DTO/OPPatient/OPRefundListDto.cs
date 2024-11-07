using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class OPRefundListDto
    {
        public int RefundId { get; set; }

        public DateTime RefundDate { get; set; }
        // public long RegNo { get; set; }
        public string PatientName { get; set; }
        //public string MobileNo { get; set; }
        //public string DoctorName { get; set; }
        //public DateTime VisitDate { get; set; }


        //public string DVisitDate { get; set; }
    }
}
