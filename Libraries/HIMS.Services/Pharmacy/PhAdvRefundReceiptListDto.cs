using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public  class PhAdvRefundReceiptListDto
    {
        
        public string? PatientName { get; set; }
        public string? GenderName { get; set; }
        public string? RegNo { get; set; }
        public long RefundId { get; set; }
        public DateTime RefundDate { get; set; }
        public string? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public decimal? RefundAmount { get; set; }
        public string Remark { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentTime { get; set; }
        public decimal CashPayAmount { get; set; }
        public decimal ChequePayAmount { get; set; }
        public string ChequeNo { get; set; }
        public string? BankName { get; set; }
        public DateTime ChequeDate { get; set; }
        public decimal CardPayAmount { get; set; }
        public string CardNo { get; set; }
        public string CardBankName { get; set; }
        public DateTime CardDate { get; set; }
        public string? TransactionType { get; set; }
        public string AddedBy { get; set; }
        public string? UserName { get; set; }
        public string? HospitalName { get; set; }
        public string? HospitalAddress { get; set; }
        public string? HospitalCity { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public bool? IsCancelled { get; set; }

    }
}
