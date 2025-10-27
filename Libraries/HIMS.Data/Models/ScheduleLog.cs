namespace HIMS.Data.Models
{
    public partial class ScheduleLog
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public DateTime ExecuteOn { get; set; }
        public bool IsActive { get; set; }
        public string? Result { get; set; }
        public DateTime ExecuteDay { get; set; }
        public int ExecuteHour { get; set; }

        public virtual ScheduleMaster Schedule { get; set; } = null!;
    }
}
