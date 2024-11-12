using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
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

namespace HIMS.Services.OPPatient
{
    public class PhoneAppointment2Service : IPhoneAppointment2Service
    {
        private readonly HIMSDbContext _context;
        public PhoneAppointment2Service(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<PhoneAppointment2ListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PhoneAppointment2ListDto>(model, "m_rtrv_PhoneAppList");
        }
        public virtual async Task<TPhoneAppointment> InsertAsyncSP(TPhoneAppointment objTPhoneAppointment, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "SeqNo", "IsCancelled", "IsCancelledBy", "IsCancelledDate" };
            var entity = objTPhoneAppointment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vPhoneAppId = odal.ExecuteNonQuery("v_insert_T_PhoneAppointment_1", CommandType.StoredProcedure, "PhoneAppId", entity);
            objTPhoneAppointment.PhoneAppId = Convert.ToInt32(vPhoneAppId);

            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

            return objTPhoneAppointment;
        }
        public virtual async Task CancelAsync(TPhoneAppointment objTPhoneAppointment, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TPhoneAppointment objphoneApp = await _context.TPhoneAppointments.FindAsync(objTPhoneAppointment.PhoneAppId);
                objphoneApp.IsCancelled = objTPhoneAppointment.IsCancelled;
                objphoneApp.IsCancelledBy = objTPhoneAppointment.IsCancelledBy;
                objphoneApp.IsCancelledDate = objTPhoneAppointment.IsCancelledDate;
                _context.TPhoneAppointments.Update(objphoneApp);
                _context.Entry(objphoneApp).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
