using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Purchase
{
    public class SupplierPaymentStatusListDto
    {
        public long SupplierId { get; set; }

        public long? GRNID { get; set; }

        public String? SupplierName { get; set; }
        public long? StoreId { get; set; }
        public String? StoreName { get; set; }
        public String? GrnNumber { get; set; }

        public String? GRNDate { get; set; }

        public String? GRNTime { get; set; }
        public String? InvoiceNo { get; set; }

        public String? DeliveryNo { get; set; }

        public String? GateEntryNo { get; set; }
        public bool? Cash_CreditType { get; set; }

        public bool? GRNType { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalDiscAmount { get; set; }
        public decimal? TotalVATAmount { get; set; }

        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalAmount { get; set; }
        public String? Remark { get; set; }

        public String? ReceivedBy { get; set; }

        //public bool? IsVerified { get; set; }
        //public bool? IsClosed { get; set; }
        public long? AddedBy { get; set; }
        //public bool? IsPaymentProcess { get; set; }

        public String? PaymentPrcDate { get; set; }

        public String? ProcessDes { get; set; }
        public String? InvDate { get; set; }
        public String? DaysOfInv { get; set; }

        public decimal? ReturnNetAmt { get; set; }

    }
}
