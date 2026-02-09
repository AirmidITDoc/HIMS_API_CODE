using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HIMS.ScheduleJobs.Data
{
    public static class ConnectionStrings
    {
        public static string MainDbConnectionString { get; set; }

        public static void SetConnectionString(string connectionString)
        {
            if (MainDbConnectionString == null)
            {
                MainDbConnectionString = connectionString;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
