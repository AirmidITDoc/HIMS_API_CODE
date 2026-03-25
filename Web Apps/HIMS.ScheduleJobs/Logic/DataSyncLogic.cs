using HIMS.ScheduleJobs.Data;
using System;
using System.Collections.Generic;
using System.Data;
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
                Console.WriteLine("DataSync started...");

                 await DatabaseHelper.IUD("EXEC ps_GET_DataSync", Array.Empty<SqlParameter>());

                Console.WriteLine("DataSync executed successfully.");
               // Console.WriteLine("Result: " + result); // optional, depends on what IUD returns
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while executing DataSync:");
                Console.WriteLine(ex.Message);

                // Optional: print full stack trace
                Console.WriteLine(ex.ToString());

                Common.WriteToFile("Error on " + DateTime.Now.ToString("dd/MM/yyyy") + " => " + ex.Message);
            }
        }

        //public static async Task RunDataSync()
        //{
        //    string connectionString = "server=192.168.2.200;database=ADDANNEX_AIRMID_SERVER;uid=DEV001;password=DEV001;MultipleActiveResultSets=True;";

        //    try
        //    {
        //        Console.WriteLine("DataSync started...");

        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            await conn.OpenAsync();

        //            using (SqlCommand cmd = new SqlCommand("ps_GET_DataSync", conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                // If your SP has parameters, add here
        //                // cmd.Parameters.AddWithValue("@Param1", value);

        //                int rowsAffected = await cmd.ExecuteNonQueryAsync();

        //                Console.WriteLine("DataSync executed successfully.");
        //                Console.WriteLine("Rows affected: " + rowsAffected);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error occurred while executing DataSync:");
        //        Console.WriteLine(ex.Message);
        //        Console.WriteLine(ex.ToString());

        //        Common.WriteToFile("Error on " + DateTime.Now.ToString("dd/MM/yyyy") + " => " + ex.Message);
        //    }
        //}
    }
}
