using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class Bill
    {
        public Bill()
        {
            AddCharges = new HashSet<AddCharge>();
            BillDetails = new HashSet<BillDetail>();
        }

        public long BillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? BillTime { get; set; }
        public long? RegNo { get; set; }
        public long? OpdIpdId { get; set; }
        public byte? OpdIpdType { get; set; }
        public string? PatientName { get; set; }
        public string? Ipdno { get; set; }
        public long? AgeYear { get; set; }
        public long? AgeMonth { get; set; }
        public long? AgeDays { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public decimal? TotalAdvanceAmount { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public bool? PatientType { get; set; }
        public string? CompanyName { get; set; }
        public decimal? CompanyAmt { get; set; }
        public decimal? PatientAmt { get; set; }
        public decimal? GovtApprovedAmt { get; set; }
        public long? TariffId { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public long? UnitId { get; set; }
        public long? CashCounterId { get; set; }
        public long? ConcessionReasonId { get; set; }
        public string? BillPrefix { get; set; }
        public string? PbillNo { get; set; }
        public string? BillMonth { get; set; }
        public string? BillYear { get; set; }
        public string? PrintBillNo { get; set; }
        public decimal? RefundAmount { get; set; }
        public bool? IsSettled { get; set; }
        public bool? IsPrinted { get; set; }
        public bool? IsFree { get; set; }
        public long? CompanyId { get; set; }
        public long? InterimOrFinal { get; set; }
        public string? CompanyRefNo { get; set; }
        public long? ConcessionAuthorizationName { get; set; }
        public bool? IsBillCheck { get; set; }
        public double? SpeTaxPer { get; set; }
        public decimal? SpeTaxAmt { get; set; }
        public bool? IsBillShrHold { get; set; }
        public string? DiscComments { get; set; }
        public decimal? ChTotalAmt { get; set; }
        public decimal? ChConcessionAmt { get; set; }
        public decimal? ChNetPayAmt { get; set; }
        public decimal? CompDiscAmt { get; set; }
        public long? IsCancelled { get; set; }
        public long? AddedBy { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<AddCharge> AddCharges { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
