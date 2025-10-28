namespace HIMS.Data.Models
{
    public partial class TPatientDetail
    {
        public long PatientId { get; set; }
        public string? PatientName { get; set; }
        public long? MobileNo { get; set; }
        public string? Address { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }
}
