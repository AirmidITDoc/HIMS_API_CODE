using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ACustomerInformation
    {
        public ACustomerInformation()
        {
            ACustomerPaymentSummaries = new HashSet<ACustomerPaymentSummary>();
        }

        public long CustomerId { get; set; }
        public string? CustomerNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerContactNo { get; set; }
        public string? CustomerContactPerson { get; set; }
        public long? CustomerCity { get; set; }
        public decimal? OrderAmount { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime? Amcdate { get; set; }
        public int? Amcduration { get; set; }
        public DateTime? NextAmcdate { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<ACustomerPaymentSummary> ACustomerPaymentSummaries { get; set; }
    }
}
