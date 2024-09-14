using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ACustomerPaymentSummary
    {
        public long CustPayTranId { get; set; }
        public long? CustomerId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public decimal? Amount { get; set; }
        public string? Comments { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
        public long? ModifiedBy { get; set; }

        public virtual ACustomerInformation? Customer { get; set; }
    }
}
