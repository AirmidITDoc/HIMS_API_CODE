using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class AdvanceRefundOPReceiptListDto
    {
            public string? RegNo { get; set; }
            public string? PatientName { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public string? GenderName { get; set; }
        public string? AgeGender { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? FirstName { get; set; }

        public double? AdvanceAmount { get; set; }
        public double? AdvanceUsedAmount { get; set; }
        public double? BalanceAmount { get; set; }

        public long? RefundId { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Remark { get; set; }

        public long? PaymentId { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }

        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public DateTime? ChequeDate { get; set; }

        public decimal? CardPayAmount { get; set; }
        public string CardNo { get; set; }
        public string? CardBankName { get; set; }
        public DateTime? CardDate { get; set; }

        public long? TransactionType { get; set; }
        public string? AddedBy { get; set; }
        public string? UserName { get; set; }

        public string? HospitalName { get; set; }
        public string? HospitalAddress { get; set; }
        public string? HospitalCity { get; set; }
        public string Pin { get; set; }
        public string? Phone { get; set; }

        public bool? IsCancelled { get; set; }
        public string? Comment { get; set; }
    }

}

