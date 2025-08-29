using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class IssueToDepService : IIssueToDepService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IssueToDepService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<IssuetodeptListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IssuetodeptListDto>(model, "m_Rtrv_IssueToDep_list_by_Name");
        }

        public virtual async Task<IPagedList<IssueToDepartmentDetailListDto>> GetIssueItemListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IssueToDepartmentDetailListDto>(model, "m_rtrv_IssueItemList");
        }

        public virtual async Task<IPagedList<IndentByIDListDto>> GetIndentById(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IndentByIDListDto>(model, "m_Rtrv_Indent_by_ID");
        }

        public virtual async Task<IPagedList<IndentItemListDto>> GetIndentItemList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IndentItemListDto>(model, "retrieve_IndentItemList");
        }


        //Changes Done By Ashutosh 20 May 2025  
        public virtual async Task InsertAsyncSP(TIssueToDepartmentHeader objIssueToDepartment, List<TCurrentStock> OBjTCurrentStock, int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "IssueNo", "Receivedby", "Updatedby", "IsAccepted", "AcceptedBy", "AcceptedDatetime", "TIssueToDepartmentDetails" };
            var entity = objIssueToDepartment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string IssueId = odal.ExecuteNonQuery("v_Insert_IssueToDepartmentHeader_1_New", CommandType.StoredProcedure, "IssueId", entity);
            objIssueToDepartment.IssueId = Convert.ToInt32(IssueId);

            // Add details table records
            foreach (var objissue in objIssueToDepartment.TIssueToDepartmentDetails)
            {
                objissue.IssueId = objIssueToDepartment.IssueId;
            }
            _context.TIssueToDepartmentDetails.AddRange(objIssueToDepartment.TIssueToDepartmentDetails);
            await _context.SaveChangesAsync(UserId, Username);

            foreach (var item in OBjTCurrentStock)
            {

                string[] Entity = { "StockId", "OpeningBalance", "ReceivedQty", "BalanceQty", "UnitMrp", "PurchaseRate", "LandedRate", "VatPercentage", "BatchNo", "BatchExpDate", "PurUnitRate", "PurUnitRateWf", "Cgstper", "Sgstper", "Igstper", "BarCodeSeqNo", "GrnRetQty", "IssDeptQty" };
                var Ientity = item.ToDictionary();
                foreach (var rProperty in Entity)
                {
                    Ientity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_upd_T_Curstk_issdpt_1", CommandType.StoredProcedure, Ientity);
            }
        }

        public virtual async Task<IPagedList<MateralreceivedbyDeptLstDto>> GetMaterialrecivedbydeptList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MateralreceivedbyDeptLstDto>(model, "m_Rtrv_ReceiveIssueToDep_list_by_Name");
        }

        public virtual async Task<IPagedList<MaterialrecvedbydepttemdetailslistDto>> GetRecceivedItemListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MaterialrecvedbydepttemdetailslistDto>(model, "m_rtrv_AcceptIssueItemDetList");
        }
        public virtual async Task InsertAsync(TIssueToDepartmentHeader objIssueToDeptIndent, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update store table records
                MStoreMaster StoreInfo = await _context.MStoreMasters.FirstOrDefaultAsync(x => x.StoreId == objIssueToDeptIndent.FromStoreId);
                if (StoreInfo != null)
                {
                    StoreInfo.IssueToDeptNo = Convert.ToString(Convert.ToInt32(StoreInfo?.IssueToDeptNo ?? "0") + 1);
                    _context.MStoreMasters.Update(StoreInfo);
                    await _context.SaveChangesAsync();
                }

                // Add header & detail table records
                StoreInfo.IssueToDeptNo = Convert.ToString(Convert.ToInt32(StoreInfo?.IssueToDeptNo ?? "0") + 1);
                _context.TIssueToDepartmentHeaders.Add(objIssueToDeptIndent);
                await _context.SaveChangesAsync();

                scope.Complete();


            }

        }
        //shilpa created 23/05/2025
        public virtual async Task UpdateSP(TIssueToDepartmentHeader ObjTIssueToDepartmentHeader, List<TCurrentStock> OBjCurrentStock, TIndentHeader ObjTIndentHeader, List<TIndentDetail> ObjTIndentDetail, int UserId, string Username)
        {
            // //Add header table records
            DatabaseHelper odal = new();
            string[] UEntity = { "IssueNo", "Receivedby", "Updatedby", "IsAccepted", "AcceptedBy", "AcceptedDatetime", "TIssueToDepartmentDetails" };
            var Sntity = ObjTIssueToDepartmentHeader.ToDictionary();
            foreach (var rProperty in UEntity)
            {
                Sntity.Remove(rProperty);
            }
            string IssueId = odal.ExecuteNonQuery("m_Insert_IssueToDepartmentHeader_1_New", CommandType.StoredProcedure, "IssueId", Sntity);
            ObjTIssueToDepartmentHeader.IssueId = Convert.ToInt32(IssueId);

            // Add details table records
            foreach (var OBJIssue in ObjTIssueToDepartmentHeader.TIssueToDepartmentDetails)
            {
                OBJIssue.IssueId = ObjTIssueToDepartmentHeader.IssueId;
            }
            _context.TIssueToDepartmentDetails.AddRange(ObjTIssueToDepartmentHeader.TIssueToDepartmentDetails);
            await _context.SaveChangesAsync(UserId, Username);

            foreach (var item in OBjCurrentStock)
            {
                string[] TEntity = { "StockId", "OpeningBalance", "ReceivedQty", "BalanceQty", "UnitMrp", "PurchaseRate", "LandedRate", "VatPercentage", "BatchNo", "BatchExpDate", "PurUnitRate", "PurUnitRateWf", "Cgstper", "Sgstper", "Igstper", "BarCodeSeqNo", "GrnRetQty", "IssDeptQty" };
                var Centity = item.ToDictionary();
                foreach (var rProperty in TEntity)
                {
                    Centity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_upd_T_Curstk_issdpt_1", CommandType.StoredProcedure, Centity);
            }
            string[] Entity = { "IndentNo", "IndentDate", "IndentTime", "FromStoreId", "ToStoreId", "Addedby", "Isdeleted", "Isverify", "IsInchargeVerify", "IsInchargeVerifyId", "IsInchargeVerifyDate", "Comments", "Priority", "TIndentDetails" };
            var Tentity = ObjTIndentHeader.ToDictionary();
            foreach (var rProperty in Entity)
            {
                Tentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("Update_IndentHeader_Status_AganistIssue", CommandType.StoredProcedure, Tentity);

            foreach (var item in ObjTIndentDetail)
            {
                string[] SEntity = { "ItemId", "Qty", "IssQty", "Indent" };
                var STentity = item.ToDictionary();
                foreach (var rProperty in SEntity)
                {
                    STentity.Remove(rProperty);
                }

                odal.ExecuteNonQuery("PS_Update_Indent_Status_AganistIss", CommandType.StoredProcedure, STentity);
            }

        }

    }
}




