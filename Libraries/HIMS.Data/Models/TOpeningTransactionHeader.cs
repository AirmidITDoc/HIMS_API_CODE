using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOpeningTransactionHeader
    {
        public long OpeningHid { get; set; }
        public string? OpeningDocNo { get; set; }
        public string? StoreId { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? OpeningTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
