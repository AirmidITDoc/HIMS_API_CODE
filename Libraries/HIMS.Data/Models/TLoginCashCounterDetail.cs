using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TLoginCashCounterDetail
    {
        public long LoginCashCounterDetId { get; set; }
        public long LoginId { get; set; }
        public long CashCounterId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual LoginManager Login { get; set; } = null!;
    }
}
