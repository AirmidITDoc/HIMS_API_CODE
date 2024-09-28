using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.OPPatient
{
    public class PhoneAppointmentService : IPhoneAppointmentService
    {
        
            private readonly Data.Models.HIMSDbContext _context;
            public PhoneAppointmentService(HIMSDbContext HIMSDbContext)
            {
                _context = HIMSDbContext;
            }

            public virtual async Task<TPhoneAppointment> InsertAsyncSP(TPhoneAppointment objPhoneAppointment, int UserId, string Username)
            {
                DatabaseHelper odal = new();
                string[] rEntity = { "SeqNo", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AddedByDate", "UpdatedByDate", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var entity = objPhoneAppointment.ToDictionary(); 
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string PhoneAppId = odal.ExecuteNonQuery("v_insert_T_PhoneAppointment_1", CommandType.StoredProcedure, "PhoneAppId", entity);
                objPhoneAppointment.PhoneAppId = Convert.ToInt32(PhoneAppId);


                await _context.SaveChangesAsync(UserId, Username);

                return objPhoneAppointment;
            }


        public virtual async Task CancelAsync(TPhoneAppointment objPhoneAppointment, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TPhoneAppointment objphone = await _context.TPhoneAppointments.FindAsync(objPhoneAppointment.PhoneAppId);
                objphone.IsCancelled = objPhoneAppointment.IsCancelled;
                objphone.IsCancelledBy = objPhoneAppointment.IsCancelledBy;
                objphone.IsCancelledDate = objPhoneAppointment.IsCancelledDate;
                _context.TPhoneAppointments.Update(objphone);
                _context.Entry(objphone).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


    }
}

