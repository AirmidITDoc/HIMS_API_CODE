using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
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
            var qry = from d in this._context.TPhoneAppointments
                      join v in this._context.VisitDetails on d.PhoneAppId equals v.PhoneAppId into visit
                      from v in visit.DefaultIfEmpty()
                      where ((d.FirstName + " " + d.LastName).ToLower().Contains(str)
                            || d.RegNo.ToLower().Contains(str)
                            || d.MobileNo.ToLower().Contains(str))
                            && d.PhAppDate == DateTime.Today
                      orderby d.FirstName
                      select new PhoneAutoCompleteDto()
                      {
                          FirstName = d.FirstName,
                          AppId = v.PhoneAppId.HasValue ? v.PhoneAppId.Value : 0,
                          Id = d.PhoneAppId,
                          LastName = d.LastName,
                          Mobile = d.MobileNo,
                          RegNo = string.IsNullOrEmpty(d.RegNo) ? "0" : d.RegNo,
                          DepartmentId = d.DepartmentId,
                          DoctorId = d.DoctorId
                      };
            return await qry.Take(25).ToListAsync();

        }

        public virtual async Task InsertAsync(TPhoneAppointment objTPhoneAppointment, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Add Purchase Header
                objTPhoneAppointment.SeqNo = "0";
                _context.TPhoneAppointments.Add(objTPhoneAppointment);
                await _context.SaveChangesAsync();

                // Complete Transaction
                scope.Complete();

            }
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
        public virtual async Task<List<TPhoneAppointment>> GetAppoinments(int DocId, DateTime FromDate, DateTime ToDate)
        {
            return await this._context.TPhoneAppointments.Where(x => x.DoctorId == DocId && !x.IsCancelled.Value && x.PhAppDate >= FromDate && x.PhAppDate <= ToDate).ToListAsync();
        }

        public virtual async Task UpdateAsync(TPhoneAppointment objTPhoneApp, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            var existing = await _context.TPhoneAppointments.FirstOrDefaultAsync(x => x.PhoneAppId == objTPhoneApp.PhoneAppId);

            //  Update only the required fields
            existing.PhAppDate = objTPhoneApp.PhAppDate;
            existing.PhAppTime = objTPhoneApp.PhAppTime;
            await _context.SaveChangesAsync();
            scope.Complete();
        }
    }
}

