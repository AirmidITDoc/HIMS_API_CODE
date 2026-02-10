using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Data
{
    public static class Common
    {
        public static int ToInt(this object a)
        {
            try
            {
                return Convert.ToInt32(a);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static bool ToBool(this object a)
        {
            try
            {
                return Convert.ToBoolean(a);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string ConvertToString(this object a)
        {
            try
            {
                return Convert.ToString(a);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static TimeSpan ConvertToTime(this object a)
        {
            try
            {
                return TimeSpan.Parse(a.ToString());
            }
            catch (Exception)
            {
                return TimeSpan.MinValue;
            }
        }

        public static T GetItemNew<T>(this DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    object value = dr[column.ColumnName];
                    if (value != DBNull.Value)
                    {
                        if (string.Equals(pro.Name, column.ColumnName, StringComparison.OrdinalIgnoreCase))
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        //pro.SetValue(obj, dr[column.ColumnName] ?? "", null);  
                        else
                            continue;
                    }
                }
            }
            return obj;
        }


        public static void WriteToFile(string Message)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.Trim('\\') + "\\Logs";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
                if (!File.Exists(filepath))
                {
                    // Create a file to write to.   
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItemNew<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static void SendMail(string smtpServer, string smtpPort, string smtpUsername, string smtpPassword, bool smtpSsl, string fromEmail, string fromName, string toEmail, string ToDisplayName, string Body, string Subject, bool IsHtml = true, string? CC = null, string? BCC = null, List<AttachmentModel>? Attachments = null)
        {
            try
            {
                if (smtpUsername == null && smtpPassword == null)
                {
                    smtpUsername = "";
                    smtpPassword = "";
                }
                MailMessage message = new()
                {
                    From = new MailAddress(fromEmail, fromName)
                };
                foreach (string strEmail in toEmail.Split(','))
                {
                    message.To.Add(new MailAddress(strEmail, ToDisplayName));
                }
                if (!string.IsNullOrEmpty(CC))
                {
                    foreach (string strEmail in CC.Split(','))
                    {
                        message.CC.Add(new MailAddress(strEmail));
                    }
                }
                if (!string.IsNullOrEmpty(BCC))
                {
                    foreach (string strEmail in BCC.Split(','))
                    {
                        message.Bcc.Add(new MailAddress(strEmail));
                    }
                }

                message.IsBodyHtml = IsHtml;
                message.Subject = Subject;
                message.Body = Body;

                if (Attachments != null && Attachments.Count > 0)
                {
                    foreach (var attachmentItem in Attachments)
                    {
                        if (!string.IsNullOrEmpty(attachmentItem.AttachmentLink))
                        {
                            if (attachmentItem.AttachmentLink.StartsWith("https"))
                            {
                                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                            }

                            HttpClient webClient = new();
                            Uri webClientUri = new(attachmentItem.AttachmentLink);
                            webClient.BaseAddress = webClientUri;

                            webClient.Timeout = TimeSpan.FromMinutes(10);
                            webClient.DefaultRequestHeaders.Host = webClientUri.Host;

                            var webClientTask = webClient.GetStreamAsync(attachmentItem.AttachmentLink);
                            webClientTask.Wait();

                            if (webClientTask.IsCompleted)
                            {
                                Stream stream = webClientTask.Result;

                                using MemoryStream ms = new();
                                stream.CopyTo(ms);
                                ms.Seek(0, SeekOrigin.Begin);
                                byte[] bytes = ms.ToArray();
                                ms.Close();

                                message.Attachments.Add(new System.Net.Mail.Attachment(new MemoryStream(bytes), attachmentItem.AttachmentName));
                            }
                        }
                    }
                }

                SmtpClient smtpClient = new(smtpServer) { Port = int.Parse(smtpPort), EnableSsl = smtpSsl };

                if (smtpUsername != "" && smtpPassword != "")
                {
                    System.Net.NetworkCredential SMTPUserInfo = new(smtpUsername, smtpPassword);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = SMTPUserInfo;
                }
                else
                {
                    smtpClient.UseDefaultCredentials = true;
                    // smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                }
                smtpClient.Send(message);
                message.Dispose();
                smtpClient.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
