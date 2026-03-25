using HIMS.ScheduleJobs.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Logic
{
    public class DataSyncLogic
    {
        public DataSyncLogic() { }

        public static async Task RunDataSync()
        {
            try
            {
                await DatabaseHelper.IUD("EXEC ps_GET_DataSync", Array.Empty<SqlParameter>());
            }
            catch (Exception ex)
            {
                Common.WriteToFile("Error on " + DateTime.Now.ToString("dd/MM/yyyy") + "=>" + ex.Message);
            }
        }
    }
}
