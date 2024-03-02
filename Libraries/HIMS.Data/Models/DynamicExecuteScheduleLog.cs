using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DynamicExecuteScheduleLog
    {
        public long SrNo { get; set; }
        public long? ScheduleId { get; set; }
        public byte? IsExecute { get; set; }
        public DateTime? ExecuteDateTime { get; set; }
    }
}
