using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPhColHadOvToAcc
    {
        public long TranId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public DateTime? PayDate { get; set; }
        public decimal? PhAmount { get; set; }
        public string? PersonName { get; set; }
        public string? Comments { get; set; }
        public long? IsAddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
    }
}
