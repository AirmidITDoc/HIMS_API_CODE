using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOpeningTransactionHeader
    {
        public long OpeningHid { get; set; }
        public string? OpeningDocNo { get; set; }
        public long? StoreId { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? OpeningTime { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
