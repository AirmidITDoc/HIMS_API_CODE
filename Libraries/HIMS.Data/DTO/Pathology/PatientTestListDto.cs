using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class PatientTestListDto
    {
        public string  RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? OP_IP_No { get; set; }
        public string? VADate { get; set; }
        public string? DOA { get; set; }
        public string? VATime { get; set; }
        public string? DoctorName { get; set; }
        public long Visit_Adm_ID { get; set; }
        public string DOT { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public long? OPD_IPD_ID { get; set; }
        public long? OPD_IPD_Type { get; set; }
        public string PatientType { get; set; }
        public string? BillNo { get; set; }
        public string? PBillNo { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? GenderName { get; set; }
        public long GenderId { get; set; }
        public long Adm_Visit_docId { get; set; }
        public DateTime PathDate { get; set; }
        public string? MobileNo { get; set; }





    }
}

