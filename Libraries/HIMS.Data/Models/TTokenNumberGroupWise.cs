using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TTokenNumberGroupWise
    {
        public long TokenId { get; set; }
        public DateTime? BillDate { get; set; }
        public long? BillNo { get; set; }
        public long? GroupId { get; set; }
        public long? TokenNo { get; set; }
        public bool? IsStatus { get; set; }
    }
}
