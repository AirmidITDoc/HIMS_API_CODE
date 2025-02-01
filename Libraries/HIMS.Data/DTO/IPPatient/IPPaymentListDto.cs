using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class IPPaymentListDto
    {
        public long PaymentId { get; set; }
        public long? BillNo { get; set; }
        public long? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? PrefixName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? NetPayableAmt { get; set; }
        public long? BalanceAmt { get; set; }
        public long? Remark { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? HospitalName { get; set; }
        public string? HospitalAddress { get; set; }
        public string? City { get; set; }
        public long? Pin { get; set; }
        public long? Phone { get; set; }
        public long? IsCancelled { get; set; }
        public long? IPDNo { get; set; }
        public long? PBillNo { get; set; }
        public long? CashPayAmount { get; set; }
        public long? ChequePayAmount { get; set; }
        public long? CardPayAmount { get; set; }
        public long? AdvanceUsedAmount { get; set; }
        public long? TransactionType { get; set; }
        public long? ReceiptNo { get; set; }
        public long? AdvanceId { get; set; }
        public long? RefundId { get; set; }
        public string? UserName { get; set; }
        public DateTime? PayDate { get; set; }
        public long? PaidAmount { get; set; }
        public long? TotalAmt { get; set; }
        public long? NEFTPayAmount { get; set; }
        public long? PayTMAmount { get; set; }
    }
}
