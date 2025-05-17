using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using static LinqToDB.Sql;
using static LinqToDB.SqlQuery.SqlPredicate;
using System.Text.RegularExpressions;
using WkHtmlToPdfDotNet;
using HIMS.Services.OutPatient;

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
        public virtual async Task InsertAsyncSP(TSalesReturnHeader ObjTSalesReturnHeader, List<TSalesReturnDetail> ObjTSalesReturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TSalesDetail> ObjTSalesDetail, TSalesReturnDetail ObjTSalesReturnDetails, TSalesReturnDetail ObjSalesReturnDetails, TSalesHeader ObjTSalesHeader, Payment ObjPayment, int UserId, string Username)
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

            foreach (var item in ObjTSalesReturnDetail)
            {
                     item.SalesReturnId = Convert.ToInt32(vSalesReturnId);

                string[] TEntity = { "Mrp", "Mrptotal", "SalesReturn", "SalesReturnDetId" };
                var Aentity = item.ToDictionary();
                foreach (var rProperty in TEntity)
                {
                    Aentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("insert_SalesReturnDetails_1", CommandType.StoredProcedure, Aentity);
            }

            foreach (var item in ObjTCurrentStock)
            {
                string[] REntity = { "StockId", "OpeningBalance", "ReceivedQty", "BalanceQty", "UnitMrp", "PurchaseRate", "LandedRate", "VatPercentage", "BatchNo", "BatchExpDate", "PurUnitRate", "PurUnitRateWf", "Cgstper", "Sgstper", "Igstper", "BarCodeSeqNo", "GrnRetQty", "IssDeptQty" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in REntity)
                {
                    Pentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("Update_T_CurStk_SalesReturn_Id_1", CommandType.StoredProcedure, Pentity);

            }

            foreach (var item in ObjTSalesDetail)
            {
                string[] REntity = { "SalesId", "ItemId", "BatchNo", "BatchExpDate", "UnitMrp", "Qty", "TotalAmount", "VatPer", "VatAmount", "DiscPer", "DiscAmount", "GrossAmount", "LandedPrice", "TotalLandedAmount", "PurRateWf", "PurTotAmt", "Cgstper", "IssDeptQty", "Sgstper", "Cgstamt","Sgstamt", "Igstper", "Igstamt", "IsPurRate", "StkId", "Mrp", "MrpTotal", "Sales" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in REntity)
                {
                    Pentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("Update_SalesReturnQty_SalesTbl_1", CommandType.StoredProcedure, Pentity);

            }
            string[] AEntity = { "SalesReturnDetId", "SalesDetId", "ItemId", "BatchNo", "BatchExpDate", "UnitMrp", "Qty", "TotalAmount", "SalesId", "VatPer","VatAmount","DiscPer","DiscAmount","GrossAmount","LandedPrice","TotalLandedAmount","PurRate","PurTot","SalesID","SalesDetID","IsCashOrCredit", "Cgstper", "Cgstamt", "Sgstper", "Sgstamt", "Igstper", "Igstamt", "StkId", "Mrp", "Mrptotal", "SalesReturn" };
            var yentity = ObjTSalesReturnDetails.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                yentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("Update_SalesRefundAmt_SalesHeader", CommandType.StoredProcedure, yentity);

            string[] SEntity = { "SalesReturnDetId", "SalesDetId", "ItemId", "BatchNo", "BatchExpDate", "UnitMrp", "Qty", "TotalAmount", "SalesId", "VatPer", "VatAmount", "DiscPer", "DiscAmount", "GrossAmount", "LandedPrice", "TotalLandedAmount", "PurRate", "PurTot", "SalesID", "SalesDetID", "IsCashOrCredit", "Cgstper", "Cgstamt", "Sgstper", "Sgstamt", "Igstper", "Igstamt", "StkId", "Mrp", "Mrptotal", "SalesReturn" };
            var Rentity = ObjSalesReturnDetails.ToDictionary();
            foreach (var rProperty in SEntity)
            {
                Rentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("Cal_GSTAmount_SalesReturn", CommandType.StoredProcedure, Rentity);

            string[] HEntity = { "SalesId", "Date", "Time", "SalesNo", "OpIpId", "OpIpType", "TotalAmount", "VatAmount","DiscAmount" ,"NetAmount", "PaidAmount", "BalanceAmount", "ConcessionReasonId", "ConcessionAuthorizationId", "CashCounterId",
                "IsSellted","IsPrint","IsFree","UnitId","AddedBy","UpdatedBy","ExternalPatientName","DoctorName","StoreId","IsPrescription","ExtRegNo","CreditReason",
            "CreditReasonId","RefundAmt","WardId","BedId","DiscperH","IsPurBill","IsBillCheck","IsRefundFlag","IsCancelled","SalesHeadName","SalesTypeId","RegId","PatientName",
                "RegNo","ExtMobileNo","RoundOff","ExtAddress","TSalesDetails"};
            var Ventity = ObjTSalesHeader.ToDictionary();
            foreach (var rProperty in HEntity)
            {
                Ventity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("Insert_ItemMovementReport_Cursor", CommandType.StoredProcedure, Ventity);

            string[] PEntity = { "Tdsamount","ReceiptNo", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            var Sentity = ObjPayment.ToDictionary();
            foreach (var rProperty in PEntity)
            {
                Sentity.Remove(rProperty);
            }
            Sentity["OPDIPDType"] = 1;
            string PaymentId = odal.ExecuteNonQuery("insert_Payment_Pharmacy_New_1", CommandType.StoredProcedure, "PaymentId", Sentity);
            ObjPayment.PaymentId = Convert.ToInt32(PaymentId);

        }


    }
}
