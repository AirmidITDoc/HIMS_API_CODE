using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class LabregBilldetailListDto
    {
        public string? BillNo { get; set; }
        public DateTime BillTime { get; set; }
        public long ChargesId { get; set; }
        public string ServiceName { get; set; }
        public DateTime ChargesTime { get; set; }
        public double Price { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public string? DoctorName { get; set; }
        public bool? IsCompleted { get; set; }

    }
    public class LabResultListDto
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
    public class LabResultDetailsListDto
    {
        public string? LabRequestNo { get; set; }
        public string PatientName { get; set; }
        public string OP_IP_No { get; set; }
        public string RegDate { get; set; }
        public string RegTime { get; set; }
        public string DoctorName { get; set; }
        public long? Visit_Adm_ID { get; set; }
        public long? PathTestID { get; set; }
        public string? ServiceName { get; set; }
        public string? IsPathOutSource { get; set; }
        public string? IsRadOutSource { get; set; }
        public long? PathReportID { get; set; }
        public long? ServiceId { get; set; }
        public string? DOA { get; set; }
        public string? DOT { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public long OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public string? PatientType { get; set; }
        public string? PBillNo { get; set; }
        public string? AgeYear { get; set; }
        public string? GenderName { get; set; }
        public long? IsTemplateTest { get; set; }
        public string? CategoryName { get; set; }
        public long? ChargeId { get; set; }
        public long? Adm_Visit_docId { get; set; }
        public string? SampleNo { get; set; }
        public string? SampleCollectionTime { get; set; }
        public string? IsSampleCollection { get; set; }
        public bool? IsVerifySign { get; set; }
        public long IsVerifyid { get; set; }
        public string? IsVerifyedDate { get; set; }
        public long? OutSourceId { get; set; }
        public string? OutSourceLabName { get; set; }
        public string? OutSourceSampleSentDateTime { get; set; }
        public long? OutSourceCreatedBy { get; set; }
        public string? OutSourceCreatedDateTime { get; set; }
        public long? OutSourceModifiedby { get; set; }
        public string? OutSourceModifiedDateTime { get; set; }
        public string? UserName { get; set; }
        public string? VerifiedUserName { get; set; }
        public long AddedBy { get; set; }
        public long UpdatedBy { get; set; }
        public string? ReportDate { get; set; }
        public string? ReportTime { get; set; }
        public string? ReportCompletedUser { get; set; }




        








    }


}
																
