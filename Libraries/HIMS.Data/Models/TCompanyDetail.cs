using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCompanyDetail
    {
        public long CompanyDetId { get; set; }
        public long? CompanyHeaderId { get; set; }
        public long? BillNo { get; set; }
        public byte? OpIpType { get; set; }
        public decimal? BillAmount { get; set; }
        public decimal? Tdsamount { get; set; }
        public decimal? WrfAmount { get; set; }
        public decimal? NetPayAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
    }
}
