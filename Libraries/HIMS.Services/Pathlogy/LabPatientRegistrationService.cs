using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Pathlogy
{
    public class LabPatientRegistrationService : ILabPatientRegistrationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public LabPatientRegistrationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<LabPatientRegistrationListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabPatientRegistrationListDto>(model, "ps_LabPatientRegistrationList");
        }

        public virtual async Task InsertAsync(TLabPatientRegistration ObjTLabPatientRegistration, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Get last SequenceNo (string type)
                var lastSeqNoStr = await _context.TLabPatientRegistrations
                    .OrderByDescending(x => x.LabRequestNo)
                    .Select(x => x.LabRequestNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                ObjTLabPatientRegistration.LabRequestNo = (lastSeqNo + 1).ToString();
                _context.TLabPatientRegistrations.Add(ObjTLabPatientRegistration);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        //public virtual async Task InsertAsync(TOtRequestHeader ObjTOtRequestHeader, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        _context.TOtRequestHeaders.Add(ObjTOtRequestHeader);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}

        public virtual async Task UpdateAsync(TLabPatientRegistration ObjTLabPatientRegistration, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // 1. Attach the entity without marking everything as modified
                _context.Attach(ObjTLabPatientRegistration);
                _context.Entry(ObjTLabPatientRegistration).State = EntityState.Modified;
                // Always ignore LabRequestNo (auto-increment column)
                _context.Entry(ObjTLabPatientRegistration).Property(x => x.LabRequestNo).IsModified = false;

                // 2. Ignore specific columns
                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                    {
                        _context.Entry(ObjTLabPatientRegistration).Property(column).IsModified = false;
                    }
                }
                //Delete details table realted records
                var lst = await _context.TLabTestRequests.Where(x => x.LabPatientId == ObjTLabPatientRegistration.LabPatientId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.TLabTestRequests.RemoveRange(lst);
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
      

        //public virtual async Task UpdateAsync(TOtRequestHeader ObjTOtRequestHeader, int UserId, string Username, string[]? ignoreColumns = null)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // 1. Attach the entity without marking everything as modified
        //        _context.Attach(ObjTOtRequestHeader);
        //        _context.Entry(ObjTOtRequestHeader).State = EntityState.Modified;

        //        // 2. Ignore specific columns
        //        if (ignoreColumns?.Length > 0)
        //        {
        //            foreach (var column in ignoreColumns)
        //            {
        //                _context.Entry(ObjTOtRequestHeader).Property(column).IsModified = false;
        //            }
        //        }
        //        //Delete details table realted records
        //        var lst = await _context.TOtRequestSurgeryDetails.Where(x => x.OtrequestId == ObjTOtRequestHeader.OtrequestId).ToListAsync();
        //        if (lst.Count > 0)
        //        {
        //            _context.TOtRequestSurgeryDetails.RemoveRange(lst);
        //        }

        //        await _context.SaveChangesAsync();
        //        //Delete details table realted records
        //        var lsts = await _context.TOtRequestAttendingDetails.Where(x => x.OtrequestId == ObjTOtRequestHeader.OtrequestId).ToListAsync();
        //        if (lst.Count > 0)
        //        {
        //            _context.TOtRequestSurgeryDetails.RemoveRange(lst);
        //        }

        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}

    }
}
