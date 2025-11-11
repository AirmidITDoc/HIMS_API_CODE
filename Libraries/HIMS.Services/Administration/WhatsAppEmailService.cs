using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using System.Data;

namespace HIMS.Services.Administration
{
    public class WhatsAppEmailService : IWhatsAppEmailService
    {
        private readonly HIMSDbContext _context;
        public WhatsAppEmailService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(TWhatsAppSmsOutgoing ObjWhatsApp, int UserId, string Username)
        {
            try
            {
                DatabaseHelper odal = new();
                string[] rEntity = { "MobileNumber", "smsString", "isSent", "smsType", "smsFlag", "smsDate", "tranNo", "patientType", "templateId", "smSurl", "filePath", "smsOutGoingID" };

                var lentity = ObjWhatsApp.ToDictionary();
                foreach (var rProperty in lentity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        lentity.Remove(rProperty);
                }
                string vSmsoutGoingId = odal.ExecuteNonQuery("ps_insert_WhatsAppSMS", CommandType.StoredProcedure, "smsOutGoingID", lentity);
                ObjWhatsApp.SmsoutGoingId = Convert.ToInt32(vSmsoutGoingId);

            }
            catch (Exception ex)
            {
                TWhatsAppSmsOutgoing? objWhatsAppSMS = await _context.TWhatsAppSmsOutgoings.FindAsync(ObjWhatsApp.SmsoutGoingId);
                _context.TWhatsAppSmsOutgoings.Remove(objWhatsAppSMS);
                await _context.SaveChangesAsync();
            }
        }
    }
}
