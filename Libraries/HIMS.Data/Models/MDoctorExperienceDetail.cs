namespace HIMS.Data.Models
{
    public partial class MDoctorExperienceDetail
    {
        public long DocExpId { get; set; }
        public long? DoctorId { get; set; }
        public string? HospitalName { get; set; }
        public string? Designation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual DoctorMaster? Doctor { get; set; }
    }
}
