using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;



namespace HIMS.Services.Pharmacy
{
    public class PurchaseRequisitionFinalService : IPurchaseRequisitionFinalService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PurchaseRequisitionFinalService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(TPrheader ObjTPrheader, List<TPurchaseRequisitionHeader> objPurchaseRequisitionHeader, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);

            {
                var lastSeqNoStr = await _context.TPrheaders
                    .OrderByDescending(x => x.Prno)
                    .Select(x => x.Prno)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                // Increment the sequence number
                int newSeqNo = lastSeqNo + 1;
                ObjTPrheader.Prno = newSeqNo.ToString();
                ObjTPrheader.CreatedBy = UserId;
                ObjTPrheader.CreatedDate = AppTime.Now;

                _context.TPrheaders.Add(ObjTPrheader);
                await _context.SaveChangesAsync();

                if (objPurchaseRequisitionHeader != null && objPurchaseRequisitionHeader.Any())
                {
                    foreach (var tpr in objPurchaseRequisitionHeader)
                    {
                        if (tpr.PurchaseRequisitionId != 0)
                        {
                            DatabaseHelper odal = new();
                            var entity = new Dictionary<string, object> { { "PrrequestHeaderId", tpr.PurchaseRequisitionId } };
                            odal.ExecuteNonQuery("ps_UpdatePurchaseRequestStatus", CommandType.StoredProcedure, entity);

                        }
                    }
                }

                scope.Complete();
            }         
        }

        public virtual async Task<IPagedList<PurchaseRequisitionFinalHeaderListDto>> PurchaseRequisitionFinalHeaderListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PurchaseRequisitionFinalHeaderListDto>(model, "ps_PurchaseRequisitionFinalHeaderList");
        }

        public virtual async Task<IPagedList<PurchaseRequisitionFinalDetailListDto>> PurchaseRequisitionFinalDetailListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PurchaseRequisitionFinalDetailListDto>(model, "ps_PurchaseRequisitionFinalDetailList");
        }

        public virtual async Task PRToPOInsertAsync(TPurchaseHeader objPurchase, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Fetch Store Info
                MStoreMaster StoreInfo = await _context.MStoreMasters.FirstOrDefaultAsync(x => x.StoreId == objPurchase.StoreId);

                if (StoreInfo == null)
                {
                    throw new Exception($"StoreInfo is null. No store found with StoreId: {objPurchase.StoreId}");
                }

                // Ensure PurchaseNo is valid
                int purchaseNo = 0;
                if (!string.IsNullOrEmpty(StoreInfo.PurchaseNo))
                {
                    purchaseNo = Convert.ToInt32(StoreInfo.PurchaseNo);
                }
                StoreInfo.PurchaseNo = (purchaseNo + 1).ToString();

                // Update Store Record
                _context.MStoreMasters.Update(StoreInfo);
                await _context.SaveChangesAsync();

                // Add Purchase Header
                objPurchase.PurchaseNo = StoreInfo.PurchaseNo;
                _context.TPurchaseHeaders.Add(objPurchase);
                await _context.SaveChangesAsync();

                // Complete Transaction
                scope.Complete();

            }
        }
    }
}
