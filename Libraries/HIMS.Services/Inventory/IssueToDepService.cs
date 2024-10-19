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
    public class IssueToDepService : IIssueToDepService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IssueToDepService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsyncSP(TIssueToDepartmentHeader objIssueToDepartment, TIssueToDepartmentDetail objTIssueToDepartmentDetail, TCurrentStock objCurrentStock ,int UserId, string Username)
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
            
                string[] dEntity = { "IssueDepId", "ReturnQty", "Status"};
                var dentity = objIssueToDepartment.ToDictionary();
                foreach (var rProperty in dEntity)
                 {
                 entity.Remove(rProperty);
                 }
                 odal.ExecuteNonQuery("v_insert_IssueToDepartmentDetails_1", CommandType.StoredProcedure,  entity);



                  string[] cEntity = { "StockId", "OpeningBalance", "ReceivedQty", "BalanceQty", "UnitMrp", "PurchaseRate", "LandedRate", "VatPercentage", "BatchNo", "BatchExpDate", "PurUnitRate", "PurUnitRateWf", "Cgstper", "Sgstper", "Igstper",
                 "BarCodeSeqNo","GrnRetQty","IssDeptQty" };
                 var centity = objIssueToDepartment.ToDictionary();
                 foreach (var rProperty in cEntity)
                 {
                  entity.Remove(rProperty);
                 }
                 odal.ExecuteNonQuery("v_upd_T_Curstk_issdpt_1", CommandType.StoredProcedure,  entity);


        }
    }
}

