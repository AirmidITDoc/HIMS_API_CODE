using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TExpense
    {
        public long ExpId { get; set; }
        public DateTime? ExpDate { get; set; }
        public DateTime? ExpTime { get; set; }
        public byte? ExpType { get; set; }
        public decimal? ExpAmount { get; set; }
        public string? PersonName { get; set; }
        public string? Narration { get; set; }
        public long? IsAddedby { get; set; }
        public long? IsUpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public string? VoucharNo { get; set; }
        public long? ExpHeadId { get; set; }
    }
}
