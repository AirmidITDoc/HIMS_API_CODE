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
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
