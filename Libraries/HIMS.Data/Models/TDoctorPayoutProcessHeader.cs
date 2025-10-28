namespace HIMS.Data.Models
{
    public partial class TDoctorPayoutProcessHeader
    {
        public TDoctorPayoutProcessHeader()
        {
            TDoctorPayoutProcessDetails = new HashSet<TDoctorPayoutProcessDetail>();
        }

        public long DoctorPayoutId { get; set; }
        public long? DoctorId { get; set; }
        public DateTime? ProcessStartDate { get; set; }
        public DateTime? ProcessEndDate { get; set; }
        public DateTime? ProcessDate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? DoctorAmount { get; set; }
        public decimal? HospitalAmount { get; set; }
        public decimal? Tdsamount { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TDoctorPayoutProcessDetail> TDoctorPayoutProcessDetails { get; set; }
    }
}
