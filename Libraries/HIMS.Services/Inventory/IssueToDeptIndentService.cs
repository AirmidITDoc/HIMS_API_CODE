using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data;
using HIMS.Data.DataProviders;
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
    public class IssueToDeptIndentService : IIssueToDeptIndentService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IssueToDeptIndentService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
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
        public virtual async Task UpdateSP(TIssueToDepartmentHeader ObjTIssueToDepartmentHeader,  List<TCurrentStock> OBjCurrentStock, TIndentHeader ObjTIndentHeader ,int UserId, string Username)
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
                string[] Entity = { "IndentNo", "IndentDate", "IndentTime", "FromStoreId", "ToStoreId", "Addedby", "Isdeleted", "Isverify", "IsInchargeVerify", "IsInchargeVerifyId", "IsInchargeVerifyDate", "Comments", "Priority", "TIndentDetails"};
                var Tentity = ObjTIndentHeader.ToDictionary();
                foreach (var rProperty in Entity)
                {
                Tentity.Remove(rProperty);
                }
               odal.ExecuteNonQuery("Update_IndentHeader_Status_AganistIssue", CommandType.StoredProcedure, Tentity);

        }
    }
}
