using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class LabResultPrintedListDto
    {
      public long LabPatientId { get; set; }
        public string? DoctorName { get; set; }
        public string? LBL { get; set; }
        public string? PBillNo { get; set; }
        public bool IsSampleCollection { get; set; }
        public string? PathReportID { get; set; }
        public string? SampleNo { get; set; }
        public long? BillNo { get; set; }
        public DateTime? PathDate { get; set; }
        public string? WardName { get; set; }
        public string? DepartmentName { get; set; }
        public string? PatientType { get; set; }
        public DateTime? VADate { get; set; }
        public DateTime? VATime { get; set; }
        public string? LabRequestNo { get; set; }
        public string? PatientName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? MobileNo { get; set; }
        public string? AgeYear { get; set; }
       public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public byte? OPD_IPD_Type { get; set; }
        public string? GenderName { get; set; }
        public string? HospitalName { get; set; }
        public DateTime? SampleCollectionTime { get; set; }
       public long? OutSourceId { get; set; }
        public DateTime? SampleReceviedDateTime { get; set; }
       public long? SampleReceviedUserId { get; set; }
        public bool IsSampleReceivedStatus { get; set; }
        public string? ServiceName { get; set; }
        public bool IsPathOutSource { get; set; }
        public bool IsRadOutSource { get; set; }
        public string? RefDoctorName { get; set; }
        public string? UserName { get; set; }
    }
}
