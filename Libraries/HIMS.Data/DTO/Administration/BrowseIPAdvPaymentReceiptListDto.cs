using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class BrowseIPAdvPaymentReceiptListDto
    {
        public string PrefixName { get; set; }
        public string? PatientName { get; set; }
        public long? PaymentId { get; set; }
        public string? PayDate { get; set; }
        public string? RegNo { get; set; }
        public decimal? TotalAmt { get; set; }
        public decimal? PaidAmount { get; set; }
        public string? PBillNo { get; set; }
        public string? ReceiptNo { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public DateTime? ChequeDate { get; set; }
        public decimal? CardPayAmount { get; set; }
        public string? CardNo { get; set; }
        public string? CardBankName { get; set; }
        public DateTime? CardDate { get; set; }
        public string? UserName { get; set; }
        public int? AdvanceUsedAmount { get; set; }
        public decimal? NEFTPayAmount { get; set; }
        public decimal? PayTMAmount { get; set; }
        public long? AdvanceId { get; set; }

        public long? TransactionType { get; set; }






    }
}
