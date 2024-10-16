﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class Refund
    {
        public Refund()
        {
            TRefundDetails = new HashSet<TRefundDetail>();
            AddCharges = new HashSet<AddCharge>();
            Payments = new HashSet<Payment>();
        }

        public long RefundId { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public long? BillId { get; set; }
        public long? AdvanceId { get; set; }
        public bool? Opdipdtype { get; set; }
        public long? Opdipdid { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Remark { get; set; }
        public long? TransactionId { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? CashCounterId { get; set; }
        public bool? IsRefundFlag { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TRefundDetail> TRefundDetails { get; set; }
        public virtual ICollection<AddCharge> AddCharges { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

    }
}
