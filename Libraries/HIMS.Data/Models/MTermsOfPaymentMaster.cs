using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MTermsOfPaymentMaster
    {
        public long Id { get; set; }
        public string? TermsOfPayment { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpDatedBy { get; set; }
    }
}
