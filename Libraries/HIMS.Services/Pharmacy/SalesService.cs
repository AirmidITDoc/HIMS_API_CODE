using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Pharmacy;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Security.Principal;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HIMS.Services.Users
{
    public class SalesService : ISalesService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public SalesService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
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
            return await DatabaseHelper.GetGridDataBySp<SalesDraftBillListDto>(model, "ps_m_Rtrv_SalesDraftBillList");
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
            return await DatabaseHelper.GetGridDataBySp<BrowseIPPharAdvanceReceiptListDto>(model, "ps_Rtrv_BrowseIPPharAdvanceReceipt");
        }
        public virtual async Task<IPagedList<PharAdvanceListDto>> PharAdvanceList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PharAdvanceListDto>(model, "ps_Rtrv_Phar_AdvanceList");
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
        public virtual async Task<IPagedList<ItemNameBalanceQtyListDtoKenya>> BalqtysalesDraftlistKenya(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemNameBalanceQtyListDtoKenya>(model, "ps_rtrv_ItemName_BatchPOP_BalanceQty_kenya");
        }
        public virtual async Task<IPagedList<GetRefundByAdvanceIdListDto>> GetRefundByAdvanceId(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<GetRefundByAdvanceIdListDto>(model, "sp_GetRefundByAdvanceId");
        }
        public virtual async Task<IPagedList<SalesDraftBillItemListDto>> SalesDraftBillItemDet(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SalesDraftBillItemListDto>(model, "ps_SalesDraftBillItemDet");
        }
        public virtual async Task<IPagedList<PrescriptionItemDetListDto>> PrescriptionItemDetList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PrescriptionItemDetListDto>(model, "ps_PrescriptionItemDet");
        }
        public virtual async Task<IPagedList<salespatientwiseListDto>> salespatientwiseList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<salespatientwiseListDto>(model, "ps_rptget_CreditAmount");
        }

        public virtual async Task<IPagedList<ItemGenericByNameListDto>> ItemGenericByNameList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ItemGenericByNameListDto>(model, "Retrieve_Item_Generic_ByName");
        }



        // Added by vimal on 05/09/2025
        public virtual async Task InsertAsyncSP(TSalesHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, PaymentPharmacy ObjPayment, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int UserId, string Username)
        {
            // Open transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                // 1️⃣ Insert Sales Header
                string[] rEntity = { "SalesNo", "CashCounterId", "ExtRegNo", "RefundAmt", "IsRefundFlag", "RegId", "PatientName", "RegNo", "UpdatedBy", "IsCancelled", "TSalesDetails" };
                var entity = ObjSalesHeader.ToDictionary();
                foreach (var rProperty in rEntity)
                    entity.Remove(rProperty);

                string SalesId = odal.ExecuteNonQueryNew("m_insert_Sales_1", CommandType.StoredProcedure, "SalesId", entity);
                ObjSalesHeader.SalesId = Convert.ToInt32(SalesId);
                ObjPayment.BillNo = ObjSalesHeader.SalesId;

                // 2️⃣ Insert Sales Details (EF)
                foreach (var item in ObjSalesHeader.TSalesDetails)
                    item.SalesId = ObjSalesHeader.SalesId;

                _context.TSalesDetails.AddRange(ObjSalesHeader.TSalesDetails);
                await _context.SaveChangesAsync(UserId, Username);

                // 3️⃣ Update Current Stock (SP)
                foreach (var item in ObjTCurrentStock)
                {
                    string[] Entity = { "StockId", "OpeningBalance", "ReceivedQty", "BalanceQty", "UnitMrp", "PurchaseRate", "LandedRate", "VatPercentage", "BatchNo", "BatchExpDate", "PurUnitRate", "PurUnitRateWf", "Cgstper", "Sgstper", "Igstper", "BarCodeSeqNo", "GrnRetQty", "IssDeptQty" };
                    var Ientity = item.ToDictionary();
                    foreach (var rProperty in Entity)
                        Ientity.Remove(rProperty);

                    odal.ExecuteNonQueryNew("m_Update_T_CurStk_Sales_Id_1", CommandType.StoredProcedure, "", Ientity);
                }

                var SalesIdObj = new { ObjSalesHeader.SalesId };
                odal.ExecuteNonQueryNew("m_Cal_DiscAmount_Sales", CommandType.StoredProcedure, "", SalesIdObj.ToDictionary());
                odal.ExecuteNonQueryNew("m_Cal_GSTAmount_Sales", CommandType.StoredProcedure, "", SalesIdObj.ToDictionary());

                // 4️⃣ Insert Payment
                string[] PEntity = { "StrId", "ReceiptNo", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
                var Sentity = ObjPayment.ToDictionary();
                foreach (var rProperty in PEntity)
                    Sentity.Remove(rProperty);

                string PaymentId = odal.ExecuteNonQueryNew("insert_Payment_Pharmacy_New_1", CommandType.StoredProcedure, "PaymentId", Sentity);
                ObjPayment.PaymentId = Convert.ToInt32(PaymentId);

                // 5️⃣ Update Prescription
                string[] TEntity = { "IppreId", "IpmedId", "IPMedID", "OpdIpdType", "Pdate", "Ptime", "ClassId", "GenericId", "DrugId", "DoseId", "Days", "QtyPerDay", "TotalQty", "Remark", "IsAddBy", "StoreId", "WardId", "GrnRetQty", "IssDeptQty", "Ipmed" };
                var Nentity = ObjPrescription.ToDictionary();
                foreach (var rProperty in TEntity)
                    Nentity.Remove(rProperty);

                odal.ExecuteNonQueryNew("m_Update_T_IPPrescription_Isclosed_Status_1", CommandType.StoredProcedure, "", Nentity);

                // 6️⃣ Update Draft Header
                string[] DEntity = { "Date","Time", "SalesNo", "OpIpId", "OpIpType", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "PaidAmount", "BalanceAmount", "ConcessionReasonId", "ConcessionAuthorizationId", "CashCounterId",
                                      "IsSellted", "IsPrint", "UnitId", "AddedBy", "UpdatedBy" ,"ExternalPatientName","DoctorName","StoreId","CreditReason","CreditReasonId","IsCancelled","IsPrescription","WardId","BedId","ExtMobileNo","ExtAddress"};
                var Hentity = ObjDraftHeader.ToDictionary();
                foreach (var rProperty in DEntity)
                    Hentity.Remove(rProperty);

                odal.ExecuteNonQueryNew("m_Update_T_SalDraHeader_IsClosed_1", CommandType.StoredProcedure, "", Hentity);

                //Commit if all good
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                //Rollback on error
                await transaction.RollbackAsync();
                throw;
            }
        }
        public virtual async Task InsertSalesSaveWithPayment(TSalesHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, PaymentPharmacy ObjPayment, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, List<TPaymentPharmacy> ObjTPaymentPharmacy, int CurrentUserId, string CurrentUserName)
        {
            // Open transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                // 1️⃣ Insert Sales Header
                string[] AEntity = { "Date", "Time", "OpIpId", "OpIpType", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "PaidAmount", "BalanceAmount", "ConcessionReasonId", "ConcessionAuthorizationId", "IsSellted", "IsPrint", "IsFree", "UnitId", "ExternalPatientName", "DoctorName", "StoreId", "IsPrescription", "AddedBy", "CreditReason", "CreditReasonId", "WardId", "BedId", "DiscperH", "IsPurBill", "IsBillCheck", "SalesHeadName", "SalesTypeId", "RegId", "ExtMobileNo", "RoundOff", "ExtAddress", "SalesId" /*TSalesDetails*/ };
                var entity = ObjSalesHeader.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                string SalesId = odal.ExecuteNonQueryNew("ps_m_insert_Sales_1", CommandType.StoredProcedure, "SalesId", entity);
                ObjSalesHeader.SalesId = Convert.ToInt32(SalesId);
                ObjPayment.BillNo = ObjSalesHeader.SalesId;
                await _context.LogProcedureExecution(entity, nameof(TSalesHeader), ObjSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


                // 2️⃣ Insert Sales Details (EF)
                foreach (var item in ObjSalesHeader.TSalesDetails)
                    item.SalesId = ObjSalesHeader.SalesId;

                _context.TSalesDetails.AddRange(ObjSalesHeader.TSalesDetails);
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);


                // 3️⃣ Update Current Stock (SP)
                foreach (var items in ObjTCurrentStock)
                {
                    string[] SEntity = { "ItemId", "IssueQty", "IstkId", "StoreId" };
                    var IIentity = items.ToDictionary();
                    foreach (var rProperty in IIentity.Keys.ToList())
                    {
                        if (!SEntity.Contains(rProperty))
                            IIentity.Remove(rProperty);
                    }

                    odal.ExecuteNonQueryNew("ps_Update_T_CurStk_Sales_Id_1", CommandType.StoredProcedure, "", IIentity);
                    await _context.LogProcedureExecution(IIentity, nameof(TCurrentStock), items.StockId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

                }

                var SalesIdObj = new { ObjSalesHeader.SalesId };
                odal.ExecuteNonQueryNew("ps_Cal_DiscAmount_Sales", CommandType.StoredProcedure, "", SalesIdObj.ToDictionary());
                odal.ExecuteNonQueryNew("ps_Cal_GSTAmount_Sales", CommandType.StoredProcedure, "", SalesIdObj.ToDictionary());

                // 4️⃣ Insert Payment
                string[] PEntity = { "PaymentId", "BillNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "Opdipdtype", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "UnitId" };
                var SSentity = ObjPayment.ToDictionary();
                foreach (var rProperty in SSentity.Keys.ToList())
                {
                    if (!PEntity.Contains(rProperty))
                        SSentity.Remove(rProperty);
                }
                string PaymentId = odal.ExecuteNonQueryNew("ps_insert_Payment_Pharmacy_New_1", CommandType.StoredProcedure, "PaymentId", SSentity);
                ObjPayment.PaymentId = Convert.ToInt32(PaymentId);
                await _context.LogProcedureExecution(entity, nameof(PaymentPharmacy), ObjPayment.PaymentId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


                // 5️⃣ Update Prescription
                string[] TEntity = { "OpIpId", "IsClosed" };
                var Nentity = ObjPrescription.ToDictionary();
                foreach (var rProperty in Nentity.Keys.ToList())
                {
                    if (!TEntity.Contains(rProperty))
                        Nentity.Remove(rProperty);
                }

                odal.ExecuteNonQueryNew("ps_Update_T_IPPrescription_Isclosed_Status_1", CommandType.StoredProcedure, "", Nentity);
                await _context.LogProcedureExecution(entity, nameof(TIpPrescription), ObjPrescription.IppreId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


                // 6️⃣ Update Draft Header
                string[] DEntity = { "DsalesId", "IsClosed" };
                var Hentity = ObjDraftHeader.ToDictionary();
                foreach (var rProperty in Hentity.Keys.ToList())
                {
                    if (!DEntity.Contains(rProperty))
                        Hentity.Remove(rProperty);
                }
                odal.ExecuteNonQueryNew("ps_Update_T_SalDraHeader_IsClosed_1", CommandType.StoredProcedure, "", Hentity);
                await _context.LogProcedureExecution(Hentity, nameof(TSalesDraftHeader), ObjDraftHeader.DsalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                // 4️⃣ Insert Payment
                foreach (var item in ObjTPaymentPharmacy)
                {
                    string[] Entity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                                           "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};


                    TPaymentPharmacy objTPay = new();
                    objTPay = item;
                    objTPay.BillNo = ObjPayment.BillNo;

                    var pentity = item.ToDictionary();
                    foreach (var rProperty in pentity.Keys.ToList())
                    {
                        if (!Entity.Contains(rProperty))
                            pentity.Remove(rProperty);
                    }
                    string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_PaymentPharmacy", CommandType.StoredProcedure, "PaymentId", pentity);
                    item.PaymentId = Convert.ToInt32(VPaymentId);
                }


                //Commit if all good
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                //Rollback on error
                await transaction.RollbackAsync();
                throw;
            }
        }

        public virtual async Task InsertAsyncSPC(TSalesHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int CurrentUserId, string CurrentUserName, CancellationToken cancellationToken = default)
        {
            // Whitelists declared once
            var headerWhitelist = new HashSet<string>(StringComparer.Ordinal)
            {
            "Date","Time","OpIpId","OpIpType","TotalAmount","VatAmount","DiscAmount","NetAmount",
            "PaidAmount","BalanceAmount","ConcessionReasonId","ConcessionAuthorizationId","IsSellted",
            "IsPrint","IsFree","UnitId","ExternalPatientName","DoctorName","StoreId","IsPrescription",
            "AddedBy","CreditReason","CreditReasonId","WardId","BedId","DiscperH","IsPurBill",
            "IsBillCheck","SalesHeadName","SalesTypeId","RegId","ExtMobileNo","RoundOff","ExtAddress","SalesId"
            };

            var stockWhitelist = new HashSet<string>(StringComparer.Ordinal) { "ItemId", "IssueQty", "IstkId", "StoreId" };
            var prescriptionWhitelist = new HashSet<string>(StringComparer.Ordinal) { "OpIpId", "IsClosed" };
            var draftWhitelist = new HashSet<string>(StringComparer.Ordinal) { "DsalesId", "IsClosed" };

            // Manage connection lifecycle explicitly
            var dbConnection = _context.Database.GetDbConnection();
            var wasClosed = dbConnection.State != ConnectionState.Open;

            if (wasClosed)
                await dbConnection.OpenAsync(cancellationToken);

            using var transaction = await _context.Database.BeginTransactionAsync(
                System.Data.IsolationLevel.ReadCommitted, cancellationToken);

            try
            {
                var odal = new DatabaseHelper();
                odal.SetConnection(dbConnection);
                odal.SetTransaction(transaction.GetDbTransaction());

                // 1) Insert Sales Header
                var headerDict = ObjSalesHeader.ToDictionary();
                FilterDict(headerDict, headerWhitelist);

                string salesIdStr = odal.ExecuteNonQueryNew("ps_m_insert_Sales_1", CommandType.StoredProcedure, "SalesId", headerDict);

                ObjSalesHeader.SalesId = Convert.ToInt32(salesIdStr);

                await _context.LogProcedureExecution(headerDict, nameof(TSalesHeader), ObjSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                // 2) Insert Sales Details (EF) — single SaveChanges
                if (ObjSalesHeader.TSalesDetails?.Count > 0)
                {
                    foreach (var item in ObjSalesHeader.TSalesDetails)
                        item.SalesId = ObjSalesHeader.SalesId;

                    _context.TSalesDetails.AddRange(ObjSalesHeader.TSalesDetails);
                    await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                }

                // 3) Update Current Stock (SP per row)
                if (ObjTCurrentStock?.Count > 0)
                {
                    List<Dictionary<string, object>> stockDicts = new();
                    foreach (var stockItem in ObjTCurrentStock)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var stockDict = stockItem.ToDictionary();
                        FilterDict(stockDict, stockWhitelist);

                        odal.ExecuteNonQueryNew("ps_Update_T_CurStk_Sales_Id_1", CommandType.StoredProcedure, null, stockDict);
                        stockDicts.Add(stockDict);
                    }
                    // FIXED: Log the stock dict, not the header dict
                    if (stockDicts.Count > 0)
                        await _context.LogProcedureExecution(stockDicts, nameof(TCurrentStock), ObjSalesHeader.SalesId, Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                }

                // 4) Discount + GST recalculation (build dict ONCE per call — ExecuteNonQueryNew may mutate)
                odal.ExecuteNonQueryNew("m_Cal_DiscAmount_Sales", CommandType.StoredProcedure, null, new Dictionary<string, object> { ["SalesId"] = ObjSalesHeader.SalesId });

                odal.ExecuteNonQueryNew("m_Cal_GSTAmount_Sales", CommandType.StoredProcedure, null, new Dictionary<string, object> { ["SalesId"] = ObjSalesHeader.SalesId });

                // 5) Update Prescription
                var prescriptionDict = ObjPrescription.ToDictionary();
                FilterDict(prescriptionDict, prescriptionWhitelist);

                odal.ExecuteNonQueryNew("ps_Update_T_IPPrescription_Isclosed_Status_1", CommandType.StoredProcedure, null, prescriptionDict);

                // FIXED: Log the prescription dict
                await _context.LogProcedureExecution(prescriptionDict, nameof(TIpPrescription), ObjSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

                // 6) Update Draft Header
                var draftDict = ObjDraftHeader.ToDictionary();
                FilterDict(draftDict, draftWhitelist);

                odal.ExecuteNonQueryNew("ps_Update_T_SalDraHeader_IsClosed_1", CommandType.StoredProcedure, null, draftDict);

                // FIXED: Log the draft dict
                await _context.LogProcedureExecution(draftDict, nameof(TSalesDraftHeader), ObjSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

                // Commit
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                try { await transaction.RollbackAsync(CancellationToken.None); } catch { /* swallow rollback errors */ }
                throw;
            }
            finally
            {
                if (wasClosed && dbConnection.State == ConnectionState.Open)
                    await dbConnection.CloseAsync();
            }

            static void FilterDict(Dictionary<string, object> dict, HashSet<string> allowed)
            {
                foreach (var key in dict.Keys.ToList())
                    if (!allowed.Contains(key))
                        dict.Remove(key);
            }
        }

        //public virtual async Task InsertAsyncSPC(TSalesHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int CurrentUserId, string CurrentUserName)
        //{
        //    // Open transaction
        //    using var transaction = await _context.Database.BeginTransactionAsync();
        //    try
        //    {
        //        DatabaseHelper odal = new();
        //        odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
        //        odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

        //        // 1️⃣ Insert Sales Header
        //        string[] AEntity = { "Date", "Time", "OpIpId", "OpIpType", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "PaidAmount", "BalanceAmount", "ConcessionReasonId", "ConcessionAuthorizationId", "IsSellted", "IsPrint", "IsFree", "UnitId", "ExternalPatientName", "DoctorName", "StoreId", "IsPrescription", "AddedBy", "CreditReason", "CreditReasonId", "WardId", "BedId", "DiscperH", "IsPurBill", "IsBillCheck", "SalesHeadName", "SalesTypeId", "RegId", "ExtMobileNo", "RoundOff", "ExtAddress", "SalesId" /*TSalesDetails*/ };
        //        var entity = ObjSalesHeader.ToDictionary();

        //        foreach (var rProperty in entity.Keys.ToList())
        //        {
        //            if (!AEntity.Contains(rProperty))
        //                entity.Remove(rProperty);
        //        }

        //        string SalesId = odal.ExecuteNonQueryNew("ps_m_insert_Sales_1", CommandType.StoredProcedure, "SalesId", entity);
        //        ObjSalesHeader.SalesId = Convert.ToInt32(SalesId);
        //        await _context.LogProcedureExecution(entity, nameof(TSalesHeader), ObjSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


        //        // 2️⃣ Insert Sales Details (EF)
        //        foreach (var item in ObjSalesHeader.TSalesDetails)
        //            item.SalesId = ObjSalesHeader.SalesId;

        //        _context.TSalesDetails.AddRange(ObjSalesHeader.TSalesDetails);
        //        await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);


        //        // 3️⃣ Update Current Stock (SP)
        //        foreach (var items in ObjTCurrentStock)
        //        {
        //            string[] SEntity = { "ItemId", "IssueQty", "IstkId", "StoreId" };
        //            var IIentity = items.ToDictionary();
        //            foreach (var rProperty in IIentity.Keys.ToList())
        //            {
        //                if (!SEntity.Contains(rProperty))
        //                    IIentity.Remove(rProperty);
        //            }

        //            odal.ExecuteNonQueryNew("ps_Update_T_CurStk_Sales_Id_1", CommandType.StoredProcedure, "", IIentity);
        //            await _context.LogProcedureExecution(entity, nameof(TSalesHeader), ObjSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

        //        }

        //        var SalesIdObj = new { ObjSalesHeader.SalesId };
        //        odal.ExecuteNonQueryNew("m_Cal_DiscAmount_Sales", CommandType.StoredProcedure, "", SalesIdObj.ToDictionary());
        //        odal.ExecuteNonQueryNew("m_Cal_GSTAmount_Sales", CommandType.StoredProcedure, "", SalesIdObj.ToDictionary());

        //        // 5️⃣ Update Prescription
        //        string[] TEntity = { "OpIpId", "IsClosed" };
        //        var Nentity = ObjPrescription.ToDictionary();
        //        foreach (var rProperty in Nentity.Keys.ToList())
        //        {
        //            if (!TEntity.Contains(rProperty))
        //                Nentity.Remove(rProperty);
        //        }

        //        odal.ExecuteNonQueryNew("ps_Update_T_IPPrescription_Isclosed_Status_1", CommandType.StoredProcedure, "", Nentity);
        //        await _context.LogProcedureExecution(entity, nameof(TSalesHeader), ObjSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


        //        // 6️⃣ Update Draft Header
        //        string[] DEntity = { "DsalesId", "IsClosed" };
        //        var Hentity = ObjDraftHeader.ToDictionary();
        //        foreach (var rProperty in Hentity.Keys.ToList())
        //        {
        //            if (!DEntity.Contains(rProperty))
        //                Hentity.Remove(rProperty);
        //        }
        //        odal.ExecuteNonQueryNew("ps_Update_T_SalDraHeader_IsClosed_1", CommandType.StoredProcedure, "", Hentity);
        //        await _context.LogProcedureExecution(entity, nameof(TSalesHeader), ObjSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


        //        //Commit if all good
        //        await transaction.CommitAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        //Rollback on error
        //        await transaction.RollbackAsync();
        //        throw;
        //    }
        //}

        public virtual async Task InsertSalesInPatientAsyncSPC(TSalesInpatientHeader ObjSalesHeader, List<TCurrentStock> ObjTCurrentStock, TIpPrescription ObjPrescription, TSalesDraftHeader ObjDraftHeader, int CurrentUserId, string CurrentUserName)
        {
            // Open transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                // 1️⃣ Insert Sales Header
                string[] AEntity = { "Date", "Time", "OpIpId", "OpIpType", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "PaidAmount", "BalanceAmount", "ConcessionReasonId", "ConcessionAuthorizationId", "IsSellted", "IsPrint", "IsFree", "UnitId",
                    "ExternalPatientName", "DoctorName", "StoreId", "IsPrescription", "AddedBy", "CreditReason", "CreditReasonId", "WardId", "BedId", "DiscperH", "IsPurBill", "IsBillCheck", "SalesHeadName", "SalesTypeId", "RegId", "ExtMobileNo", "RoundOff",
                    "ExtAddress", "SalesId" /*TSalesDetails*/ };
                var entity = ObjSalesHeader.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                string SalesId = odal.ExecuteNonQueryNew("ps_insert_T_SalesInpatientHeader_1", CommandType.StoredProcedure, "SalesId", entity);
                ObjSalesHeader.SalesId = Convert.ToInt32(SalesId);
                await _context.LogProcedureExecution(entity, nameof(TSalesInpatientHeader), ObjSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


                // 2️⃣ Insert Sales Details (EF)
                foreach (var item in ObjSalesHeader.TSalesInpatientDetails)
                    item.SalesId = ObjSalesHeader.SalesId;

                _context.TSalesInpatientDetails.AddRange(ObjSalesHeader.TSalesInpatientDetails);
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);


                // 3️⃣ Update Current Stock (SP)
                foreach (var items in ObjTCurrentStock)
                {
                    string[] SEntity = { "ItemId", "IssueQty", "IstkId", "StoreId" };
                    var IIentity = items.ToDictionary();
                    foreach (var rProperty in IIentity.Keys.ToList())
                    {
                        if (!SEntity.Contains(rProperty))
                            IIentity.Remove(rProperty);
                    }

                    odal.ExecuteNonQueryNew("ps_Update_T_CurStk_Sales_Id_1", CommandType.StoredProcedure, "", IIentity);
                    await _context.LogProcedureExecution(entity, nameof(TSalesInpatientHeader), ObjSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                }

                var SalesIdObj = new { ObjSalesHeader.SalesId };
                odal.ExecuteNonQueryNew("ps_Cal_DiscAmount_SalesInpatientHeader", CommandType.StoredProcedure, "", SalesIdObj.ToDictionary());
                odal.ExecuteNonQueryNew("ps_Cal_GSTAmount_SalesInpatientHeader", CommandType.StoredProcedure, "", SalesIdObj.ToDictionary());

                // 5️⃣ Update Prescription
                string[] TEntity = { "OpIpId", "IsClosed" };
                var Nentity = ObjPrescription.ToDictionary();
                foreach (var rProperty in Nentity.Keys.ToList())
                {
                    if (!TEntity.Contains(rProperty))
                        Nentity.Remove(rProperty);
                }

                odal.ExecuteNonQueryNew("ps_Update_T_IPPrescription_Isclosed_Status_1", CommandType.StoredProcedure, "", Nentity);
                await _context.LogProcedureExecution(entity, nameof(TSalesInpatientHeader), ObjSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


                // 6️⃣ Update Draft Header
                string[] DEntity = { "DsalesId", "IsClosed" };
                var Hentity = ObjDraftHeader.ToDictionary();
                foreach (var rProperty in Hentity.Keys.ToList())
                {
                    if (!DEntity.Contains(rProperty))
                        Hentity.Remove(rProperty);
                }
                odal.ExecuteNonQueryNew("ps_Update_T_SalDraHeader_IsClosed_1", CommandType.StoredProcedure, "", Hentity);
                await _context.LogProcedureExecution(entity, nameof(TSalesInpatientHeader), ObjSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

                //var SalesReturnObj = new
                //{
                //    Id = Convert.ToInt32(ObjSalesHeader.SalesId),
                //    TypeId = 1

                //};
                //odal.ExecuteNonQuery("ps_Insert_ItemMovementReport_InpatientIssueCursor", CommandType.StoredProcedure, SalesReturnObj.ToDictionary());

                //Commit if all good
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                //Rollback on error
                await transaction.RollbackAsync();
                throw;
            }
        }

        public virtual async Task InsertSD(TSalesDraftHeader ObjDraftHeader, List<TSalesDraftDet> ObjTSalesDraftDet, int CurrentUserId, string CurrentUserName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                string[] rEntity = { "DsalesId", "Date",  "Time", "OpIpId", "OpIpType", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "PaidAmount", "BalanceAmount","ConcessionReasonId", "ConcessionAuthorizationId", "IsSellted",
                    "IsPrint","UnitId","AddedBy","ExternalPatientName","DoctorName","StoreId","CreditReason","CreditReasonId","IsClosed","IsPrescription","WardId","BedId","ExtMobileNo", "ExtAddress"};
                var entity = ObjDraftHeader.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                string DsalesId = odal.ExecuteNonQuery("m_insert_T_SalesDraftHeader_1", CommandType.StoredProcedure, "DsalesId", entity);
                ObjDraftHeader.DsalesId = Convert.ToInt32(DsalesId);
                await _context.LogProcedureExecution(entity, nameof(TSalesDraftHeader), ObjDraftHeader.DsalesId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


                foreach (var item in ObjTSalesDraftDet)
                {
                    item.DsalesId = Convert.ToInt32(DsalesId);
                    string[] PEntity = { "DsalesId", "ItemId", "BatchNo", "BatchExpDate", "UnitMrp", "Qty", "TotalAmount", "VatPer", "VatAmount", "DiscPer", "DiscAmount", "GrossAmount", "LandedPrice", "TotalLandedAmount", "PurRateWf", "PurTotAmt" };

                    var Tentity = item.ToDictionary();
                    foreach (var rProperty in Tentity.Keys.ToList())
                    {
                        if (!PEntity.Contains(rProperty))
                            Tentity.Remove(rProperty);
                    }
                    odal.ExecuteNonQuery("insert_T_SalesDraftDet_1", CommandType.StoredProcedure, Tentity);
                    await _context.LogProcedureExecution(Tentity, nameof(TSalesDraftDet), item.SalDetId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                }
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

                // ✅ Commit
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // ❌ Rollback
                await transaction.RollbackAsync();
                throw;
            }
        }

        //public virtual async Task UpdateAsyncSalesDraft(TSalesDraftHeader ObjDraftHeader, List<TSalesDraftDet> ObjTSalesDraftDet, int CurrentUserId, string CurrentUserName)
        //{

        //    DatabaseHelper odal = new();
        //    var tokensObj = new
        //    {
        //        DsalesId = Convert.ToInt32(ObjDraftHeader.DsalesId)
        //    };
        //    odal.ExecuteNonQuery("Delete_SalesDraftBill", CommandType.StoredProcedure, tokensObj.ToDictionary());

        //    string[] DEntity = { "DsalesId", "Date",  "Time", "OpIpId", "OpIpType", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "PaidAmount", "BalanceAmount","ConcessionReasonId", "ConcessionAuthorizationId", "IsSellted",
        //            "IsPrint","UnitId","AddedBy","ExternalPatientName","DoctorName","StoreId","CreditReason","CreditReasonId","IsClosed","IsPrescription","WardId","BedId","ExtMobileNo", "ExtAddress"};
        //    var bentity = ObjDraftHeader.ToDictionary();
        //    foreach (var rProperty in bentity.Keys.ToList())
        //    {
        //        if (!DEntity.Contains(rProperty))
        //            bentity.Remove(rProperty);
        //    }
        //    odal.ExecuteNonQuery("ps_Update_TSalesDraftHeader", CommandType.StoredProcedure, bentity);
        //    await _context.LogProcedureExecution(bentity, nameof(TSalesDraftHeader), ObjDraftHeader.DsalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


        //    foreach (var item in ObjTSalesDraftDet)
        //    {
        //        //item.DsalesId = Convert.ToInt32(DsalesId);

        //        string[] CREntity = { "DsalesId", "ItemId", "BatchNo", "BatchExpDate", "UnitMrp", "Qty", "TotalAmount", "VatPer", "VatAmount", "DiscPer", "DiscAmount", "GrossAmount", "LandedPrice", "TotalLandedAmount", "PurRateWf", "PurTotAmt" };
        //        var centity = item.ToDictionary();
        //        foreach (var rProperty in centity.Keys.ToList())
        //        {
        //            if (!CREntity.Contains(rProperty))
        //                centity.Remove(rProperty);
        //        }
        //        odal.ExecuteNonQuery("insert_T_SalesDraftDet_1", CommandType.StoredProcedure, centity);
        //        await _context.LogProcedureExecution(centity, nameof(TSalesDraftDet), item.SalDetId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);



        //    }
        //}



        public virtual async Task Delete(TSalesDraftHeader ObjDraftHeader, int CurrentUserId, string CurrentUserName)
        {
            // Begin Transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
                string[] rEntity = { "DsalesId" };
                var entity = ObjDraftHeader.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Update_SalesDraftHeader", CommandType.StoredProcedure, entity);
                // Audit Log
                await _context.LogProcedureExecution(entity, nameof(TSalesDraftHeader), ObjDraftHeader.DsalesId.ToInt(), Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);
                // Save Log
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                // Commit
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // Rollback
                await transaction.RollbackAsync();
                throw;
            }
        }


        //shilpa//26/05/2025
        public virtual void InsertS(TPhadvanceHeader ObjTPhadvanceHeader, TPhadvanceDetail ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, List<TPaymentPharmacy> ObjTPaymentPharmacy, int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "StoreId", "UnitId", "AdvanceId", "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceAmount", "AdvanceUsedAmount", "BalanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate" };
            var entity = ObjTPhadvanceHeader.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string VAdvanceId = odal.ExecuteNonQuery("PS_insert_T_PHAdvanceHeader_1", CommandType.StoredProcedure, "AdvanceId", entity);
            ObjTPhadvanceHeader.AdvanceId = Convert.ToInt32(VAdvanceId);
            ObjTPhadvanceDetail.AdvanceId = Convert.ToInt32(VAdvanceId);




            string[] DEntity = { "Date", "Time", "UnitId", "AdvanceId", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "UsedAmount", "BalanceAmount", "RefundAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "StoreId", "AdvanceDetailId" };
            var Dentity = ObjTPhadvanceDetail.ToDictionary();
            foreach (var rProperty in Dentity.Keys.ToList())
            {
                if (!DEntity.Contains(rProperty))
                    Dentity.Remove(rProperty);
            }
            string VAdvanceDetailID = odal.ExecuteNonQuery("ps_m_insert_TPHAdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", Dentity);
            ObjTPhadvanceDetail.AdvanceDetailId = Convert.ToInt32(VAdvanceDetailID);
            ObjPaymentPharmacy.AdvanceId = Convert.ToInt32(VAdvanceDetailID);


            string[] PEntity = { "BillNo", "UnitId", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Opdipdtype", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount" };
            var Entity = ObjPaymentPharmacy.ToDictionary();
            foreach (var rProperty in Entity.Keys.ToList())
            {
                if (!PEntity.Contains(rProperty))
                    Entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_insert_I_PHPayment_1", CommandType.StoredProcedure, Entity);

            foreach (var item in ObjTPaymentPharmacy)
            {
                item.AdvanceId = Convert.ToInt32(VAdvanceDetailID);

                string[] SEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                                           "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};


                //TPaymentPharmacy objTPay = new();
                //objTPay = item;
                //objTPay.BillNo = ObjPayment.BillNo;

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


        public virtual void UpdateS(TPhadvanceHeader ObjTPhadvanceHeader, TPhadvanceDetail ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, List<TPaymentPharmacy> ObjTPaymentPharmacy, int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] Entity = { "AdvanceAmount", "BalanceAmount", "AdvanceId" };
            var Uentity = ObjTPhadvanceHeader.ToDictionary();
            foreach (var rProperty in Uentity.Keys.ToList())
            {
                if (!Entity.Contains(rProperty))
                    Uentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_m_Update_T_PHAdvanceHeader", CommandType.StoredProcedure, Uentity);

            string[] DEntity = { "Date", "Time", "UnitId", "AdvanceId", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "UsedAmount", "BalanceAmount", "RefundAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "StoreId", "AdvanceDetailId" };
            var Dentity = ObjTPhadvanceDetail.ToDictionary();
            foreach (var rProperty in Dentity.Keys.ToList())
            {
                if (!DEntity.Contains(rProperty))
                    Dentity.Remove(rProperty);
            }
            string VAdvanceDetailID = odal.ExecuteNonQuery("ps_m_insert_TPHAdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", Dentity);
            ObjTPhadvanceDetail.AdvanceDetailId = Convert.ToInt32(VAdvanceDetailID);
            ObjPaymentPharmacy.AdvanceId = Convert.ToInt32(VAdvanceDetailID);



            string[] PEntity = { "BillNo", "UnitId", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Opdipdtype", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount" };
            var PPEntity = ObjPaymentPharmacy.ToDictionary();
            foreach (var rProperty in PPEntity.Keys.ToList())
            {
                if (!PEntity.Contains(rProperty))
                    PPEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_insert_I_PHPayment_1", CommandType.StoredProcedure, PPEntity);

            foreach (var item in ObjTPaymentPharmacy)
            {
                item.AdvanceId = Convert.ToInt32(VAdvanceDetailID);


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
        public virtual void InsertR(TPhRefund ObjTPhRefund, TPhadvanceHeader ObjTPhadvanceHeader, List<TPhadvRefundDetail> ObjTPhadvRefundDetail, List<TPhadvanceDetail> ObjTPhadvanceDetail, PaymentPharmacy ObjPaymentPharmacy, List<TPaymentPharmacy> ObjTPaymentPharmacy, int UserId, string Username)
        {

            // //Add header table records
            DatabaseHelper odal = new();
            string[] rEntity = { "RefundDate", "RefundTime", "BillId", "AdvanceId", "OpdIpdType", "OpdIpdId", "RefundAmount", "Remark", "TransactionId", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "StrId", "RefundId" };
            var entity = ObjTPhRefund.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string VRefundId = odal.ExecuteNonQuery("PS_insert_T_PhAdvRefund_1", CommandType.StoredProcedure, "RefundId", entity);
            ObjTPhRefund.RefundId = Convert.ToInt32(VRefundId);
            ObjPaymentPharmacy.RefundId = Convert.ToInt32(VRefundId);

            string[] AEntity = { "AdvanceId", "AdvanceUsedAmount", "BalanceAmount" };
            var Aentity = ObjTPhadvanceHeader.ToDictionary();
            foreach (var rProperty in Aentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Aentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_update_PhAdvanceHeader_1", CommandType.StoredProcedure, Aentity);

            foreach (var item in ObjTPhadvRefundDetail)
            {

                string[] DEntity = { "AdvDetailId", "RefundDate", "RefundTime", "AdvRefundAmt" };
                var Dentity = item.ToDictionary();
                foreach (var rProperty in Dentity.Keys.ToList())
                {
                    if (!DEntity.Contains(rProperty))
                        Dentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_insert_T_PHAdvRefundDetail_1", CommandType.StoredProcedure, Dentity);
            }

            foreach (var item in ObjTPhadvanceDetail)
            {

                string[] PEntity = { "AdvanceDetailId", "BalanceAmount", "RefundAmount" };
                var Pentity = item.ToDictionary();
                foreach (var rProperty in Pentity.Keys.ToList())
                {
                    if (!PEntity.Contains(rProperty))
                        Pentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_update_T_PHAdvanceDetailBalAmount_1", CommandType.StoredProcedure, Pentity);
            }
            string[] PHEntity = { "BillNo", "UnitId", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType",
                "Opdipdtype", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount" };
            var Entity = ObjPaymentPharmacy.ToDictionary();
            foreach (var rProperty in Entity.Keys.ToList())
            {
                if (!PHEntity.Contains(rProperty))
                    Entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("insert_I_PHPayment_1", CommandType.StoredProcedure, Entity);

            foreach (var item in ObjTPaymentPharmacy)
            {
                item.RefundId = Convert.ToInt32(VRefundId);


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
        public virtual async Task Insert(List<PaymentPharmacy> ObjPayment, List<TSalesHeader> ObjTSalesHeader, List<AdvanceDetail> ObjAdvanceDetail, AdvanceHeader ObjAdvanceHeader, List<TPaymentPharmacy> ObjTPaymentPharmacy, int CurrentUserId, string CurrentUserName)

        {
            // Begin Transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
                foreach (var item in ObjPayment)
                {
                    string[] rEntity = { "PaymentId", "UnitId", "BillNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "Opdipdtype", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "Tdsamount", "Wfamount", "PayTmdate" };
                    var entity = item.ToDictionary();
                    foreach (var rProperty in entity.Keys.ToList())
                    {
                        if (!rEntity.Contains(rProperty))
                            entity.Remove(rProperty);
                    }
                    string PaymentId = odal.ExecuteNonQuery("insert_Payment_Pharmacy_New_1", CommandType.StoredProcedure, "PaymentId", entity);
                    //    ObjPayment.PaymentId = Convert.ToInt32(PaymentId);
                    await _context.LogProcedureExecution(entity, nameof(PaymentPharmacy), item.PaymentId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                }

                foreach (var item in ObjTSalesHeader)
                {
                    string[] REntity = { "SalesId", "BalanceAmount", "RefundAmt" };
                    var Tentity = item.ToDictionary();
                    foreach (var rProperty in Tentity.Keys.ToList())
                    {
                        if (!REntity.Contains(rProperty))
                            Tentity.Remove(rProperty);
                    }
                    odal.ExecuteNonQuery("m_update_Pharmacy_BillBalAmount_1", CommandType.StoredProcedure, Tentity);
                    await _context.LogProcedureExecution(Tentity, nameof(TSalesHeader), item.SalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                }

                foreach (var item in ObjAdvanceDetail)
                {

                    string[] Entity = { "AdvanceDetailId", "UsedAmount", "BalanceAmount" };
                    var Ientity = item.ToDictionary();
                    foreach (var rProperty in Ientity.Keys.ToList())
                    {
                        if (!Entity.Contains(rProperty))
                            Ientity.Remove(rProperty);
                    }
                    odal.ExecuteNonQuery("m_update_T_PHAdvanceDetail_1", CommandType.StoredProcedure, Ientity);
                    await _context.LogProcedureExecution(Ientity, nameof(AdvanceDetail), item.AdvanceDetailId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                }

                string[] TEntity = { "AdvanceId", "AdvanceUsedAmount", "BalanceAmount" };
                var Nentity = ObjAdvanceHeader.ToDictionary();
                foreach (var rProperty in Nentity.Keys.ToList())
                {
                    if (!TEntity.Contains(rProperty))
                        Nentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("m_update_T_PHAdvanceHeader_1", CommandType.StoredProcedure, Nentity);
                // Audit Log
                await _context.LogProcedureExecution(Nentity, nameof(AdvanceHeader), ObjAdvanceHeader.AdvanceId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

                foreach (var item in ObjTPaymentPharmacy)
                {
                    string[] Entity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                    "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};

                    var pentity = item.ToDictionary();
                    foreach (var rProperty in pentity.Keys.ToList())
                    {
                        if (!Entity.Contains(rProperty))
                            pentity.Remove(rProperty);
                    }
                    string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_PaymentPharmacy", CommandType.StoredProcedure, "PaymentId", pentity);
                    item.PaymentId = Convert.ToInt32(VPaymentId);
                    // Audit Log
                    await _context.LogProcedureExecution(pentity, nameof(TPaymentPharmacy), item.PaymentId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                }
                // Save Logs
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);


                // Commit Transaction
                await transaction.CommitAsync();
            }


            catch (Exception)
            {
                // Rollback Transaction
                await transaction.RollbackAsync();
                throw;
            }
        }

        public virtual async Task InsertPhBillDiscount(TSalesHeader ObjTSalesHeader, int CurrentUserId, string CurrentUserName)
        {
            // Begin Transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
                string[] AEntity = { "SalesId", "NetAmount", "DiscAmount", "BalanceAmount", "ConcessionReasonId", "CreatedBy" };
                var entity = ObjTSalesHeader.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                odal.ExecuteNonQuery("m_Update_PhBillDiscountAfter", CommandType.StoredProcedure, entity);
                await _context.LogProcedureExecution(entity, nameof(TSalesHeader), ObjTSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                // Save Log
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                // Commit Transaction
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // Rollback Transaction
                await transaction.RollbackAsync();
                throw;
            }
        }

        public virtual async Task InsertSP(TSalesHeader ObjTSalesHeader, int CurrentUserId, string CurrentUserName)
        {
            // Begin Transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
                string[] AEntity = { "ExtMobileNo", "ExternalPatientName", "ExtAddress", "DoctorName", "SalesId" };
                var entity = ObjTSalesHeader.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                odal.ExecuteNonQuery("ps_SalesExtpatientDetUpdate", CommandType.StoredProcedure, entity);
                await _context.LogProcedureExecution(entity, nameof(TSalesHeader), ObjTSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                // Save Log
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                // Commit Transaction
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // Rollback Transaction
                await transaction.RollbackAsync();
                throw;
            }
        }




        public virtual async Task<List<SalesPatientAutoCompleteDto>> SearchRegistration(string str)
        {
            var headers = await _context.TSalesHeaders
               .Where(x => x.ExternalPatientName.ToLower().StartsWith(str) || x.ExtMobileNo.ToLower().StartsWith(str))
              .Select(x => new SalesPatientAutoCompleteDto
              {
                  ExternalPatientName = x.ExternalPatientName,
                  ExtMobileNo = x.ExtMobileNo,
                  DoctorName = x.DoctorName
              })
                .ToListAsync();

            var drafts = await _context.TSalesDraftHeaders
                .Where(x => x.ExternalPatientName.ToLower().StartsWith(str) || x.ExtMobileNo.ToLower().StartsWith(str))
                .Select(x => new SalesPatientAutoCompleteDto
                {
                    ExternalPatientName = x.ExternalPatientName,
                    ExtMobileNo = x.ExtMobileNo,
                    DoctorName = x.DoctorName
                })
                .ToListAsync();

            var result = headers.Union(drafts)
                .DistinctBy(x => new { x.ExtMobileNo, x.ExternalPatientName })
                .OrderByDescending(x => x.ExtMobileNo == str ? 3 :
                                        x.ExtMobileNo.StartsWith(str) ? 2 :
                                        x.ExternalPatientName == str ? 1 : 0)
                .ThenBy(x => x.ExternalPatientName)
                .Take(25)
                .ToList();

            return result;


        }



        public virtual async Task<List<SalesPatientAutoCompleteDto>> SearchExtDoctor(string str)
        {
            var headers = await _context.TSalesHeaders
               .Where(x => x.DoctorName.ToLower().StartsWith(str))
              .Select(x => new SalesPatientAutoCompleteDto
              {

                  DoctorName = x.DoctorName
              })
                .ToListAsync();

            var drafts = await _context.TSalesDraftHeaders
                .Where(x => x.DoctorName.ToLower().StartsWith(str))
                .Select(x => new SalesPatientAutoCompleteDto
                {

                    DoctorName = x.DoctorName
                })
                .ToListAsync();

            var result = headers.Union(drafts)
                .DistinctBy(x => new { x.DoctorName })
                .OrderByDescending(x => x.DoctorName == str ? 3 :
                                        x.DoctorName.StartsWith(str) ? 2 :
                                        x.DoctorName == str ? 1 : 0)
                .ThenBy(x => x.DoctorName)
                .Take(25)
                .ToList();

            return result;

        }
        public virtual async Task<float> GetStock(long StockId)
        {
            TCurrentStock objStock = await _context.TCurrentStocks.FirstOrDefaultAsync(x => x.StockId == StockId);
            return (objStock?.BalanceQty ?? 0) - (objStock?.GrnRetQty ?? 0);
        }
        public virtual async Task Update(TSalesHeader ObjTSalesHeader, List<TSalesDetail> ObjTSalesDetail, List<TCurrentStock> ObjTCurrentStock, int CurrentUserId, string CurrentUserName)
        {
            // Begin Transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction
                string[] rEntity = { "SalesId", "StoreId", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "BalanceAmount" };
                var entity = ObjTSalesHeader.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_UpdateSalesHeader", CommandType.StoredProcedure, entity);
                await _context.LogProcedureExecution(entity, nameof(TSalesHeader), ObjTSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                foreach (var item in ObjTSalesDetail)
                {

                    string[] Entity = { "SalesId", "SalesDetId", "ItemId", "UnitMrp", "Qty", "TotalAmount" };
                    var dentity = item.ToDictionary();
                    foreach (var rProperty in dentity.Keys.ToList())
                    {
                        if (!Entity.Contains(rProperty))
                            dentity.Remove(rProperty);
                    }

                    odal.ExecuteNonQuery("ps_UpdateSalesDetails", CommandType.StoredProcedure, dentity);
                    await _context.LogProcedureExecution(entity, nameof(TSalesDetail), item.SalesDetId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

                }

                foreach (var item in ObjTCurrentStock)
                {
                    string[] REntity = { "ItemId", "IssueQty", "StoreId", "IstkId" };
                    var Pentity = item.ToDictionary();
                    foreach (var rProperty in Pentity.Keys.ToList())
                    {
                        if (!REntity.Contains(rProperty))
                            Pentity.Remove(rProperty);
                    }
                    odal.ExecuteNonQuery("Update_T_CurStk_SalesReturn_Id_1", CommandType.StoredProcedure, Pentity);
                    await _context.LogProcedureExecution(entity, nameof(TCurrentStock), item.StockId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


                }
                // Save Log
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                // Commit Transaction
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // Rollback Transaction
                await transaction.RollbackAsync();
                throw;
            }
        }

        public virtual async Task SalesUpdate(TSalesInpatientHeader ObjTSalesHeader, List<TSalesInpatientDetail> ObjTSalesDetail, List<TCurrentStock> ObjTCurrentStock, int CurrentUserId, string CurrentUserName)
        {
            // Begin Transaction
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                DatabaseHelper odal = new();
                odal.SetConnection(_context.Database.GetDbConnection()); // <-- Share same DbConnection
                odal.SetTransaction(transaction.GetDbTransaction());     // <-- Share same DbTransaction

                string[] rEntity = { "SalesId", "StoreId", "TotalAmount", "VatAmount", "DiscAmount", "NetAmount", "BalanceAmount" };
                var entity = ObjTSalesHeader.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_UpdateSalesInPatientHeader", CommandType.StoredProcedure, entity);
                await _context.LogProcedureExecution(entity, nameof(TSalesInpatientHeader), ObjTSalesHeader.SalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                foreach (var item in ObjTSalesDetail)
                {

                    string[] Entity = { "SalesId", "SalesDetId", "ItemId", "UnitMrp", "Qty", "TotalAmount" };
                    var dentity = item.ToDictionary();
                    foreach (var rProperty in dentity.Keys.ToList())
                    {
                        if (!Entity.Contains(rProperty))
                            dentity.Remove(rProperty);
                    }

                    odal.ExecuteNonQuery("ps_UpdateSalesInPatientDetails", CommandType.StoredProcedure, dentity);
                    await _context.LogProcedureExecution(entity, nameof(TSalesInpatientDetail), item.SalesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

                }

                foreach (var item in ObjTCurrentStock)
                {
                    string[] REntity = { "ItemId", "IssueQty", "StoreId", "IstkId" };
                    var Pentity = item.ToDictionary();
                    foreach (var rProperty in Pentity.Keys.ToList())
                    {
                        if (!REntity.Contains(rProperty))
                            Pentity.Remove(rProperty);
                    }
                    odal.ExecuteNonQuery("Update_T_CurStk_SalesReturn_Id_1", CommandType.StoredProcedure, Pentity);
                    await _context.LogProcedureExecution(entity, nameof(TCurrentStock), item.StockId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
                }
                // Save Log
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
                // Commit Transaction
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // Rollback Transaction
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
