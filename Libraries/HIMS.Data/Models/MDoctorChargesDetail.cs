namespace HIMS.Data.Models
{
    public partial class MDoctorChargesDetail
    {
        public long DocChargeId { get; set; }
        public long? DoctorId { get; set; }
        public long? ServiceId { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public decimal? Price { get; set; }
        public int? Days { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual DoctorMaster? Doctor { get; set; }
    }
}
