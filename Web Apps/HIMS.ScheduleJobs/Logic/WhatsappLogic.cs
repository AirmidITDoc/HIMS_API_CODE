using HIMS.ScheduleJobs.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Logic
{
    public class WhatsappLogic
    {
        public WhatsappLogic() { }
        public static async Task SendWhatsAppSms()
        {
            DataTable dt = await DatabaseHelper.FetchDataTableAsync("GET_WhatsAppSMS_DATA", Array.Empty<SqlParameter>());
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    string result = "";
                    int Status = 0;
                    try
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Get, row["SMSUrl"].ToString());
                        var response = await client.SendAsync(request);
                        result = await response.Content.ReadAsStringAsync();
                        result = result.Trim();
                        Status = 1;
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                        Status = -2;
                    }
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@LastResponse", result);
                    parameters[1] = new SqlParameter("@Id", row["SMSOutGoingId"].ToInt());
                    parameters[2] = new SqlParameter("@Status", Status);
                    await DatabaseHelper.IUD("UPDATE T_WhatsAppSMS_Outgoing SET Status=@Status,LastTry=GETDATE(),LastResponse=@LastResponse WHERE SmsOutGoingId=@Id", parameters);
                }
                catch (Exception ex)
                {
                    Common.WriteToFile("Error on " + DateTime.Now.ToString("dd/MM/yyyy") + "=>" + ex.Message);
                }
            }
        }
    }
}
