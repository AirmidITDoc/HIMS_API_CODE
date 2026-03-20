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
        public virtual async Task InsertAsync(TLabAppointment ObjTLabAppointment, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TLabAppointments
                    .OrderByDescending(x => x.SeqNo)
                    .Select(x => x.SeqNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTLabAppointment.SeqNo = newSeqNo.ToString();



                ObjTLabAppointment.CreatedBy = UserId;
                ObjTLabAppointment.CreatedDate = AppTime.Now;

                _context.TLabAppointments.Add(ObjTLabAppointment);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
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
        public virtual async Task<List<TLabAppointment>> GetLabAppoinments(int DocId, DateTime FromDate, DateTime ToDate,int?CategoryId)
        {
            return await this._context.TLabAppointments.Where(x => x.DoctorId == DocId && !x.IsCancelled.Value && x.AppDate >= FromDate && x.AppDate <= ToDate && (CategoryId == null || x.CategoryId == CategoryId) ).ToListAsync();
        }
       

    }
}
