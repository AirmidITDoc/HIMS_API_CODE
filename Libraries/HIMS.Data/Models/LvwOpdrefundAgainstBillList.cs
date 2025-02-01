using System;
using System.Collections.Generic;
using HIMS.Data.Models;

namespace HIMS.Data.Models
{
    public partial class LvwOpdrefundAgainstBillList
    {
        public long RefundId { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public string? PbillNo { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientType { get; set; }
        public string? TariffName { get; set; }

        public string? CompanyName { get; set; }
        public DateTime? MobileNo { get; set; }
        public decimal? RefundAmount { get; set; }
        public long PaymentId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public decimal? CardPayAmount { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public string? Remark { get; set; }
        public string? PaymentRemark { get; set; }
        public byte? TransactionId { get; set; }
        public long? TransactionType { get; set; }
        public long? BillId { get; set; }
        public string? RefunDate { get; set; }
        public DateTime? VisitDate { get; set; }
        public long? RegNo { get; set; }
        public long? TotalAmt { get; set; }
        public long? ConcessionAmt { get; set; }
        public long? NetPayableAmt { get; set; }
        public long? BalanceAmt { get; set; }
        public long? AddedBy { get; set; }
        public string? RefDoctorName { get; set; }
        public string? HospitalName { get; set; }


    }
}
