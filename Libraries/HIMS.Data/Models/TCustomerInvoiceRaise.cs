namespace HIMS.Data.Models
{
    public partial class TCustomerInvoiceRaise
    {
        public long InvoiceRaiseId { get; set; }
        public string? InvNumber { get; set; }
        public DateTime? InvDate { get; set; }
        public DateTime? InvTime { get; set; }
        public long? CustomerId { get; set; }
        public decimal? Amount { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
