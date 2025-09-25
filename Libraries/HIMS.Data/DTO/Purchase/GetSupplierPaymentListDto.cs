using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Purchase
{
    public class GetSupplierPaymentListDto
    {
        public string? GrnNumber { get; set; }
        public DateTime? Grndate { get; set; }
        public string? SupplierName { get; set; }
        public long SupPayId { get; set; }
        public string? SupPayDate { get; set; }
        public string? SupPayTime { get; set; }
        public string? SupPayNo { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? CashPayAmt { get; set; }
        public decimal? ChequePayAmt { get; set; }
        public DateTime? ChequePayDate { get; set; }
        public string? ChequeBankName { get; set; }
        public string? ChequeNo { get; set; }
        public string? Remarks { get; set; }

        public string? UserName { get; set; }
        public decimal? NetAmount { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? PartyReceiptNo { get; set; }
        public long SupplierId { get; set; }


        

    }
}
