using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;
using System.Transactions;
using Microsoft.EntityFrameworkCore;


namespace HIMS.Services.Transaction
{
    public class smsConfigService : IsmsConfigService
    {
        private readonly HIMSDbContext _context;
        public smsConfigService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        public virtual async Task<IPagedList<EmailSendoutListDto>> GetEmailSconfig(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<EmailSendoutListDto>(model, "ps_Rtrv_T_EmailOutgoinglist");
        }

        public virtual async Task<IPagedList<SMSConfigListDto>> GetSMSconfig(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SMSConfigListDto>(model, "Rtrv_Sent_SMS_List");
        }

        public virtual async Task<IPagedList<WhatsAppsendOutListDto>> GetWhatsAppconfig(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<WhatsAppsendOutListDto>(model, "ps_Rtrv_T_Whatsappoutlist");
        }

        public virtual async Task InsertAsyncSP(SsSmsConfig objSsSmsConfig, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "Url", "Keys", "Campaign", "Routeid", "SenderId", "UserName", "Spassword", "StorageLocLink", "ConType" };
            var entity = objSsSmsConfig.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("PS_M_insert_SMS_Config", CommandType.StoredProcedure, entity);

            await _context.SaveChangesAsync(UserId, Username);
        }

        public virtual async Task UpdateAsyncSP(SsSmsConfig objSsSmsConfig, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "Url", "Keys", "Campaign", "Routeid", "SenderId", "UserName", "Spassword", "StorageLocLink", "ConType" };
            var entity = objSsSmsConfig.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("PS_m_Update_SMS_Config", CommandType.StoredProcedure, entity);

            await _context.SaveChangesAsync(UserId, Username);
        }

        public virtual async Task UpdateAsync(EmailConfiguration obj, int userId, string username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            var existing = await _context.EmailConfigurations.FirstOrDefaultAsync(x => x.Id == obj.Id);

            if (existing == null)
                throw new Exception("Record not found!");

            if (!string.IsNullOrWhiteSpace(obj.DisplayName) && obj.DisplayName != "string")
                existing.DisplayName = obj.DisplayName;

            if (!string.IsNullOrWhiteSpace(obj.EmailAddress) && obj.EmailAddress != "string")
                existing.EmailAddress = obj.EmailAddress;

            if (!string.IsNullOrWhiteSpace(obj.MailServerSmtp) && obj.MailServerSmtp != "string")
                existing.MailServerSmtp = obj.MailServerSmtp;

            if (obj.SmtpPort > 0)
                existing.SmtpPort = obj.SmtpPort;

            if (obj.ServerTimeout > 0)
                existing.ServerTimeout = obj.ServerTimeout;

            existing.SmtpRequiredAuthentication = obj.SmtpRequiredAuthentication;

            existing.RequiredSquiredPasswordAuthentication = obj.RequiredSquiredPasswordAuthentication;

            if (!string.IsNullOrWhiteSpace(obj.UserName) && obj.UserName != "string")
                existing.UserName = obj.UserName;

            if (!string.IsNullOrWhiteSpace(obj.Password) && obj.Password != "string")
                existing.Password = obj.Password;

            existing.IsActive = obj.IsActive;

            existing.SmtpSsl = obj.SmtpSsl;

            await _context.SaveChangesAsync();
            scope.Complete();
        }
        public virtual async Task InsertAsync(SmspdfConfig ObjSmspdfConfig, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.SmspdfConfigs.Add(ObjSmspdfConfig);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(SmspdfConfig ObjSmspdfConfig, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
             // Attach entity
            _context.Attach(ObjSmspdfConfig);
            var entry = _context.Entry(ObjSmspdfConfig);

            // Mark entity as modified
            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            scope.Complete();
        }

    }

}
