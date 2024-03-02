using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TGrnsupPayment
    {
        public long SupPayId { get; set; }
        public DateTime? SupPayDate { get; set; }
        public DateTime? SupPayTime { get; set; }
        public string? SupPayNo { get; set; }
        public long? GrnId { get; set; }
        public decimal? CashPayAmt { get; set; }
        public decimal? ChequePayAmt { get; set; }
        public DateTime? ChequePayDate { get; set; }
        public string? ChequeBankName { get; set; }
        public string? ChequeNo { get; set; }
        public string? Remarks { get; set; }
        public long? IsAddedBy { get; set; }
        public long? IsUpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDatetime { get; set; }
        public string? PartyReceiptNo { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public DateTime? Neftdate { get; set; }
        public decimal? PayTmamount { get; set; }
        public string? PayTmtranNo { get; set; }
        public DateTime? PayTmdate { get; set; }
    }
}
