using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;
//using static LinqToDB.Sql;
//using static LinqToDB.SqlQuery.SqlPredicate;

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
        public virtual async Task<IPagedList<BrowseSalesBillListDto>> BrowseSalesBillList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseSalesBillListDto>(model, "m_Retrieve_BrowseSalesBill");
        }

        public virtual async Task<IPagedList<SalesBillReturnCashListDto>> SalesBillReturnCashList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesBillReturnCashListDto>(model, "Retrieve_SalesBill_Return_Cash");
        }
        public virtual async Task<IPagedList<SalesBillReturnCreditListDto>> SalesBillReturnCreditList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesBillReturnCreditListDto>(model, "Retrieve_SalesBill_Return_Credit");
        }
        //Changes Done By Ashutosh 19 May 2025 
        public virtual void InsertSP(TSalesReturnHeader ObjTSalesReturnHeader, List<TSalesReturnDetail> ObjTSalesReturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TSalesDetail> ObjTSalesDetail, PaymentPharmacy ObjPayment, List<TPaymentPharmacy> ObjTPaymentPharmacy ,int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] Entity = { "SalesReturnNo", "CashCounterId", "UpdatedBy", "IsBillCheck", "TSalesReturnDetails" };
            var entity = ObjTSalesReturnHeader.ToDictionary();
            foreach (var rProperty in Entity)
            {
                entity.Remove(rProperty);
            }
            string vSalesReturnId = odal.ExecuteNonQuery("PS_insert_SalesReturnHeader_1", CommandType.StoredProcedure, "SalesReturnId", entity);
            ObjTSalesReturnHeader.SalesReturnId = Convert.ToInt32(vSalesReturnId);
            ObjPayment.RefundId = Convert.ToInt32(vSalesReturnId);


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
                string[] REntity = { "SalesId", "ItemId", "BatchNo", "BatchExpDate", "UnitMrp", "Qty", "TotalAmount", "VatPer", "VatAmount", "DiscPer", "DiscAmount", "GrossAmount", "LandedPrice", "TotalLandedAmount", "PurRateWf", "PurTotAmt", "Cgstper", "IssDeptQty", "Sgstper", "Cgstamt", "Sgstamt", "Igstper", "Igstamt", "IsPurRate", "StkId", "Mrp", "MrpTotal", "Sales" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in REntity)
                {
                    Pentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("Update_SalesReturnQty_SalesTbl_1", CommandType.StoredProcedure, Pentity);

            }
            var SalesReturnIdObj = new
            {
                SalesReturnId = Convert.ToInt32(vSalesReturnId)
            };
            odal.ExecuteNonQuery("Update_SalesRefundAmt_SalesHeader", CommandType.StoredProcedure, SalesReturnIdObj.ToDictionary());

            var SalesReturnsIdObj = new
            {
                SalesReturnId = Convert.ToInt32(vSalesReturnId)
            };
            odal.ExecuteNonQuery("Cal_GSTAmount_SalesReturn", CommandType.StoredProcedure, SalesReturnsIdObj.ToDictionary());


            var SalesReturnObj = new
            {
                Id = Convert.ToInt32(vSalesReturnId),
                TypeId = 2

            };
            odal.ExecuteNonQuery("Insert_ItemMovementReport_Cursor", CommandType.StoredProcedure, SalesReturnObj.ToDictionary());

            string[] PEntity = { "StrId", "ReceiptNo", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var Sentity = ObjPayment.ToDictionary();
            foreach (var rProperty in PEntity)
            {
                Sentity.Remove(rProperty);
            }
            string PaymentId = odal.ExecuteNonQuery("insert_Payment_Pharmacy_New_1", CommandType.StoredProcedure, "PaymentId", Sentity);
            ObjPayment.PaymentId = Convert.ToInt32(PaymentId);
            foreach (var item in ObjTPaymentPharmacy)
            {
                //item.AdvanceId = Convert.ToInt32(VAdvanceDetailID);


                string[] SEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                                           "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};

                var pentity = item.ToDictionary();
                foreach (var rProperty in pentity.Keys.ToList())
                {
                    if (!SEntity.Contains(rProperty))
                        pentity.Remove(rProperty);
                }
                string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_PaymentPharmacy", CommandType.StoredProcedure, "PaymentId", pentity);
                item.PaymentId = Convert.ToInt32(VPaymentId);

            }

        }
        //Changes Done By Ashutosh 19 May 2025 
        public virtual void InsertSPCredit(TSalesReturnHeader ObjTSalesReturnHeader, List<TSalesReturnDetail> ObjTSalesReturnDetail, List<TCurrentStock> ObjTCurrentStock, List<TSalesDetail> ObjTSalesDetail, int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] Entity = { "SalesReturnNo", "CashCounterId", "UpdatedBy", "IsBillCheck", "TSalesReturnDetails" };
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
                string[] REntity = { "SalesId", "ItemId", "BatchNo", "BatchExpDate", "UnitMrp", "Qty", "TotalAmount", "VatPer", "VatAmount", "DiscPer", "DiscAmount", "GrossAmount", "LandedPrice", "TotalLandedAmount", "PurRateWf", "PurTotAmt", "Cgstper", "IssDeptQty", "Sgstper", "Cgstamt", "Sgstamt", "Igstper", "Igstamt", "IsPurRate", "StkId", "Mrp", "MrpTotal", "Sales" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in REntity)
                {
                    Pentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("Update_SalesReturnQty_SalesTbl_1", CommandType.StoredProcedure, Pentity);

            }
            var SalesReturnIdObj = new
            {
                SalesReturnId = Convert.ToInt32(vSalesReturnId)
            };
            odal.ExecuteNonQuery("Update_SalesRefundAmt_SalesHeader", CommandType.StoredProcedure, SalesReturnIdObj.ToDictionary());

            var SalesReturnsIdObj = new
            {
                SalesReturnId = Convert.ToInt32(vSalesReturnId)
            };
            odal.ExecuteNonQuery("Cal_GSTAmount_SalesReturn", CommandType.StoredProcedure, SalesReturnsIdObj.ToDictionary());


            var SalesReturnObj = new
            {
                Id = Convert.ToInt32(vSalesReturnId),
                TypeId = 2

            };
            odal.ExecuteNonQuery("Insert_ItemMovementReport_Cursor", CommandType.StoredProcedure, SalesReturnObj.ToDictionary());

            //string[] PEntity = { "Tdsamount", "ReceiptNo", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            //var Sentity = ObjPayment.ToDictionary();
            //foreach (var rProperty in PEntity)
            //{
            //    Sentity.Remove(rProperty);
            //}
            //Sentity["OPDIPDType"] = 1;
            //string PaymentId = odal.ExecuteNonQuery("insert_Payment_Pharmacy_New_1", CommandType.StoredProcedure, "PaymentId", Sentity);
            //ObjPayment.PaymentId = Convert.ToInt32(PaymentId);

        }


    }
}
