using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.OutPatient
{
    public  class PhoneAppService: IPhoneAppService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PhoneAppService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<TPhoneAppointment> InsertAsyncSP(TPhoneAppointment objPhoneAppointment, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = {"SeqNo","IsCancelled","IsCancelledBy","IsCancelledDate","AddedByDate","UpdatedByDate","CreatedBy","CreatedDate", "ModifiedBy","ModifiedDate"};
            var entity = objPhoneAppointment.ToDictionary(); 
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string PhoneAppId = odal.ExecuteNonQuery("insert_T_PhoneAppointment_1", CommandType.StoredProcedure, "PhoneAppId", entity);
            objPhoneAppointment.PhoneAppId = Convert.ToInt32(PhoneAppId);


            await _context.SaveChangesAsync(UserId, Username);

            return objPhoneAppointment;
        }
        public virtual async Task InsertAsync(TPhoneAppointment objPhoneAppointment, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TPhoneAppointments.Add(objPhoneAppointment);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TPhoneAppointment objPhoneAppointment, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TPhoneAppointments.Update(objPhoneAppointment);
                _context.Entry(objPhoneAppointment).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task CancelAsync(TPhoneAppointment objPhoneAppointment, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TPhoneAppointment objphone = await _context.TPhoneAppointments.FindAsync(objPhoneAppointment.PhoneAppId);
                //objphone.IsActive = false;
                objphone.CreatedDate = objPhoneAppointment.CreatedDate;
                objphone.CreatedBy = objPhoneAppointment.CreatedBy;
                _context.TPhoneAppointments.Update(objphone);
                _context.Entry(objphone).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}
