namespace HIMS.Data.Models
{
    public partial class TCustomerInformation
    {
        public long CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerMobileNo { get; set; }
        public string? CustomerPinCode { get; set; }
        public string? ContactPersonName { get; set; }
        public string? ContactPersonMobileNo { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime? Amcdate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
