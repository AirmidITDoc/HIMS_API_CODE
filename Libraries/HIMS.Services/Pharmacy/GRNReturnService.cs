using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.GRN;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Transactions;

namespace HIMS.Services.Pharmacy
{
    public class GRNReturnService : IGRNReturnService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public GRNReturnService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsyncSP(TGrnreturnHeader objGRNReturn, List<TCurrentStock> objCStock, List<TGrndetail> objReturnQty, int UserId, string Username)
        {
            // Add header table records
            DatabaseHelper odal = new();
            SqlParameter[] para = new SqlParameter[21];
            para[0] = new SqlParameter("@GrnId", objGRNReturn.Grnid);
            para[1] = new SqlParameter("@GRNReturnDate", objGRNReturn.GrnreturnDate);
            para[2] = new SqlParameter("@GRNReturnTime", objGRNReturn.GrnreturnTime);
            para[3] = new SqlParameter("@StoreId", objGRNReturn.StoreId);
            para[4] = new SqlParameter("@SupplierID", objGRNReturn.SupplierId);
            para[5] = new SqlParameter("@TotalAmount", objGRNReturn.TotalAmount);
            para[6] = new SqlParameter("@GrnReturnAmount", objGRNReturn.GrnReturnAmount);
            para[7] = new SqlParameter("@TotalDiscAmount", objGRNReturn.TotalDiscAmount);
            para[8] = new SqlParameter("@TotalVATAmount", objGRNReturn.TotalVatAmount);
            para[9] = new SqlParameter("@TotalOtherTaxAmount", objGRNReturn.TotalOtherTaxAmount);
            para[10] = new SqlParameter("@TotalOctroiAmount", objGRNReturn.TotalOctroiAmount);

            para[11] = new SqlParameter("@NetAmount", objGRNReturn.NetAmount);
            para[12] = new SqlParameter("@Cash_Credit", objGRNReturn.CashCredit);
            para[13] = new SqlParameter("@Remark", objGRNReturn.Remark);
            para[14] = new SqlParameter("@IsVerified", objGRNReturn.IsVerified);
            para[15] = new SqlParameter("IsVerified", objGRNReturn.IsVerified);
            para[16] = new SqlParameter("@AddedBy", objGRNReturn.AddedBy ?? 0);
            para[17] = new SqlParameter("@IsCancelled", objGRNReturn.IsCancelled);
            para[18] = new SqlParameter("@IsClosed", objGRNReturn.IsClosed);
            para[19] = new SqlParameter("@GrnType", objGRNReturn.GrnType);
            para[20] = new SqlParameter("@IsGrnTypeFlag", objGRNReturn.IsGrnTypeFlag);

            para[21] = new SqlParameter("@GRNReturnId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };

            string grnreturnNo = odal.ExecuteNonQuery("m_insert_GRNReturnH_GrnReturnNo_1", CommandType.StoredProcedure, "@GRNReturnId", para);
            objGRNReturn.GrnreturnNo = grnreturnNo;

            // Add details table records
            foreach (var objItem in objGRNReturn.TGrnreturnDetails)
            {
                objItem.GrnreturnId = objGRNReturn.GrnreturnId;
            }
            _context.TGrnreturnDetails.AddRange(objGRNReturn.TGrnreturnDetails);
            await _context.SaveChangesAsync(UserId, Username);
        }

        public virtual async Task InsertAsync(TGrnreturnHeader objGRNReturn, List<TCurrentStock> objCStock, List<TGrndetail> objReturnQty, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update store table records
                MStoreMaster StoreInfo = await _context.MStoreMasters.FirstOrDefaultAsync(x => x.StoreId == objGRNReturn.StoreId);
                StoreInfo.GrnreturnNo = Convert.ToString(Convert.ToInt32(StoreInfo.GrnreturnNo) + 1);
                _context.MStoreMasters.Update(StoreInfo);
                await _context.SaveChangesAsync();

                // Add header & detail table records
                objGRNReturn.GrnreturnNo = StoreInfo.GrnreturnNo;
                _context.TGrnreturnHeaders.Add(objGRNReturn);
                await _context.SaveChangesAsync();

                // Update curren stock table records
                List<TCurrentStock> objCStockList = new();
                foreach (var objC in objCStock)
                {
                    var objCInfo = await _context.TCurrentStocks.FirstOrDefaultAsync(x => x.ItemId == objC.ItemId && x.StockId == objC.StockId && x.StoreId == objC.StoreId);
                    if (objCInfo != null)
                    {
                        objCInfo.GrnRetQty = Convert.ToInt32(objCInfo.GrnRetQty) + objC.IssueQty;
                        objCStockList.Add(objC);
                        // Tell EF to only update this property
                        _context.Entry(objCInfo).Property(x => x.GrnRetQty).IsModified = true; 
                    }
                }
                await _context.SaveChangesAsync();
             //   Update grn details table records
                List<TGrndetail> objGrnList = new();
                foreach (var objGrn in objReturnQty)
                {
                    var objGRNInfo = await _context.TGrndetails.FirstOrDefaultAsync(x => x.GrndetId == objGrn.GrndetId);
                    if (objGRNInfo != null)
                    {
                        objGRNInfo.ReturnQty = Convert.ToInt32(objGRNInfo.ReturnQty) + objGrn.ReturnQty;
                        objGrnList.Add(objGRNInfo);
                        // Tell EF to only update this property
                        _context.Entry(objGRNInfo).Property(x => x.ReturnQty).IsModified = true;
                    }
                }
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }

        public virtual async Task VerifyAsync(TGrnreturnHeader objGRN,  int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "GrnreturnNo", "Grnid", "GrnreturnDate", "GrnreturnTime", "StoreId", "SupplierId", "TotalAmount", "GrnReturnAmount", "TotalDiscAmount", "TotalVatAmount", "TotalOtherTaxAmount", "TotalOctroiAmount", "NetAmount", "CashCredit",
                "Remark","AddedBy","UpdatedBy","IsCancelled","IsClosed","Prefix","GrnType","IsGrnTypeFlag","TGrnreturnDetails" };
            var entity = objGRN.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_Update_GRNReturn_Verify_Status_1", CommandType.StoredProcedure,  entity);
        
        }

        public virtual async Task<IPagedList<GrnListByNameListDto>> GetGRnListbynameAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<GrnListByNameListDto>(model, "ps_Rtrv_GRNReturnList_by_Name");
        }

        public virtual async Task<IPagedList<GRNReturnListDto>> GetGRNReturnList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<GRNReturnListDto>(model, "getGRNReturnList");
        }

        public virtual async Task<IPagedList<grnlistbynameforgrnreturnlistDto>> Getgrnlistbynameforgrnreturn(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<grnlistbynameforgrnreturnlistDto>(model, "Rtrv_GRNList_by_Name_For_GRNReturn");
        }


        public virtual async Task<IPagedList<ItemListBysupplierNameDto>> ItemListBysupplierNameAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemListBysupplierNameDto>(model, "Rtrv_ItemList_by_Supplier_Name_For_GRNReturn");
        }

    }
}
