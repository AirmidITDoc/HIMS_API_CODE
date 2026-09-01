using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class DailyDashBoardIPSubModule
    {
        public List<TodayvsYesterdayModel> TodayvsYesterday { get; set; }

    }
    public class TodayvsYesterdayModel
    { 
        public long? TodaysAdmissions { get; set; }
        public long? CurrentOccupancy { get; set; }

    }
}
