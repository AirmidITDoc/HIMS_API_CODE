using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPhSm
    {
        public long PhSmId { get; set; }
        public DateTime? PhSmDate { get; set; }
        public DateTime? PhSmTime { get; set; }
        public decimal? PhNetSales { get; set; }
        public decimal? PhColAmt { get; set; }
        public decimal? MedNetSales { get; set; }
        public decimal? MedColAmt { get; set; }
        public decimal? RgAmt { get; set; }
        public decimal? VajpayAmt { get; set; }
        public decimal? CharityAmt { get; set; }
        public long? IsAddedBy { get; set; }
        public DateTime? IsAddedDate { get; set; }
        public long? IsUpdatedBy { get; set; }
        public DateTime? IsUpdatedDate { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
    }
}
