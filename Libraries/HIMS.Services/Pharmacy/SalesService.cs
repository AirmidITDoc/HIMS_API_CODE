using HIMS.Core.Domain.Grid;
using HIMS.Core.Domain.Logging;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Master;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Pharmacy;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Users
{
    public class SalesService : ISalesService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public SalesService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task InsertAsync(TSalesHeader objSales, Payment objPayment, int UserId, string Username)
        {
            objSales.RoundOff = objSales.PaidAmount - objSales.NetAmount;
            _context.TSalesHeaders.Add(objSales);
            await _context.SaveChangesAsync();
            DatabaseHelper odal = new();
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@SalesId", objSales.SalesId);
            para[1] = new SqlParameter("@StoreId", objSales.StoreId);
            odal.ExecuteScalar("UpdateBillNo", CommandType.StoredProcedure, para);
            foreach (var objItem in objSales.TSalesDetails)
            {
                TCurrentStock objStock = await _context.TCurrentStocks.FirstOrDefaultAsync(x => x.StockId == objItem.StkId && x.StoreId == objSales.StoreId && x.ItemId == objItem.ItemId);
                if (objStock != null)
                {
                    objStock.IssueQty += (float)objItem.Qty;
                    objStock.BalanceQty += (float)objItem.Qty;
                    _context.Entry(objStock).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            var config = (await _context.ConfigSettings.ToListAsync()).FirstOrDefault();
            var StoreInfo = await _context.MStoreMasters.FirstOrDefaultAsync(x => x.StoreId == objSales.StoreId);
            if (config != null)
            {
                using var transaction = _context.Database.BeginTransaction();

                try
                {
                    transaction.CreateSavepoint("insert_Payment_Pharmacy_New_1");
                    if (objSales.OpIpType == 0 || objSales.OpIpType == 1 || objSales.OpIpType == 3)
                    {
                        objPayment.CashCounterId = objSales.OpIpType == 0 ? config.OpdReceiptCounterId : (objSales.OpIpType == 1 ? config.IpdReceiptCounterId : StoreInfo.PharSalRecCountId);
                        var objCashCounter = await _context.CashCounters.FirstOrDefaultAsync(x => x.CashCounterId == objPayment.CashCounterId);
                        objPayment.ReceiptNo = (objCashCounter != null ? Convert.ToInt64(objCashCounter.BillNo) + 1 : 1).ToString();
                        objPayment.BillNo = objSales.SalesId;
                        _context.Payments.Add(objPayment);
                        objCashCounter.BillNo = objPayment.ReceiptNo;
                        _context.Entry(objCashCounter).State = EntityState.Modified;
                        await _context.SaveChangesAsync(UserId, Username);
                    }
                    else if (objSales.OpIpType == 4)
                    {
                        PaymentPharmacy objPharmacy = await _context.PaymentPharmacies.FirstOrDefaultAsync(x => x.BillNo == objSales.SalesId);
                        if ((objPharmacy?.PaymentId ?? 0) > 0)
                        {
                            objPharmacy.StrId = objSales.StoreId;
                            _context.Entry(objPharmacy).State = EntityState.Modified;
                            await _context.SaveChangesAsync(UserId, Username);
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.RollbackToSavepoint("insert_Payment_Pharmacy_New_1");
                }
            }

            if (objSales.OpIpType == 0)
            {

            }
        }



        public virtual async Task<IPagedList<PharSalesCurrentSumryListDto>> GetList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PharSalesCurrentSumryListDto>(model, "m_rtrv_Phar_SalesList_CurrentSumry");
        }
        public virtual async Task<IPagedList<PharCurrentDetListDto>> SalesDetailsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PharCurrentDetListDto>(model, "m_rtrv_Phar_SalesList_CurrentDet");
        }

        public virtual async Task<IPagedList<SalesDetailsListDto>> Getsalesdetaillist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesDetailsListDto>(model, "ps_Rtrv_SalesDetails");
        }

        public virtual async Task<IPagedList<SalesBillListDto>> salesbrowselist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesBillListDto>(model, "ps_Rtrv_SalesBillList");
        }
        public virtual async Task<IPagedList<SalesDraftBillListDto>> SalesDraftBillList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesDraftBillListDto>(model, "m_Rtrv_SalesDraftBillList");
        }
        public virtual async Task<IPagedList<BalAvaStoreListDto>> BalAvaStoreList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BalAvaStoreListDto>(model, "m_getBalAvaListStore");
        }

        public virtual async Task<IPagedList<PrescriptionListforSalesDto>> PrescriptionList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PrescriptionListforSalesDto>(model, "m_Retrieve_PrescriptionListforSales");
        }

        public virtual async Task<IPagedList<PrescriptionDetListDto>> PrescriptionDetList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PrescriptionDetListDto>(model, "Ret_PrescriptionDet");
        }


        // done by Ashu Date : 20-May-2025

        public virtual async Task InsertAsyncSP(TSalesHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, Payment ObjPayment, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "SalesNo", "CashCounterId", "ExtRegNo", "RefundAmt", "IsRefundFlag", "RegId", "PatientName", "RegNo", "RoundOff", "UpdatedBy", "IsCancelled", "TSalesDetails" };
            var entity = ObjSalesHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string SalesId = odal.ExecuteNonQuery("m_insert_Sales_1", CommandType.StoredProcedure, "SalesId", entity);
            ObjSalesHeader.SalesId = Convert.ToInt32(SalesId);

            // Add details table records
            foreach (var item in ObjSalesHeader.TSalesDetails)
            {
                item.SalesId = ObjSalesHeader.SalesId;
            }
            _context.TSalesDetails.AddRange(ObjSalesHeader.TSalesDetails);
            await _context.SaveChangesAsync(UserId, Username);

            foreach (var item in ObjTCurrentStock)
            {

                string[] Entity = { "StockId", "OpeningBalance", "ReceivedQty", "BalanceQty", "UnitMrp", "PurchaseRate", "LandedRate", "VatPercentage", "BatchNo", "BatchExpDate", "PurUnitRate", "PurUnitRateWf", "Cgstper", "Sgstper", "Igstper", "BarCodeSeqNo", "GrnRetQty", "IssDeptQty" };
                var Ientity = item.ToDictionary();
                foreach (var rProperty in Entity)
                {
                    Ientity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_Update_T_CurStk_Sales_Id_1", CommandType.StoredProcedure, Ientity);
            }
            var SalesIdObj = new
            {
                SalesId = Convert.ToInt32(SalesId)
            };
            odal.ExecuteNonQuery("m_Cal_DiscAmount_Sales", CommandType.StoredProcedure, SalesIdObj.ToDictionary());

            var SalessIdObj = new
            {
                SalesId = Convert.ToInt32(SalesId)
            };
            odal.ExecuteNonQuery("m_Cal_GSTAmount_Sales", CommandType.StoredProcedure, SalesIdObj.ToDictionary());


            string[] PEntity = { "Tdsamount", "ReceiptNo", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            var Sentity = ObjPayment.ToDictionary();
            foreach (var rProperty in PEntity)
            {
                Sentity.Remove(rProperty);
            }
            Sentity["OPDIPDType"] = 1;
            string PaymentId = odal.ExecuteNonQuery("insert_Payment_Pharmacy_New_1", CommandType.StoredProcedure, "PaymentId", Sentity);
            ObjPayment.PaymentId = Convert.ToInt32(PaymentId);


            string[] TEntity = { "IppreId","IpmedId", "IPMedID", "OpdIpdType", "Pdate", "Ptime", "ClassId", "GenericId", "DrugId", "DoseId", "Days", "QtyPerDay", "TotalQty", "Remark",
                "IsAddBy", "StoreId", "WardId", "GrnRetQty", "IssDeptQty" ,"Ipmed"};
            var Nentity = ObjPrescription.ToDictionary();
            foreach (var rProperty in TEntity)
            {
                Nentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_Update_T_IPPrescription_Isclosed_Status_1", CommandType.StoredProcedure, Nentity);

            string[] DEntity = { "Date","Time", "SalesNo", "OpIpId", "OpIpType", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "PaidAmount", "BalanceAmount",
                "ConcessionReasonId", "ConcessionAuthorizationId", "CashCounterId",
                "IsSellted", "IsPrint", "UnitId", "AddedBy", "UpdatedBy" ,"ExternalPatientName","DoctorName","StoreId","CreditReason","CreditReasonId",
                "IsCancelled","IsPrescription","WardId","BedId","ExtMobileNo","ExtAddress"};
            var Hentity = ObjDraftHeader.ToDictionary();
            foreach (var rProperty in DEntity)
            {
                Hentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_Update_T_SalDraHeader_IsClosed_1", CommandType.StoredProcedure, Hentity);
        }

        // done by Ashu Date : 20-May-2025
        public virtual async Task InsertAsyncSPC(TSalesHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "SalesNo", "CashCounterId", "ExtRegNo", "RefundAmt", "IsRefundFlag", "RegId", "PatientName", "RegNo", "RoundOff", "UpdatedBy", "IsCancelled", "TSalesDetails" };
            var entity = ObjSalesHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string SalesId = odal.ExecuteNonQuery("m_insert_Sales_1", CommandType.StoredProcedure, "SalesId", entity);
            ObjSalesHeader.SalesId = Convert.ToInt32(SalesId);

            // Add details table records
            foreach (var item in ObjSalesHeader.TSalesDetails)
            {
                item.SalesId = ObjSalesHeader.SalesId;
            }
            _context.TSalesDetails.AddRange(ObjSalesHeader.TSalesDetails);
            await _context.SaveChangesAsync(UserId, Username);

            foreach (var item in ObjTCurrentStock)
            {

                string[] Entity = { "StockId", "OpeningBalance", "ReceivedQty", "BalanceQty", "UnitMrp", "PurchaseRate", "LandedRate", "VatPercentage", "BatchNo", "BatchExpDate", "PurUnitRate", "PurUnitRateWf", "Cgstper", "Sgstper", "Igstper", "BarCodeSeqNo", "GrnRetQty", "IssDeptQty" };
                var Ientity = item.ToDictionary();
                foreach (var rProperty in Entity)
                {
                    Ientity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_Update_T_CurStk_Sales_Id_1", CommandType.StoredProcedure, Ientity);
            }
            var SalesIdObj = new
            {
                SalesId = Convert.ToInt32(SalesId)
            };
            odal.ExecuteNonQuery("m_Cal_DiscAmount_Sales", CommandType.StoredProcedure, SalesIdObj.ToDictionary());

            var SalessIdObj = new
            {
                SalesId = Convert.ToInt32(SalesId)
            };
            odal.ExecuteNonQuery("m_Cal_GSTAmount_Sales", CommandType.StoredProcedure, SalesIdObj.ToDictionary());


            //string[] PEntity = { "Tdsamount", "ReceiptNo", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            //var Sentity = ObjPayment.ToDictionary();
            //foreach (var rProperty in PEntity)
            //{
            //    Sentity.Remove(rProperty);
            //}
            //Sentity["OPDIPDType"] = 1;
            //string PaymentId = odal.ExecuteNonQuery("insert_Payment_Pharmacy_New_1", CommandType.StoredProcedure, "PaymentId", Sentity);
            //ObjPayment.PaymentId = Convert.ToInt32(PaymentId);


            string[] TEntity = { "IppreId","IpmedId", "IPMedID", "OpdIpdType", "Pdate", "Ptime", "ClassId", "GenericId", "DrugId", "DoseId", "Days", "QtyPerDay", "TotalQty", "Remark",
                "IsAddBy", "StoreId", "WardId", "GrnRetQty", "IssDeptQty" ,"Ipmed"};
            var Nentity = ObjPrescription.ToDictionary();
            foreach (var rProperty in TEntity)
            {
                Nentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_Update_T_IPPrescription_Isclosed_Status_1", CommandType.StoredProcedure, Nentity);

            string[] DEntity = { "Date","Time", "SalesNo", "OpIpId", "OpIpType", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "PaidAmount", "BalanceAmount",
                "ConcessionReasonId", "ConcessionAuthorizationId", "CashCounterId",
                "IsSellted", "IsPrint", "UnitId", "AddedBy", "UpdatedBy" ,"ExternalPatientName","DoctorName","StoreId","CreditReason","CreditReasonId",
                "IsCancelled","IsPrescription","WardId","BedId","ExtMobileNo","ExtAddress"};
            var Hentity = ObjDraftHeader.ToDictionary();
            foreach (var rProperty in DEntity)
            {
                Hentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_Update_T_SalDraHeader_IsClosed_1", CommandType.StoredProcedure, Hentity);
        }


        public virtual async Task InsertAsyncSPD( TSalesDraftHeader ObjDraftHeader, List<TSalesDraftDet> ObjTSalesDraftDet, int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "SalesNo", "CashCounterId","UpdatedBy", "IsCancelled" };
            var entity = ObjDraftHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string DsalesId = odal.ExecuteNonQuery("m_insert_T_SalesDraftHeader_1", CommandType.StoredProcedure, "DsalesId", entity);
             ObjDraftHeader.DsalesId = Convert.ToInt32(DsalesId);

            foreach (var item in ObjTSalesDraftDet)
            {
                item.DsalesId = Convert.ToInt32(DsalesId);


                string[] pEntity = { "SalDetId" };
                var Tentity = item.ToDictionary();
                foreach (var rProperty in pEntity)
                {
                    Tentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("insert_T_SalesDraftDet_1", CommandType.StoredProcedure, Tentity);
            }
        } 
        
    }
}
