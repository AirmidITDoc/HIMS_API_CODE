using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DynamicExecuteSchedule
    {
        public long ScheduleId { get; set; }
        public string? ScheduleExecuteType { get; set; }
        public int? MonthDay { get; set; }
        public string? WeekDayName { get; set; }
        public DateTime? ExecuteTime { get; set; }
        public string? Query { get; set; }
        public bool? IsDelete { get; set; }
    }
}
