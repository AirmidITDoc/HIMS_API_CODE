using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class BillDetail
    {
        public long BillDetailId { get; set; }
        public long? BillNo { get; set; }
        public long? ChargesId { get; set; }
    }
}
