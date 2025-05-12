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

        public virtual async Task<IPagedList<TIssueToDepartmentDetail>> GetIssueItemListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<TIssueToDepartmentDetail>(model, "m_rtrv_IssueItemList");
        }

        public virtual async Task<IPagedList<IndentByIDListDto>> GetIndentById(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IndentByIDListDto>(model, "m_Rtrv_Indent_by_ID");
        }

        public virtual async Task<IPagedList<IndentItemListDto>> GetIndentItemList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IndentItemListDto>(model, "retrieve_IndentItemList");
        }

        
        public virtual async Task InsertAsyncSP(TIssueToDepartmentHeader objIssueToDepartment, TCurrentStock OBjTCurrentStock, int UserId, string Username)
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

                string[] Entity = { "StockId", "OpeningBalance", "ReceivedQty", "BalanceQty", "UnitMrp", "PurchaseRate", "LandedRate", "VatPercentage", "BatchNo", "BatchExpDate", "PurUnitRate", "PurUnitRateWf", "Cgstper", "Sgstper", "Igstper", "BarCodeSeqNo", "GrnRetQty", "IssDeptQty" };
                var Ientity = OBjTCurrentStock.ToDictionary();
                foreach (var rProperty in Entity)
                {
                  Ientity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("v_upd_T_Curstk_issdpt_1", CommandType.StoredProcedure, Ientity);

        }

    }

}




