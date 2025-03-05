using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class BrowseIPRefundListDto
    {
        public long RefundId { get; set; }
        public string? RegNo { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public long? BillId { get; set; }
        public long? AdvanceId { get; set; }
        public string? PatientName { get; set; }
        public string? GenderName { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Remark { get; set; }
        public long PaymentId { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public string? ChequeDate { get; set; }
        public decimal? CardPayAmount { get; set; }
        public string? CardNo { get; set; }
        public string? CardBankName { get; set; }
        public DateTime? CardDate { get; set; }
        public long? TransactionType { get; set; }
        public string? AddedBy { get; set; }
        public string? UserName { get; set; }
        public string? HospitalName { get; set; }
        public string? HospitalAddress { get; set; }
        public string? HospitalCity { get; set; }
        public string? Phone { get; set; }
        public bool? IsCancelled { get; set; }
        public string? Pin { get; set; }

    }
}
