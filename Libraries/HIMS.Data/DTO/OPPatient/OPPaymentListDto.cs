using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class OPPaymentListDto
    {
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? AgeYear { get; set; }
        public string? DoctorName { get; set; }
        public string? RefDoctorName { get; set; }
        public string? MobileNo { get; set; }
        public string? OPDNo { get; set; }
        public string? CompanyName { get; set; }
        public string? HospitalName { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? PBillNo { get; set; }
        public string? BillDate { get; set; }
        public decimal BillAmount { get; set; }
        public double? DiscAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal BalanceAmt { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public string? ReceiptNo { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public decimal? CardPayAmount { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public decimal? PayTmamount { get; set; }
        public decimal OnlinePay { get; set; }
        public long PaymentId { get; set; }
        public string? UserName { get; set; }
        public long? CompanyId { get; set; }
        public long BillNo { get; set; }


    }
}
