namespace HIMS.Data.Models
{
    public partial class TProcessOtp
    {
        public long MsgId { get; set; }
        public long? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? Message { get; set; }
        public string? Otpno { get; set; }
        public bool? IsVerified { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
