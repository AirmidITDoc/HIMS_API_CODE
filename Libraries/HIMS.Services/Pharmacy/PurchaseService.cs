using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.GRN;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.Pharmacy
{
    public class PurchaseService : IPurchaseService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public PurchaseService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(TPurchaseHeader objPurchase, int UserId, string Username)
        {
            // Add header table records
            DatabaseHelper odal = new();
            SqlParameter[] para = new SqlParameter[27];
            para[0] = new SqlParameter("@PurchaseDate", objPurchase.PurchaseDate);
            para[1] = new SqlParameter("@PurchaseTime", objPurchase.PurchaseTime);
            para[2] = new SqlParameter("@StoreId", objPurchase.StoreId);
            para[3] = new SqlParameter("@SupplierID", objPurchase.SupplierId);
            para[4] = new SqlParameter("@TotalAmount", objPurchase.TotalAmount);
            para[5] = new SqlParameter("@DiscAmount", objPurchase.DiscAmount);
            para[6] = new SqlParameter("@TaxAmount", objPurchase.TaxAmount);
            para[7] = new SqlParameter("@FreightAmount", objPurchase.FreightAmount);
            para[8] = new SqlParameter("@OctriAmount", objPurchase.OctriAmount);
            para[9] = new SqlParameter("@GrandTotal", objPurchase.GrandTotal);
            para[10] = new SqlParameter("@Isclosed", objPurchase.Isclosed);

            para[11] = new SqlParameter("@IsVerified", objPurchase.IsVerified);
            para[12] = new SqlParameter("@Remarks", objPurchase.Remarks);
            para[13] = new SqlParameter("@TaxID", objPurchase.TaxId);
            para[14] = new SqlParameter("@AddedBy", objPurchase.AddedBy ?? 0);
            para[15] = new SqlParameter("@UpdatedBy", objPurchase.UpdatedBy ?? 0);
            para[16] = new SqlParameter("@PaymentTermId", objPurchase.PaymentTermId);
            para[17] = new SqlParameter("@ModeofPayment", objPurchase.ModeOfPayment);
            para[18] = new SqlParameter("@Worrenty", objPurchase.Worrenty);
            para[19] = new SqlParameter("@RoundVal", objPurchase.RoundVal);
            para[20] = new SqlParameter("@TotCGSTAmt", objPurchase.TotCgstamt);

            para[21] = new SqlParameter("@TotSGSTAmt", objPurchase.TotSgstamt);
            para[22] = new SqlParameter("@TotIGSTAmt", objPurchase.TotIgstamt);
            para[23] = new SqlParameter("@TransportChanges", objPurchase.TransportChanges);
            para[24] = new SqlParameter("@HandlingCharges", objPurchase.HandlingCharges);
            para[25] = new SqlParameter("@FreightCharges", objPurchase.FreightCharges);
            para[26] = new SqlParameter("@PurchaseId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };

            //SqlParameter retval = new SqlParameter("@PurchaseId", System.Data.SqlDbType.BigInt);
            //retval.Direction = System.Data.ParameterDirection.Output;
            //para[26] = retval;

            string purchaseNo = odal.ExecuteNonQuery("m_insert_PurchaseHeader_WithPurNo_1", CommandType.StoredProcedure, "@PurchaseId", para);
            objPurchase.PurchaseId = Convert.ToInt32(purchaseNo);

            // Add details table records
            foreach (var objItem in objPurchase.TPurchaseDetails)
            {
                objItem.PurchaseId = objPurchase.PurchaseId;
            }
            _context.TPurchaseDetails.AddRange(objPurchase.TPurchaseDetails);
            await _context.SaveChangesAsync(UserId, Username);
        }


        public virtual async Task InsertAsync(TPurchaseHeader objPurchase, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted },TransactionScopeAsyncFlowOption.Enabled);
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

        //public virtual async Task InsertAsync(TPurchaseHeader objPurchase, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Update store table records
        //        MStoreMaster StoreInfo = await _context.MStoreMasters.FirstOrDefaultAsync(x => x.StoreId == objPurchase.StoreId);
        //        StoreInfo.PurchaseNo = Convert.ToString(Convert.ToInt32(StoreInfo.PurchaseNo) + 1);
        //        _context.MStoreMasters.Update(StoreInfo);
        //        await _context.SaveChangesAsync();

        //        // Add header & detail table records
        //        objPurchase.PurchaseNo = StoreInfo.PurchaseNo;
        //        _context.TPurchaseHeaders.Add(objPurchase);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}




        //public virtual async Task UpdateAsync(TPurchaseHeader objPurchase, int UserId, string Username)
        //{
        //    using var transaction = await _context.Database.BeginTransactionAsync();
        //    try
        //    {
        //        var lst = await _context.TPurchaseDetails.Where(x => x.PurchaseId == objPurchase.PurchaseId).ToListAsync();
        //        _context.TPurchaseDetails.RemoveRange(lst);
        //        await _context.SaveChangesAsync();

        //        _context.TPurchaseHeaders.Update(objPurchase);
        //        _context.Entry(objPurchase).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        await transaction.CommitAsync();
        //    }
        //    catch (Exception)
        //    {
        //        await transaction.RollbackAsync();
        //        throw;
        //    }

        //}


        public virtual async Task UpdateAsync(TPurchaseHeader objPurchase, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Delete details table realted records
                var lst = await _context.TPurchaseDetails.Where(x => x.PurchaseId == objPurchase.PurchaseId).ToListAsync();
                _context.TPurchaseDetails.RemoveRange(lst);

                // Update header & detail table records
                _context.TPurchaseHeaders.Update(objPurchase);
                _context.Entry(objPurchase).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task VerifyAsync(TPurchaseHeader objPurchase, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                TPurchaseHeader objPur = await _context.TPurchaseHeaders.FindAsync(objPurchase.PurchaseId);
                objPur.IsVerified = objPurchase.IsVerified;
                objPur.IsVerifiedId = objPurchase.IsVerifiedId;
                objPur.VerifiedDateTime = objPurchase.VerifiedDateTime;
                _context.TPurchaseHeaders.Update(objPur);
                _context.Entry(objPur).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
      
        public virtual async Task<IPagedList<PurchaseListDto>> GetPurchaseListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PurchaseListDto>(model, "m_Rtrv_PurchaseOrderList_by_Name_Pagn");
        }

        public virtual async Task<IPagedList<PurchaseDetailListDto>> GetPurchaseDetailListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PurchaseDetailListDto>(model, "m_Rtrv_PurchaseItemList");
        }

        public virtual async Task<IPagedList<PurchaseDetailListDto>> GetOldPurchaseorderAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PurchaseDetailListDto>(model, "m_Rtrv_ItemDetailsForPurchasepdate");
        }

        public virtual async Task<IPagedList<LastthreeItemListDto>> GetLastthreeItemListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LastthreeItemListDto>(model, "Rtrv_LastThreeItemInfo");
        }

        public virtual async Task<IPagedList<SupplierRatelistDto>> GetSupplierRatetAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SupplierRatelistDto>(model, "Rtrv_SupplierRate");
        }

        public virtual async Task<IPagedList<GrnListByNameListDto>> GetGRnListbynameAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<GrnListByNameListDto>(model, "Rtrv_GRNReturnList_by_Name");
        }

        public virtual async Task<IPagedList<GRNReturnListDto>> GetGRNReturnList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<GRNReturnListDto>(model, "getGRNReturnList");
        }

        public virtual async Task<IPagedList<ItemListBysupplierNameDto>> GetItemListbysuppliernameAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemListBysupplierNameDto>(model, "Rtrv_ItemList_by_Supplier_Name_For_GRNReturn");
        }

        public virtual async Task<IPagedList<grnlistbynameforgrnreturnlistDto>> Getgrnlistbynameforgrnreturn(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<grnlistbynameforgrnreturnlistDto>(model, "Rtrv_GRNList_by_Name_For_GRNReturn");
        }

       
       
        public virtual async Task<IPagedList<SupplierPaymentStatusListDto>> GetSupplierPaymentStatusList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SupplierPaymentStatusListDto>(model, "Rtrv_GRNList_ForAccount_payment");
        }
    }

       
}
    
