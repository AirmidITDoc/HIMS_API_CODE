using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class LabApprovalResultListDto
    {
        public string? RegNo { get; set; }
        public string PatientName { get; set; }
        public string OP_IP_No { get; set; }
        public DateTime VADate { get; set; }
        public DateTime VATime { get; set; }
        public string DoctorName { get; set; }
        public long? Visit_Adm_ID { get; set; }
        public string? DOA { get; set; }
        public string? DOT { get; set; }
        public bool? IsCompleted { get; set; }
        public string? VIsPrinted { get; set; }
        public long? opdipdid { get; set; }
        public string? opdipdtype { get; set; }
        public string? PatientType { get; set; }
        public string? BillNo { get; set; }
        public string? PBillNo { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? GenderName { get; set; }
        public long? GenderId { get; set; }
        public long? Adm_Visit_docId { get; set; }
        public string? PathDate { get; set; }
        public string? MobileNo { get; set; }
        public string? HospitalName { get; set; }
        public string? DepartmentName { get; set; }
        public long? LabPatientId { get; set; }
        public DateTime? DateofBirth { get; set; }

    }
}
