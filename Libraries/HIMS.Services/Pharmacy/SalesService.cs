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
using static LinqToDB.Common.Configuration;

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



       


        // done by Ashu Date : 20-May-2025

        public virtual async Task InsertAsyncSP(TSalesHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, Payment ObjPayment, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "SalesNo", "CashCounterId", "ExtRegNo", "RefundAmt", "IsRefundFlag", "RegId", "PatientName", "RegNo",  "UpdatedBy", "IsCancelled", "TSalesDetails" };
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
            Sentity["OPDIPDType"] = 3;
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
            string[] rEntity = { "SalesNo", "CashCounterId", "ExtRegNo", "RefundAmt", "IsRefundFlag", "RegId", "PatientName", "RegNo",  "UpdatedBy", "IsCancelled", "TSalesDetails" };
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
        //shilpa//26/05/2025
        public virtual async Task InsertAsyncS(TPhadvanceHeader ObjTPhadvanceHeader, TPhadvanceDetail ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "StoreId" };
            var entity = ObjTPhadvanceHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string VAdvanceId = odal.ExecuteNonQuery("PS_insert_T_PHAdvanceHeader_1", CommandType.StoredProcedure, "AdvanceId", entity);
            ObjTPhadvanceHeader.AdvanceId = Convert.ToInt32(VAdvanceId);
            ObjTPhadvanceDetail.AdvanceId = Convert.ToInt32(VAdvanceId);
            ObjPaymentPharmacy.AdvanceId = Convert.ToInt32(VAdvanceId);



            string[] DEntity = { "AdvanceNo" };
            var Dentity = ObjTPhadvanceDetail.ToDictionary();
            foreach (var rProperty in DEntity)
            {
                Dentity.Remove(rProperty);
            }
            string VAdvanceDetailID = odal.ExecuteNonQuery("PS_insert_TPHAdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", Dentity);
            ObjTPhadvanceDetail.AdvanceDetailId = Convert.ToInt32(VAdvanceDetailID);

            string[] PEntity = { "PaymentId", "CashCounterId", "IsSelfOrcompany", "CompanyId", "StrId", "TranMode" };
            var Entity = ObjPaymentPharmacy.ToDictionary();
            foreach (var rProperty in PEntity)
            {
                Entity.Remove(rProperty);
            }
           odal.ExecuteNonQuery("PS_insert_I_PHPayment_1", CommandType.StoredProcedure, Entity);

        }


        public virtual async Task UpdateAsyncS(TPhadvanceHeader ObjTPhadvanceHeader, TPhadvanceDetail ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] Entity = { "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceUsedAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "StoreId" };
            var Uentity = ObjTPhadvanceHeader.ToDictionary();
            foreach (var rProperty in Entity)
            {
                Uentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_Update_T_PHAdvanceHeader", CommandType.StoredProcedure, Uentity);

            string[] DEntity = { "AdvanceNo" };
            var Dentity = ObjTPhadvanceDetail.ToDictionary();
            foreach (var rProperty in DEntity)
            {
                Dentity.Remove(rProperty);
            }
            string VAdvanceDetailID = odal.ExecuteNonQuery("m_insert_TPHAdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", Dentity);
            ObjTPhadvanceDetail.AdvanceDetailId = Convert.ToInt32(VAdvanceDetailID);

            string[] PEntity = { "PaymentId", "CashCounterId", "IsSelfOrcompany", "CompanyId", "StrId", "TranMode" };
            var PPEntity = ObjPaymentPharmacy.ToDictionary();
            foreach (var rProperty in PEntity)
            {
                PPEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_insert_I_PHPayment_1", CommandType.StoredProcedure, PPEntity);

        }
        public virtual async Task InsertAsyncR(TPhRefund ObjTPhRefund, TPhadvanceHeader ObjTPhadvanceHeader , List<AdvRefundDetail> ObjAdvRefundDetail , List<TPhadvanceDetail> ObjTPhadvanceDetail , PaymentPharmacy ObjPaymentPharmacy ,int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "CashCounterId", "IsRefundFlag", "RefundNo"};
            var entity = ObjTPhRefund.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string VRefundId = odal.ExecuteNonQuery("PS_insert_T_PhAdvRefund_1", CommandType.StoredProcedure, "RefundId", entity);
            ObjTPhRefund.RefundId = Convert.ToInt32(VRefundId);

            string[] AEntity = { "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "StoreId"};
            var Aentity = ObjTPhadvanceHeader.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                Aentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_PhAdvanceHeader_1", CommandType.StoredProcedure, Aentity);
            foreach (var item in ObjAdvRefundDetail)
            {

                string[] DEntity = { "AdvRefId" };
                var Dentity = item.ToDictionary();
                foreach (var rProperty in DEntity)
                {
                    Dentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_insert_T_PHAdvRefundDetail_1", CommandType.StoredProcedure, Dentity);
            }
            foreach (var item in ObjTPhadvanceDetail)
            {

                string[] PEntity = { "Date", "Time", "AdvanceId", "AdvanceNo", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "UsedAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "StoreId" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in PEntity)
                {
                    Pentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_update_T_PHAdvanceDetailBalAmount_1", CommandType.StoredProcedure, Pentity);
            }
            string[] PHEntity = { "PaymentId", "CashCounterId", "IsSelfOrcompany", "CompanyId", "StrId", "TranMode"};
            var Phentity = ObjPaymentPharmacy.ToDictionary();
            foreach (var rProperty in PHEntity)
            {
                Phentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("insert_I_PHPayment_1", CommandType.StoredProcedure, Phentity);
        }
        public virtual async Task InsertAsync( List<Payment> ObjPayment, List<TSalesHeader> ObjTSalesHeader,List<AdvanceDetail> ObjAdvanceDetail, AdvanceHeader ObjAdvanceHeader,int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();

            foreach (var item in ObjPayment)
            {
                string[] rEntity = { "Tdsamount", "ReceiptNo", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
                var entity = item.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                entity["OPDIPDType"] = 3;
                string PaymentId = odal.ExecuteNonQuery("m_insert_Payment_Pharmacy_New_1", CommandType.StoredProcedure, "PaymentId", entity);
            //    ObjPayment.PaymentId = Convert.ToInt32(PaymentId);
            }

            foreach (var item in ObjTSalesHeader)
            {
                string[] REntity = {  "Date","Time","SalesNo","OpIpId", "OpIpType","NetAmount","PaidAmount", "TotalAmount", "VatAmount", "DiscAmount", "ConcessionReasonId", "ConcessionAuthorizationId", "CashCounterId", "IsSellted", "IsPrint", "IsFree", "UnitId", "AddedBy",
                "ExternalPatientName", "DoctorName", "StoreId", "IsPrescription", "CreditReason", "CreditReasonId", "ExtRegNo", "WardId", "BedId", "DiscperH", "IsPurBill", "IsBillCheck", "IsRefundFlag", "RegId", "SalesHeadName", "SalesTypeId", "PatientName", "RegNo",
                "UpdatedBy", "ExtMobileNo", "RoundOff","ExtAddress","IsCancelled", "TSalesDetails" };
                var Tentity = item.ToDictionary();
                foreach (var rProperty in REntity)
                {
                    Tentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_update_Pharmacy_BillBalAmount_1", CommandType.StoredProcedure, Tentity);
            }
            foreach (var item in ObjAdvanceDetail)
            {

                string[] Entity = { "Date", "Time", "AdvanceId", "AdvanceNo", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "RefundAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason",  };
                var Ientity = item.ToDictionary();
                foreach (var rProperty in Entity)
                {
                    Ientity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_update_T_PHAdvanceDetail_1", CommandType.StoredProcedure, Ientity);
            }

            string[] TEntity = { "Date","RefId", "OpdIpdType", "OpdIpdId", "AdvanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate"};
            var Nentity = ObjAdvanceHeader.ToDictionary();
            foreach (var rProperty in TEntity)
            {
                Nentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_T_PHAdvanceHeader_1", CommandType.StoredProcedure, Nentity);

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

        public virtual async Task<IPagedList<Pharbillsettlementlist>> PharIPBillSettlement(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<Pharbillsettlementlist>(model, "m_Rtrv_Phar_Bill_List_Settlement");
        }
        public virtual async Task<IPagedList<BrowseIPPharAdvanceReceiptListDto>> BrowseIPPharAdvanceReceiptList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseIPPharAdvanceReceiptListDto>(model, "Rtrv_BrowseIPPharAdvanceReceipt");
        }
        public virtual async Task<IPagedList<PharAdvanceListDto>> PharAdvanceList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PharAdvanceListDto>(model, "m_Rtrv_Phar_AdvanceList");
        }
        public virtual async Task<IPagedList<PhAdvRefundReceiptListDto>> PhAdvRefundReceiptList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PhAdvRefundReceiptListDto>(model, "Rtrv_BrowseT_PhAdvRefundReceipt");
        }
        public virtual async Task<IPagedList<PhARefundOfAdvanceListDto>> PhARefundOfAdvanceList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PhARefundOfAdvanceListDto>(model, "m_Rtrv_Phar_RefundOfAdvance");
        }
        public virtual async Task<IPagedList<ItemNameBalanceQtyListDto>> BalqtysalesDraftlist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemNameBalanceQtyListDto>(model, "Retrieve_ItemName_BatchPOP_BalanceQty");
        }
    }
}
