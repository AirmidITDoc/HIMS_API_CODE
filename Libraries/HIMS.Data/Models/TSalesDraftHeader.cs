using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TSalesDraftHeader
    {
        public long DsalesId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public string? SalesNo { get; set; }
        public long? OpIpId { get; set; }
        public long? OpIpType { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public long? ConcessionReasonId { get; set; }
        public long? ConcessionAuthorizationId { get; set; }
        public long? CashCounterId { get; set; }
        public bool? IsSellted { get; set; }
        public bool? IsPrint { get; set; }
        public long? UnitId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? ExternalPatientName { get; set; }
        public string? DoctorName { get; set; }
        public long? StoreId { get; set; }
        public string? CreditReason { get; set; }
        public long? CreditReasonId { get; set; }
        public bool? IsClosed { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsPrescription { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
    }
}
