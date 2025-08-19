using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class PrescriptionReturnListDto
    {

        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public long? PresReId { get; set; }
        public string? Date { get; set; }
        public string? PresTime { get; set; }
        public long OPIPId { get; set; }
        public string? VstAdmDate { get; set; }
        public string? StoreName { get; set; }
        public byte OPIPType { get; set; }
        //public long? IpmedId { get; set; }
        public string? PatientType { get; set; }
        public string? DoctorName { get; set; }
        public string? IPDNo { get; set; }
        public string? BedName { get; set; }
        public string? RoomName { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string? AdmissionTime { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }




    }
}
