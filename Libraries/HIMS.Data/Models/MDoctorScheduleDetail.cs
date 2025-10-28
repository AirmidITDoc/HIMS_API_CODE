namespace HIMS.Data.Models
{
    public partial class MDoctorScheduleDetail
    {
        public long DocSchedId { get; set; }
        public long? DoctorId { get; set; }
        public string? ScheduleDays { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? Slot { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual DoctorMaster? Doctor { get; set; }
    }
}
