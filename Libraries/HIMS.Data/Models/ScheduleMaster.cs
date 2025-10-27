namespace HIMS.Data.Models
{
    public partial class ScheduleMaster
    {
        public ScheduleMaster()
        {
            ScheduleLogs = new HashSet<ScheduleLog>();
        }

        public int Id { get; set; }
        public string ScheduleName { get; set; } = null!;
        public int ScheduleType { get; set; }
        public string Hours { get; set; } = null!;
        public string? Days { get; set; }
        public string? Dates { get; set; }
        public string? Custom { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Query { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<ScheduleLog> ScheduleLogs { get; set; }
    }
}
