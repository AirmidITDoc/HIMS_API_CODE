using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Nursing
{
    public class AdmisionListNursingListDto
    {
        public string RegNo { get; set; }
        public string PatientName { get; set; }
        public long AdmissionID { get; set; }
        public long WardId { get; set; }
        public string RoomName { get; set; }
        public string BedName { get; set; }
        public string AgeYear { get; set; }
        public string AgeMonth { get; set; }
        public string AgeDay { get; set; }
        public long DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public long? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public long? DocNameID { get; set; }
        public long? TariffId { get; set; }
        public string? IPDNo { get; set; }
    }
}
