using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMpesaResponse
    {
        public long Id { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? MerchantRequestId { get; set; }
        public string? CheckoutRequestId { get; set; }
        public int? ResultCode { get; set; }
        public string? ResultDesc { get; set; }
        public decimal? Amount { get; set; }
        public string? MpesaReceiptNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? ResponseOn { get; set; }
    }
}
