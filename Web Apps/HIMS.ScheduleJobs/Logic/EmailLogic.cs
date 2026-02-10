using HIMS.ScheduleJobs.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Logic
{
    public class EmailLogic
    {
        public EmailLogic() { }
        public static async Task SendEmail()
        {
            string smtp_server = "", smtp_port = "", smtp_username = "", smtp_password = "", smtp_ssl = "";
            bool isssl = true;
            DataTable dt = await DatabaseHelper.FetchDataTableAsync("GET_EMAIL_DATA", Array.Empty<SqlParameter>());
            if (dt.Rows.Count > 0)
            {
                DataTable dtEmailConfig = await DatabaseHelper.FetchDataTableAsync("SELECT TOP 1 * FROM Email_Configuration ORDER BY 1 desc", new SqlParameter[0], CommandType.Text);
                if (dtEmailConfig.Rows.Count > 0)
                {
                    smtp_server = dtEmailConfig.Rows[0]["MailServer_SMTP"].ConvertToString().Trim();
                    smtp_port = dtEmailConfig.Rows[0]["SMTP_Port"].ConvertToString().Trim();
                    smtp_username = dtEmailConfig.Rows[0]["User_Name"].ConvertToString().Trim();
                    smtp_password = dtEmailConfig.Rows[0]["Password"].ConvertToString().Trim();
                    isssl = dtEmailConfig.Rows[0]["SMTP_SSL"].ToBool();
                }
            }
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    string result = "";
                    int Status = 0;
                    try
                    {
                        List<AttachmentModel> attachmentList = new();

                        if (!string.IsNullOrWhiteSpace(row["AttachmentName"].ConvertToString()))
                        {
                            string[] attachmentNames = row["AttachmentName"].ConvertToString().Split(",");
                            string[] attachmentLinks = row["AttachmentLink"].ConvertToString().Split(",");

                            if (attachmentNames != null && attachmentLinks != null)
                            {
                                if (attachmentNames.Length == attachmentLinks.Length)
                                {
                                    for (int i = 0; i < attachmentNames.Length; i++)
                                    {
                                        if (!string.IsNullOrEmpty(attachmentNames[i]) && !string.IsNullOrEmpty(attachmentLinks[i]))
                                        {
                                            AttachmentModel objNewAttachment = new()
                                            {
                                                AttachmentName = attachmentNames[i].Trim(),
                                                AttachmentLink = attachmentLinks[i].Trim()
                                            };

                                            attachmentList.Add(objNewAttachment);
                                        }
                                    }
                                }
                            }
                        }
                        string Subject = row["MailSubject"].ConvertToString();
                        if (attachmentList != null && attachmentList.Count > 0)
                        {
                            Common.SendMail(smtp_server, smtp_port, smtp_username, smtp_password, isssl, row["FromEmail"].ConvertToString(), row["FromName"].ConvertToString(), row["ToEmail"].ConvertToString(), row["FromName"].ConvertToString(), row["MailBody"].ConvertToString(), row["MailSubject"].ConvertToString(), true, row["CC"].ConvertToString(), row["BCC"].ConvertToString(), attachmentList);
                        }
                        else
                        {
                            Common.SendMail(smtp_server, smtp_port, smtp_username, smtp_password, isssl, row["FromEmail"].ConvertToString(), row["FromName"].ConvertToString(), row["ToEmail"].ConvertToString(), row["FromName"].ConvertToString(), row["MailBody"].ConvertToString(), row["MailSubject"].ConvertToString(), true, row["CC"].ConvertToString(), row["BCC"].ConvertToString());
                        }
                        result = "Successfully sent mail.";
                        Status = 1;
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                        Status = -2;
                    }
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@LastResponse", result);
                    parameters[1] = new SqlParameter("@Id", row["Id"].ToInt());
                    parameters[2] = new SqlParameter("@Status", Status);
                    await DatabaseHelper.IUD("UPDATE T_Mail_Outgoing SET Status=@Status,LastSendingTry=GETDATE(),LastResponse=@LastResponse WHERE Id=@Id", parameters);
                }
                catch (Exception ex)
                {
                    Common.WriteToFile("Error on " + DateTime.Now.ToString("dd/MM/yyyy") + "=>" + ex.Message);
                }
            }
        }
    }
}
