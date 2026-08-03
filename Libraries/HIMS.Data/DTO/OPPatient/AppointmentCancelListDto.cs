using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public  class AppointmentCancelListDto
    {
        public DateTime? IsCancelledDate { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime VisitTime { get; set; }
        public string? GenderName { get; set; }
        public string? PatientName { get; set; }
        public string? MobileNo { get; set; }
        public string Age { get; set; }
        public long VisitId { get; set; }
        public long RegID { get; set; }
        public string? OPDNo { get; set; }
        public DateTime? FollowupDate { get; set; }
        public long DoctorId { get; set; }
        public string? Doctorname { get; set; }
        public long? RefDocId { get; set; }
        public string? RefDocName { get; set; }
        public string? DepartmentName { get; set; }

    }
}
