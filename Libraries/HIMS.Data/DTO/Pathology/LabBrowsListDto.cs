using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public  class LabBrowsListDto
    {
        public string? PbillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? BillTime { get; set; }
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? PatientAge { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? DoctorName { get; set; }
        public string? RefDoctorName { get; set; }
        public string? HospitalName { get; set; }
        public string? TariffName { get; set; }
        public string? CompanyName { get; set; }
        public string? DepartmentName { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        
       public string? OPD_IPD_ID { get; set; }
        
      //public byte? IsCancelled { get; set; }
       // public long? OPD_IPD_Type { get; set; }


        public string? PaidAmt { get; set; }
        
        public decimal? BalanceAmt { get; set; }
        public decimal? CashPay { get; set; }
        public decimal? ChequePay { get; set; }
        public decimal? CardPay { get; set; }
        public decimal? AdvUsedPay { get; set; }
        public decimal? OnlinePay { get; set; }
        public string? OPDNo { get; set; }
        public string? PayCount { get; set; }
        
        public decimal? RefundAmount { get; set; }
        public string? RefundCount { get; set; }
        public string? CashCounterName { get; set; }
        public string? BillNo { get; set; }
        public long? CompanyId { get; set; }
        public string? PatientType { get; set; }
        public string? AadharCardNo { get; set; }
        public string? EmailId { get; set; }
    }
    public class LabResultCompletedListDto
    {
        public long? PathTestID { get; set; }
        public string? ServiceName { get; set; }
        public bool? IsPathOutSource { get; set; }
        public bool? IsRadOutSource { get; set; }
        public long? PathReportID { get; set; }
        public long? ServiceId { get; set; }
        public long? OPDIPDId { get; set; }
        public long? OpdIpdType { get; set; }
        public string? PatientType { get; set; }
        public long? OutSourceId { get; set; }
        public long? AddedBy { get; set; }
        public long? ReportCompletedUser { get; set; }
        public string? CategoryName { get; set; }
        public string? OutSourceLabName { get; set; }
        public string? UserName { get; set; }
        public string? VerifiedUserName { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public bool? IsTemplateTest { get; set; }
        public bool? IsSampleCollection { get; set; }
        public bool? IsVerifySign { get; set; }
        public string? SampleNo { get; set; }
        public string? OutSourceStatus { get; set; }
        public DateTime? DOA { get; set; }   
        public DateTime? DOT { get; set; }   
        public string? SampleCollectionTime { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public string? OutSourceSampleSentDateTime { get; set; }
        public string? OutSourceReportCollectedDateTime { get; set; }
        public string? OutSourceCreatedDateTime { get; set; }
        public string? OutSourceModifiedDateTime { get; set; }
        public long? OutSourceCreatedBy { get; set; }
        public long? OutSourceModifiedBy { get; set; }
    }
}
    

