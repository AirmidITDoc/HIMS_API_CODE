using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class LabRadiologyListDto
    {
        public long RadReportId { get; set; }
        public string? RegDate { get; set; }
        public string? RegTime { get; set; }
        public string LabRequestNo { get; set; }
        public string AgeYear { get; set; }
        public string AgeMonth { get; set; }
        public string AgeDay { get; set; }
        public string? RadTime { get; set; }
        public long opdipdid { get; set; }
        public long? RadTestID { get; set; }
        public long? IsCancelled { get; set; }
        public long? ChargeId { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public long? OutSourceId { get; set; }
        public string? OutSourceLabName { get; set; }
        public string? OutSourceSampleSentDateTime { get; set; }
        public long? OutSourceStatus { get; set; }
        public string OutSourceReportCollectedDateTime { get; set; }
        public long? OutSourceCreatedBy { get; set; }
        public long? OutSourceModifiedby { get; set; }
        public string? OutSourceModifiedDateTime { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientName { get; set; }
        public long TariffId { get; set; }
        public string TariffName { get; set; }
        public string RefDoctorName { get; set; }
        public string? CompanyName { get; set; }
        public long TestId { get; set; }
        public string? TestName { get; set; }
        public string? CategoryName { get; set; }
        public long BillNo { get; set; }
        public long opdipdtype { get; set; }
        public string? PBillNo { get; set; }
        public string? UserName { get; set; }
        public bool? IsVerified { get; set; }
        public string? IsVerifyedDate { get; set; }
        public string? ServiceName { get; set; }
        public string? IsRadOutSource { get; set; }
        public string? PatientType { get; set; }
        public DateTime? ReportDate { get; set; }
        public string? ReportTime { get; set; }
        public long? RadResultDr1 { get; set; }






    }
}
