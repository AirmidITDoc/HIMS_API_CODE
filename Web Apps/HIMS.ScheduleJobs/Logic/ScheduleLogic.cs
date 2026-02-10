using HIMS.ScheduleJobs.Data;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Logic
{
    public class ScheduleLogic
    {
        public ScheduleLogic() { }
        public static async Task ExecuteScheduler()
        {
            try
            {
                await DatabaseHelper.FetchDataTableAsync("Execute_Scheduler", Array.Empty<SqlParameter>());
            }
            catch (Exception ex)
            {
                Common.WriteToFile("Error at ExecuteScheduler on " + DateTime.Now.ToString("dd/MM/yyyy") + "=>" + ex.Message);
            }
        }
    }
}
