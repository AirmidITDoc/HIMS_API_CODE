using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using HIMS.Data.DataProviders;
using System.Data;
using HIMS.Services.Utilities;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;



namespace HIMS.Services.Pharmacy
{
    public  class PurchaseRequisitionService: IPurchaseRequisitionService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PurchaseRequisitionService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<PurchaseRequitionListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PurchaseRequitionListDto>(model, "ps_PurchaseRequisitionHeaderList");
        }
        public virtual async Task InsertAsync(TPurchaseRequisitionHeader ObjTPurchaseRequisitionHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TPurchaseRequisitionHeaders
                    .OrderByDescending(x => x.PurchaseRequisitionNo)
                    .Select(x => x.PurchaseRequisitionNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTPurchaseRequisitionHeader.PurchaseRequisitionNo = newSeqNo.ToString();


                ObjTPurchaseRequisitionHeader.CreatedBy = UserId;
                ObjTPurchaseRequisitionHeader.CreatedDate = AppTime.Now;

                _context.TPurchaseRequisitionHeaders.Add(ObjTPurchaseRequisitionHeader);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(TPurchaseRequisitionHeader ObjTPurchaseRequisitionHeader, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
             {
                long PurchaseRequisitionId = ObjTPurchaseRequisitionHeader.PurchaseRequisitionId;

                // ✅ Delete related details first
                var lstAttend = await _context.TPurchaseRequisitionDetails
                    .Where(x => x.PurchaseRequisitionId == PurchaseRequisitionId)
                    .ToListAsync();
                if (lstAttend.Any())
                    _context.TPurchaseRequisitionDetails.RemoveRange(lstAttend);

              
                // Save deletion first
                await _context.SaveChangesAsync();

                // Then attach and update header
                _context.Attach(ObjTPurchaseRequisitionHeader);
                _context.Entry(ObjTPurchaseRequisitionHeader).State = EntityState.Modified;

                _context.Entry(ObjTPurchaseRequisitionHeader).Property(x => x.CreatedBy).IsModified = false;
                _context.Entry(ObjTPurchaseRequisitionHeader).Property(x => x.CreatedDate).IsModified = false;
                _context.Entry(ObjTPurchaseRequisitionHeader).Property(x => x.PurchaseRequisitionNo).IsModified = false;

                ObjTPurchaseRequisitionHeader.ModifiedBy = UserId;
                ObjTPurchaseRequisitionHeader.ModifiedDate = AppTime.Now;

                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                        _context.Entry(ObjTPurchaseRequisitionHeader).Property(column).IsModified = false;
                }

                await _context.SaveChangesAsync();
                scope.Complete();
             }
        }
        public virtual async Task Cancel(TPurchaseRequisitionHeader ObjTPurchaseRequisitionHeader, int UserId, string Username)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] Entity = { "PurchaseRequisitionId", "IsCancelledBy" };
            var entity = ObjTPurchaseRequisitionHeader.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!Entity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_Cancel_PurchaseRequisition", CommandType.StoredProcedure, entity);
        }
        public virtual async Task VerifyAsync(TPurchaseRequisitionHeader ObjTPurchaseRequisitionHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TPurchaseRequisitionHeader objPur = await _context.TPurchaseRequisitionHeaders.FindAsync(ObjTPurchaseRequisitionHeader.PurchaseRequisitionId);
                objPur.Isverify = ObjTPurchaseRequisitionHeader.Isverify;
                objPur.IsInchargeVerifyId = ObjTPurchaseRequisitionHeader.IsInchargeVerifyId;
                objPur.IsInchargeVerifyDate = ObjTPurchaseRequisitionHeader.IsInchargeVerifyDate;
                _context.TPurchaseRequisitionHeaders.Update(objPur);
                _context.Entry(objPur).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

    }
}
