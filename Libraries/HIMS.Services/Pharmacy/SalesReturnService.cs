using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;

namespace HIMS.Services.Pharmacy
{
    public class SalesReturnService : ISalesReturnService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public SalesReturnService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<SalesReturnDetailsListDto>> salesreturndetaillist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesReturnDetailsListDto>(model, "ps_Rtrv_SalesReturnDetails");
        }
        public virtual async Task<IPagedList<SalesReturnBillListDto>> salesreturnlist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesReturnBillListDto>(model, "ps_Rtrv_SalesReturnBillList");
        }
        public virtual async Task<IPagedList<SalesRetrunCurrentSumryListDto>> SalesReturnSummaryList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesRetrunCurrentSumryListDto>(model, "m_rtrv_Phar_SalesRetrunList_CurrentSumry");
        }
        public virtual async Task<IPagedList<SalesRetrunLCurrentDetListDto>> SalesReturnDetailsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesRetrunLCurrentDetListDto>(model, "m_rtrv_Phar_SalesRetrunList_CurrentDet");
        }
        public virtual async Task InsertAsyncSP(TSalesReturnHeader ObjTSalesReturnHeader, /*TCurrentStock ObjTCurrentStock ,*/int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] Entity = { "SalesReturnNo", "CashCounterId", "UpdatedBy", "IsPurBill", "IsBillCheck", "TSalesReturnDetails" };
            var entity = ObjTSalesReturnHeader.ToDictionary();
            foreach (var rProperty in Entity)
            {
                entity.Remove(rProperty);
            }
            string vSalesReturnId = odal.ExecuteNonQuery("PS_insert_SalesReturnHeader_1", CommandType.StoredProcedure, "SalesReturnId", entity);
            ObjTSalesReturnHeader.SalesReturnId = Convert.ToInt32(vSalesReturnId);

            // Add details table records
            foreach (var objSales in ObjTSalesReturnHeader.TSalesReturnDetails)
            {
                objSales.SalesReturnId = ObjTSalesReturnHeader.SalesReturnId;
            }
            _context.TSalesReturnDetails.AddRange(ObjTSalesReturnHeader.TSalesReturnDetails);
            await _context.SaveChangesAsync(UserId, Username);

            //string[] REntity = { "StockId", "OpeningBalance", "ReceivedQty", "BalanceQty", "UnitMrp", "PurchaseRate", "LandedRate", "VatPercentage", "BatchNo", "BatchExpDate", "PurUnitRate", "PurUnitRateWf", "Cgstper", "Sgstper", "Igstper", "BarCodeSeqNo", "GrnRetQty", "IssDeptQty" };
            //var Rentity = ObjTCurrentStock.ToDictionary();
            //foreach (var rProperty in REntity)
            //{
            //    Rentity.Remove(rProperty);
            //}
            //odal.ExecuteNonQuery("Update_T_CurStk_SalesReturn_Id_1", CommandType.StoredProcedure, Rentity);
        }
        }
    }
