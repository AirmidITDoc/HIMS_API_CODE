using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class IpcompanyPayment
    {
        public long CompanyId { get; set; }
        public long? PatientCompanyId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Tdsamount { get; set; }
        public decimal? PayAmount { get; set; }
        public decimal? WrfAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public DateTime? SettlementDate { get; set; }
        public DateTime? SettlementTime { get; set; }
        public long? BillNo { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public DateTime? ChequeDate { get; set; }
        public decimal? CardPayAmount { get; set; }
        public string? CardNo { get; set; }
        public string? CardBankName { get; set; }
        public DateTime? CardDate { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public DateTime? Neftdate { get; set; }
        public byte? OpdIpdType { get; set; }
        public long PaymentId { get; set; }
    }
}
