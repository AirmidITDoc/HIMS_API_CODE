using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCustomerPayment
    {
        public long PaymentId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public long? CustomerId { get; set; }
        public decimal? Amount { get; set; }
        public string? Comments { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedByDateTime { get; set; }
    }
}
