using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SsMobileAppLogin
    {
        public long CustomerId { get; set; }
        public string CustomerMobileNo { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string? CustomerAddress { get; set; }
        public int? CustomerPinCode { get; set; }
        public string? CustomerPasscode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
