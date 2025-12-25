using System;
using System.Collections.Generic;
using System.Linq;
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
}
