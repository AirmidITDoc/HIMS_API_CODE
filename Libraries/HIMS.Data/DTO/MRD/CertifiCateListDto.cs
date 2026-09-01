using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.MRD
{
    public  class CertifiCateListDto
    {
        public long DocId { get; set; }

        public DateTime? MLCDate { get; set; }

        public DateTime? MLCTime { get; set; }

        public string CertificateNo { get; set; }

        public long OP_IP_Id { get; set; }

        public string PatientName { get; set; }

        public string RegNo { get; set; }

        public string DepartmentName { get; set; }

        public DateTime? Accident_Date { get; set; }

        public DateTime? Accident_Time { get; set; }

        public string CauseofInjuries { get; set; }

        public string Details_Injuries { get; set; }

        public string AgeofInjuries { get; set; }

        public string TreatingDoctorId { get; set; }

        public string TreatingDoctorId1 { get; set; }

        public string TreatingDoctorId2 { get; set; }

        public string UserName { get; set; }

        public string Label { get; set; }

        public string PatientType { get; set; }

        public string HospitalName { get; set; }

        public string AdmittedDoctorName { get; set; }

        public string RefDoctorName { get; set; }

        public string RoomName { get; set; }

        public string BedName { get; set; }

        public string IPDNo { get; set; }

        public string CompanyName { get; set; }

        public string TariffName { get; set; }

        public int IsCancelled { get; set; }

        public int OP_IP_Type { get; set; }
    }
}
