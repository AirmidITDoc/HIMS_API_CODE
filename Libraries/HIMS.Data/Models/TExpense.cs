using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TExpense
    {
        public long ExpId { get; set; }
        public DateTime? ExpDate { get; set; }
        public DateTime? ExpTime { get; set; }
        public long? ExpHeadId { get; set; }
        public long? ExpCategoryId { get; set; }
        public byte? ExpType { get; set; }
        public string? SequenceNo { get; set; }
        public string? VoucharNo { get; set; }
        public decimal? ExpAmount { get; set; }
        public string? PersonName { get; set; }
        public string? Narration { get; set; }
        public string? Utrno { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? CancelledDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
