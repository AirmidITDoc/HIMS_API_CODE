using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OTManagement;


namespace HIMS.Services
{
    public  class OTInOperationService : IOTInOperationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OTInOperationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<OtRequestListDto>> GetListAsyncot(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<OtRequestListDto>(model, "ps_Rtrv_OT_RequestList");
        }
        public virtual async Task<IPagedList<InOperationAttendingDetailsListDto>> InOperationAttengingDetailsAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<InOperationAttendingDetailsListDto>(model, "rtrv_InOperationSurgeryDetailsList");
        }
        public virtual async Task<IPagedList<InOperationSurgeryDetailsDto>> InOperationSurgeryDetailsAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<InOperationSurgeryDetailsDto>(model, "rtrv_InOperationSurgeryDetailsList");
        }
        public virtual async Task InsertAsync(TOtInOperationHeader ObjTOtInOperationHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            {
                var lastSeqNoStr = await _context.TOtInOperationHeaders
                    .OrderByDescending(x => x.OtinOperationNo)
                    .Select(x => x.OtinOperationNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTOtInOperationHeader.OtinOperationNo = newSeqNo.ToString();


                ObjTOtInOperationHeader.CreatedBy = UserId;
                ObjTOtInOperationHeader.CreatedDate = DateTime.Now;

                _context.TOtInOperationHeaders.Add(ObjTOtInOperationHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TOtInOperationHeader ObjTOtInOperationHeader, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);


            {
                long OtinOperationId = ObjTOtInOperationHeader.OtinOperationId;

                // ✅ Delete related details first
                var lstAttend = await _context.TOtInOperationAttendingDetails
                    .Where(x => x.OtinOperationId == OtinOperationId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.TOtInOperationAttendingDetails.RemoveRange(lstAttend);

                var lstSurgery = await _context.TOtInOperationDiagnoses
                    .Where(x => x.OtinOperationId == OtinOperationId)
                    .ToListAsync();
                if (lstSurgery.Any())
                    _context.TOtInOperationDiagnoses.RemoveRange(lstSurgery);

                var lstDiagnosis = await _context.TOtInOperationPostOperDiagnoses
                    .Where(x => x.OtinOperationId == OtinOperationId)
                    .ToListAsync();
                if (lstDiagnosis.Any())
                    _context.TOtInOperationPostOperDiagnoses.RemoveRange(lstDiagnosis);

                var lstDiagnosise = await _context.TOtInOperationSurgeryDetails
                  .Where(x => x.OtinOperationId == OtinOperationId)
                  .ToListAsync();
                if (lstDiagnosis.Any())
                    _context.TOtInOperationSurgeryDetails.RemoveRange(lstDiagnosise);

                // ✅ Save deletion first
                await _context.SaveChangesAsync();

                // ✅ Then attach and update header
                _context.Attach(ObjTOtInOperationHeader);
                _context.Entry(ObjTOtInOperationHeader).State = EntityState.Modified;

                _context.Entry(ObjTOtInOperationHeader).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjTOtInOperationHeader).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(ObjTOtInOperationHeader).Property(x => x.OtinOperationNo).IsModified = false;

                ObjTOtInOperationHeader.ModifiedBy = UserId;
                ObjTOtInOperationHeader.ModifiedDate = DateTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjTOtInOperationHeader).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }

    }
}
