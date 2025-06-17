using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
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
            return await DatabaseHelper.GetGridDataBySp<PhoneAppointment2ListDto>(model, "ps_Rtrv_PhoneAppList");
        }
        public virtual async Task<IPagedList<FutureAppointmentDetailListDto>> GetListAsyncF(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<FutureAppointmentDetailListDto>(model, "ps_Rtrv_ScheduledPhoneAppList");
        }
        public virtual async Task<IPagedList<FutureAppointmentListDto>> FutureAppointmentList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<FutureAppointmentListDto>(model, "s_Rtrv_ScheduledPhoneApp");
        }
        public virtual async Task<IPagedList<TPhoneAppointment>> GetPhoneListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TPhoneAppointment>(model, "ps_Rtrv_PhoneAppointmentListSearch");
        }
        public virtual async Task<List<PhoneAutoCompleteDto>> SearchPhoneApp(string str)
        {
            return await this._context.TPhoneAppointments
                .Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(str)
                || x.RegNo.ToLower().Contains(str)
                || x.MobileNo.ToLower().Contains(str))
                .Where(x => x.PhAppDate == DateTime.Today) // Filter by today's date
                .Take(25)
                .Select(x => new PhoneAutoCompleteDto()
                {
                    FirstName = x.FirstName,
                    Id = x.PhoneAppId,
                    LastName = x.LastName,
                    Mobile = x.MobileNo,
                    RegNo = string.IsNullOrEmpty(x.RegNo) ? "0" : x.RegNo
                }).OrderBy(x => x.FirstName).ToListAsync();


            //return await (
            //    from app in this._context.TPhoneAppointments
            //    join reg in this._context.Registrations on Convert.ToInt32(app.RegNo) equals reg.RegId into regJoin
            //    from reg in regJoin.DefaultIfEmpty() // Left join
            //    where ((app.FirstName + " " + app.LastName).ToLower().Contains(str)
            //        || (reg.RegNo ?? "").ToLower().Contains(str)
            //        || app.MobileNo.ToLower().Contains(str))
            //        && app.PhAppDate == DateTime.Today
            //    orderby app.FirstName
            //    select new PhoneAutoCompleteDto
            //    {
            //        FirstName = app.FirstName,
            //        Id = app.PhoneAppId,
            //        LastName = app.LastName,
            //        Mobile = app.MobileNo,
            //        RegNo = string.IsNullOrEmpty(reg.RegNo) ? "0" : reg.RegNo,
            //        RegId = reg.RegId
            //    }
            //).Take(25).ToListAsync();

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
            string vPhoneAppId = odal.ExecuteNonQuery("ps_insert_T_PhoneAppointment_1", CommandType.StoredProcedure, "PhoneAppId", entity);
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
