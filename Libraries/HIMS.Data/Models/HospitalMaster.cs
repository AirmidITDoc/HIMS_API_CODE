namespace HIMS.Data.Models
{
    public partial class HospitalMaster
    {
        public long HospitalId { get; set; }
        public string? HospitalHeaderLine { get; set; }
        public string? HospitalName { get; set; }
        public string? HospitalAddress { get; set; }
        public string? City { get; set; }
        public string? Pin { get; set; }
        public string? Phone { get; set; }
        public string? EmailId { get; set; }
        public string? WebSiteInfo { get; set; }
        public string? Header { get; set; }
        public bool? IsActive { get; set; }
        public long? OpdBillingCounterId { get; set; }
        public long? OpdReceiptCounterId { get; set; }
        public long? OpdRefundBillCounterId { get; set; }
        public long? OpdRefundBillReceiptCounterId { get; set; }
        public long? OpdAdvanceCounterId { get; set; }
        public long? OpdRefundAdvanceCounterId { get; set; }
        public long? IpdAdvanceCounterId { get; set; }
        public long? IpdAdvanceReceiptCounterId { get; set; }
        public long? IpdBillingCounterId { get; set; }
        public long? IpdReceiptCounterId { get; set; }
        public long? IpdRefundOfBillCounterId { get; set; }
        public long? IpdRefundOfBillReceiptCounterId { get; set; }
        public long? IpdRefundOfAdvanceCounterId { get; set; }
        public long? IpdRefundOfAdvanceReceiptCounterId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? CityId { get; set; }
    }
}
