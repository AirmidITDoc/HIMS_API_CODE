using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.Pathology;


namespace HIMS.Services.Pathlogy
{
    public  class LabAppointmentService: ILabAppointmentService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public LabAppointmentService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<LabAppointmentListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabAppointmentListDto>(model, "ps_LabAppointmentList");
        }
        public virtual async Task<IPagedList<LabAppDetListDto>> LabGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabAppDetListDto>(model, "ps_Rtrv_LabAppDetList");
        }


        public virtual async Task InsertAsync(TLabAppointment ObjTLabAppointment, int UserId, string Username)

        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }, TransactionScopeAsyncFlowOption.Enabled);

            var presNoList = await _context.TLabAppointments
                .Where(x => x.SeqNo != null && x.SeqNo != "")
                .Select(x => x.SeqNo)
                .ToListAsync();

            int lastPresNo = presNoList
                .Select(p => int.TryParse(p, out var n) ? n : 0)
                .DefaultIfEmpty(0)
                .Max();

            //  Increment & assign
            ObjTLabAppointment.SeqNo = (lastPresNo + 1).ToString();

            ObjTLabAppointment.CreatedBy = UserId;
            ObjTLabAppointment.CreatedDate = AppTime.Now;

            _context.TLabAppointments.Add(ObjTLabAppointment);
            await _context.SaveChangesAsync();

            scope.Complete();
        }


        public virtual async Task UpdateAsync(TLabAppointment ObjTLabAppointment, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // 1. Attach the entity without marking everything as modified
                _context.Attach(ObjTLabAppointment);
                _context.Entry(ObjTLabAppointment).State = EntityState.Modified;
                // Always ignore LabRequestNo (auto-increment column)
                _context.Entry(ObjTLabAppointment).Property(x => x.SeqNo).IsModified = false;

                // 2. Ignore specific columns
                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                    {
                        _context.Entry(ObjTLabAppointment).Property(column).IsModified = false;
                    }
                }
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
        public virtual async Task<List<TLabAppointment>> GetLabAppoinments(int DocId, DateTime FromDate, DateTime ToDate,int CategoryId)
        {
            return await this._context.TLabAppointments.Where(x => (DocId == 0 || x.DoctorId == DocId)  && !x.IsCancelled.Value && x.AppDate >= FromDate && x.AppDate <= ToDate && (CategoryId == 0 || x.CategoryId == CategoryId) ).ToListAsync();
        }
        //public virtual async Task<List<LabAppointmentDto>> SearchLabApp(string str)
        //{
        //    var qry = from d in this._context.TLabAppointments
        //              join v in this._context.VisitDetails on d.LabAppId equals v.LabAppId into visit
        //              from v in visit.DefaultIfEmpty()
        //              where ((d.FirstName + " " + d.LastName).ToLower().Contains(str)
        //                    || d.RegNo.ToLower().Contains(str)
        //                    || d.MobileNo.ToLower().Contains(str))
        //                    && d.PhAppDate == DateTime.Today
        //              orderby d.FirstName
        //              select new LabAppointmentDto()
        //              {
        //                  FirstName = d.FirstName,
        //                  LabAppId = v.LabAppId.HasValue ? v.PhoneAppId.Value : 0,
        //                  Id = d.PhoneAppId,
        //                  LastName = d.LastName,
        //                  Mobile = d.MobileNo,
        //                  RegNo = string.IsNullOrEmpty(d.RegNo) ? "0" : d.RegNo,
        //                  DepartmentId = d.DepartmentId,
        //                  DoctorId = d.DoctorId
        //              };
        //    return await qry.Take(25).ToListAsync();

        //}
        public virtual async Task CancelAsync(TLabAppointment ObjTLabAppointments, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TLabAppointment objLabApp = await _context.TLabAppointments.FindAsync(ObjTLabAppointments.LabAppId);
                objLabApp.IsCancelled = ObjTLabAppointments.IsCancelled;
                objLabApp.IsCancelledBy = ObjTLabAppointments.IsCancelledBy;
                objLabApp.IsCancelledDate = ObjTLabAppointments.IsCancelledDate;
                _context.TLabAppointments.Update(objLabApp);
                _context.Entry(objLabApp).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}
