using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;
using System.Security.Principal;
using System.Transactions;

namespace HIMS.Services.Common
{
    public class IPBIllService : IIPBillService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IPBIllService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<IPBillListDto>> GetIPBillListAsync1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPBillListDto>(model, "ps_Rtrv_BrowseIPDBill");

        }
        public virtual async Task<IPagedList<BrowseIPPaymentListDto>> GetIPPaymentListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseIPPaymentListDto>(model, "ps_Rtrv_IPPaymentList");
        }
        public virtual async Task<IPagedList<BrowseIPRefundListDto>> GetIPRefundBillListListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseIPRefundListDto>(model, "ps_Rtrv_IPRefundBillList");
        }
        public virtual async Task<IPagedList<ServiceClassdetailListDto>> ServiceClassdetailList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ServiceClassdetailListDto>(model, "ps_rtrv_BillChargeDetailsList");
        }
        public virtual async Task InsertBillAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName)
        {

            try
            {
                DatabaseHelper odal = new();
                string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                var entity = objBill.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
                objBill.BillNo = Convert.ToInt32(vBillNo);

                using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
                {
                    foreach (var objItem1 in objBill.AddCharges)
                    {
                        // Add Charges Code
                        objItem1.BillNo = objBill.BillNo;
                        objItem1.ChargesDate = Convert.ToDateTime(objItem1.ChargesDate);
                        objItem1.IsCancelledDate = Convert.ToDateTime(objItem1.IsCancelledDate);
                        objItem1.ChargesTime = Convert.ToDateTime(objItem1.ChargesTime);
                        _context.AddCharges.Add(objItem1);
                        await _context.SaveChangesAsync();

                        // Bill Details Code
                        foreach (var objItem in objBill.BillDetails)
                        {
                            objItem.BillNo = objBill.BillNo;
                            objItem.ChargesId = objItem1?.ChargesId;
                            _context.BillDetails.Add(objItem);
                            await _context.SaveChangesAsync();
                        }


                    }

                    // Payment Code
                    int _val = 0;
                    //foreach (var objPayment in objBill.Payments)
                    //{
                    //    if (_val == 0)
                    //    {
                    //        objPayment.BillNo = objBill.BillNo;
                    //        _context.Payments.Add(objPayment);
                    //        await _context.SaveChangesAsync();
                    //    }
                    //    _val += 1;
                    //}
                    //scope.Complete();

                }




                //m_update_BillBalAmount_1
                DatabaseHelper odal3 = new();
                string[] rEntity3 = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                var entity3 = objBill.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity3.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_BillBalAmount_1", CommandType.StoredProcedure, entity3);


            }

            catch (Exception ex)
            {
                Bill? objBills = await _context.Bills.FindAsync(objBill.BillNo);
                _context.Bills.Remove(objBills);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task InsertCreditBillAsyncSP(Bill objBill, int CurrentUserId, string CurrentUserName)
        {
            //throw new NotImplementedException();

            try
            {
                // Bill Code
                DatabaseHelper odal = new();
                string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails", "Payments" };
                var entity = objBill.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
                objBill.BillNo = Convert.ToInt32(vBillNo);

                using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
                {
                    foreach (var objItem1 in objBill.AddCharges)
                    {
                        // Add Charges Code
                        objItem1.BillNo = objBill.BillNo;
                        objItem1.ChargesDate = Convert.ToDateTime(objItem1.ChargesDate);
                        objItem1.IsCancelledDate = Convert.ToDateTime(objItem1.IsCancelledDate);
                        objItem1.ChargesTime = Convert.ToDateTime(objItem1.ChargesTime);
                        _context.AddCharges.Add(objItem1);
                        await _context.SaveChangesAsync();

                        // Bill Details Code
                        foreach (var objItem in objBill.BillDetails)
                        {
                            objItem.BillNo = objBill.BillNo;
                            objItem.ChargesId = objItem1?.ChargesId;
                            _context.BillDetails.Add(objItem);
                            await _context.SaveChangesAsync();
                        }


                    }


                    scope.Complete();

                }



            }

            catch (Exception ex)
            {
                Bill? objBills = await _context.Bills.FindAsync(objBill.BillNo);
                _context.Bills.Remove(objBills);
                await _context.SaveChangesAsync();
            }
        }



        public virtual async Task<IPagedList<IPPreviousBillListDto>> GetIPPreviousBillAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPPreviousBillListDto>(model, "ps_Rtrv_IPPreviousBill_info");
        }
        public virtual async Task<IPagedList<IPAddchargesListDto>> GetIPAddchargesAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPAddchargesListDto>(model, "ps_Rtrv_AddChargesList");
        }
        public virtual async Task<IPagedList<BrowseIPDBillListDto>> GetIPBillListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseIPDBillListDto>(model, "ps_Rtrv_IP_Bill_List_Settlement");
        }
        public virtual async Task<IPagedList<PreviousBillListDto>> GetPreviousBillListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PreviousBillListDto>(model, "ps_Rtrv_IPBillInfo");
        }
        public virtual async Task<IPagedList<PathRadRequestListDto>> PathRadRequestListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PathRadRequestListDto>(model, "ps_Rtrv_PathRadRequestList");
        }
        public virtual async Task<IPagedList<IPPackageDetailsListDto>> IPPackageDetailsListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<IPPackageDetailsListDto>(model, "ps_Rtrv_PackageDetails_List");
        }

        public virtual async Task<IPagedList<PackageDetailsListDto>> Addpackagelist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PackageDetailsListDto>(model, "m_Retrieve_PackageDetails");
        }


        public virtual async Task<IPagedList<PackagedetListDto>> Retrivepackagedetaillist(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PackagedetListDto>(model, "ps_m_Rtrv_Packagedet_List");
        }
        public virtual async Task<IPagedList<BillChargeDetailsListDto>> BillChargeDetailsList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BillChargeDetailsListDto>(model, "ps_rtrv_BillEditDetailsList");
        }


        public virtual async Task InsertAsync(AddCharge objAddCharge, List<AddCharge> objAddCharges, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.AddCharges.Add(objAddCharge);
                await _context.SaveChangesAsync();
                if (objAddCharge.IsPathology == 1)
                {
                    TPathologyReportHeader objPatho = new()
                    {
                        PathDate = objAddCharge.ChargesDate,
                        PathTime = objAddCharge?.ChargesDate,
                        OpdIpdType = objAddCharge?.OpdIpdType,
                        OpdIpdId = objAddCharge?.OpdIpdId,
                        PathTestId = objAddCharge?.ServiceId,
                        AddedBy = objAddCharge?.AddedBy,
                        ChargeId = objAddCharge?.ChargesId,
                        IsCompleted = false,
                        IsPrinted = false,
                        IsSampleCollection = false,
                        TestType = false
                    };

                    _context.TPathologyReportHeaders.Add(objPatho);
                    await _context.SaveChangesAsync();
                }
                // Radiology Code
                if (objAddCharge?.IsRadiology == 1)
                {
                    TRadiologyReportHeader objRadio = new()
                    {
                        RadDate = objAddCharge.ChargesDate,
                        RadTime = objAddCharge?.ChargesDate,
                        OpdIpdType = objAddCharge?.OpdIpdType,
                        OpdIpdId = objAddCharge?.OpdIpdId,
                        RadTestId = objAddCharge?.ServiceId,
                        AddedBy = objAddCharge?.AddedBy,
                        ChargeId = objAddCharge?.ChargesId,
                        IsCompleted = false,
                        IsCancelled = 0,
                        IsPrinted = false,
                        TestType = false
                    };

                    _context.TRadiologyReportHeaders.Add(objRadio);
                    await _context.SaveChangesAsync();
                }
                if (objAddCharge.IsPackage == 1)
                {
                    foreach (var item in objAddCharges)
                    {
                        DatabaseHelper odal = new();
                        string[] AEntity = { "ClassId", "RefundAmount", "CPrice", "CQty", "CTotalAmount", "IsComServ","IsPrintCompSer", "ServiceName", "ChPrice", "ChQty", "ChTotalAmount", "IsBillableCharity", "SalesId", "BillNo", "IsHospMrk","ChargesId",
                                              "BillNoNavigation","IsDoctorShareGenerated","IsInterimBillFlag","TariffId","UnitId","CompanyServiceName","DoctorName","CreatedDate","ModifiedBy","ModifiedDate"};
                        var Packagescharge = item.ToDictionary();

                        foreach (var rProperty in AEntity)
                        {
                            Packagescharge.Remove(rProperty);
                        }
                        Packagescharge["PackageMainChargeId"] = objAddCharge.ChargesId;
                        odal.ExecuteNonQuery("m_insert_IPChargesPackages_1", CommandType.StoredProcedure, Packagescharge);
                        await _context.LogProcedureExecution(Packagescharge, nameof(AddCharge), item.ChargesId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                    }

                }


                scope.Complete();
            }
        }
        public virtual async Task IPAddchargesdelete(AddCharge ObjaddCharge, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "ChargesId", "IsCancelledBy" };
            var entity = ObjaddCharge.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_Delete_IPAddcharges", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(AddCharge), ObjaddCharge.ChargesId.ToInt(), Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);


        }




        public virtual async Task paymentAsyncSP(Payment objPayment, Bill ObjBill, List<AdvanceDetail> objadvanceDetailList, AdvanceHeader objAdvanceHeader, List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "PaymentId", "BillNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo",
                "BankName", "ChequeDate", "CardPayAmount", "CardNo","CardBankName","CardDate","AdvanceUsedAmount","AdvanceId","RefundId","TransactionType","Remark","AddBy","IsCancelled","IsCancelledBy","IsCancelledDate","OPDIPDType","NeftpayAmount","Neftno","NeftbankMaster","Neftdate","PayTmamount","PayTmtranNo","PayTmdate","Tdsamount","UnitId","Wfamount","CompanyId"};
            var entity = objPayment.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            entity["OPDIPDType"] = 1; // Ensure objpayment has OPDIPDType
            string PaymentId = odal.ExecuteNonQuery("ps_insert_Payment_New_1", CommandType.StoredProcedure, "PaymentId", entity);
            objPayment.PaymentId = Convert.ToInt32(PaymentId);


            string[] rDetailEntity = { "BillNo", "BalanceAmt" };

            var BillEntity = ObjBill.ToDictionary();
            foreach (var rProperty in BillEntity.Keys.ToList())
            {
                if (!rDetailEntity.Contains(rProperty))
                    BillEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_BillBalAmount_1", CommandType.StoredProcedure, BillEntity);

            foreach (var item in objadvanceDetailList)
            {

                string[] ADetailEntity = { "AdvanceDetailId", "UsedAmount", "BalanceAmount" };

                var AdvanceDetailEntity = item.ToDictionary();
                foreach (var rProperty in AdvanceDetailEntity.Keys.ToList())
                {
                    if (!ADetailEntity.Contains(rProperty))
                        AdvanceDetailEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_AdvanceDetail_1", CommandType.StoredProcedure, AdvanceDetailEntity);
                await _context.LogProcedureExecution(AdvanceDetailEntity, nameof(AdvanceDetail), item.AdvanceDetailId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);
         }


            string[] AHeaderEntity = { "AdvanceId", "AdvanceUsedAmount", "BalanceAmount" };

            var AdvanceHeaderEntity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in AdvanceHeaderEntity.Keys.ToList())
            {
                if (!AHeaderEntity.Contains(rProperty))
                    AdvanceHeaderEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_AdvanceHeader_1", CommandType.StoredProcedure, AdvanceHeaderEntity);
            foreach (var item in ObjTPayment)
            {
                string[] PEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                                           "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};
                var pentity = item.ToDictionary();
                foreach (var rProperty in pentity.Keys.ToList())
                {
                    if (!PEntity.Contains(rProperty))
                        pentity.Remove(rProperty);
                }
                string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_Payment", CommandType.StoredProcedure, "PaymentId", pentity);
                item.PaymentId = Convert.ToInt32(VPaymentId);
                await _context.LogProcedureExecution(pentity, nameof(TPayment), item.PaymentId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

            }
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
        }
        public virtual async Task paymentMultipleAsyncSP(List<Payment> objPayment, List<Bill> ObjBill, List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            foreach (var item in objPayment)
            {

                string[] rEntity = { "PaymentId", "BillNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo",
                "BankName", "ChequeDate", "CardPayAmount", "CardNo","CardBankName","CardDate","AdvanceUsedAmount","AdvanceId","RefundId","TransactionType","Remark","AddBy","IsCancelled","IsCancelledBy","IsCancelledDate","OPDIPDType","NeftpayAmount","Neftno","NeftbankMaster","Neftdate","PayTmamount","PayTmtranNo","PayTmdate","Tdsamount","UnitId","Wfamount","CompanyId"};
                var entity = item.ToDictionary();
                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }
                entity["OPDIPDType"] = 1; // Ensure objpayment has OPDIPDType
                string PaymentId = odal.ExecuteNonQuery("ps_insert_Payment_New_1", CommandType.StoredProcedure, "PaymentId", entity);
                item.PaymentId = Convert.ToInt32(PaymentId);
            }
            foreach (var item in ObjBill)
            {


                string[] rDetailEntity = { "BillNo", "BalanceAmt" };

                var BillEntity = item.ToDictionary();
                foreach (var rProperty in BillEntity.Keys.ToList())
                {
                    if (!rDetailEntity.Contains(rProperty))
                        BillEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_BillBalAmount_1", CommandType.StoredProcedure, BillEntity);
            }
            foreach (var item in ObjTPayment)
            {
                string[] PEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                                           "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};
                var pentity = item.ToDictionary();
                foreach (var rProperty in pentity.Keys.ToList())
                {
                    if (!PEntity.Contains(rProperty))
                        pentity.Remove(rProperty);
                }
                string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_Payment", CommandType.StoredProcedure, "PaymentId", pentity);
                item.PaymentId = Convert.ToInt32(VPaymentId);
                await _context.LogProcedureExecution(pentity, nameof(TPayment), item.PaymentId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

            }
            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
        }
        public virtual void IPbillSp(Bill ObjBill, List<BillDetail> ObjBillDetailsModel, AddCharge ObjAddCharge, Admission ObjAddmission, Payment Objpayment, Bill ObjBills, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader, List<TPayment> ObjTPayment ,int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "BillNo", "OpdIpdId", "RegNo", "PatientName", "Ipdno", "AgeYear", "AgeMonth", "AgeDays", "DoctorId", "DoctorName", "WardId", "BedId", "PatientType", "CompanyName", "CompanyAmt", "PatientAmt", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt", "BalanceAmt", "BillDate", "OpdIpdType", "AddedBy", "TotalAdvanceAmount", "BillTime", "ConcessionReasonId", "IsSettled", "IsPrinted", "IsFree", "CompanyId", "TariffId", "UnitId", "InterimOrFinal", "CompanyRefNo", "ConcessionAuthorizationName", "SpeTaxPer", "SpeTaxAmt", "DiscComments", "CompDiscAmt", "CashCounterId", "CreatedBy", "GovtApprovedAmt" };
            var entity = ObjBill.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_CashCounter_1", CommandType.StoredProcedure, "BillNo", entity);
            ObjBill.BillNo = Convert.ToInt32(vBillNo);
            //   ObjBillDetailsModel.BillNo = Convert.ToInt32(vBillNo);
            ObjAddCharge.BillNo = Convert.ToInt32(vBillNo);
            Objpayment.BillNo = Convert.ToInt32(vBillNo);


            foreach (var item in ObjBillDetailsModel)
            {
                item.BillNo = Convert.ToInt32(vBillNo);
                string[] BillEntity = { "BillNo", "ChargesId" };
                var Bentity = item.ToDictionary();
                foreach (var rProperty in Bentity.Keys.ToList())
                {
                    if (!BillEntity.Contains(rProperty))
                        Bentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_BillDetails_1", CommandType.StoredProcedure, Bentity);
            }

            string[] AEntity = { "BillNo" };

            var AddEntity = ObjAddCharge.ToDictionary();
            foreach (var rProperty in AddEntity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    AddEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Cal_DiscAmount_OPBill", CommandType.StoredProcedure, AddEntity);

            string[] rEntity2 = { "AdmissionId" };
            var entity2 = ObjAddmission.ToDictionary();
            foreach (var rProperty in entity2.Keys.ToList())
            {
                if (!rEntity2.Contains(rProperty))
                    entity2.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_T_AdmissionforIPBilling", CommandType.StoredProcedure, entity2);

            string[] pEntity = { "BillNo", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "UnitId" };
            var entity1 = Objpayment.ToDictionary();
            foreach (var rProperty in entity1.Keys.ToList())
            {
                if (!pEntity.Contains(rProperty))
                    entity1.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_insert_Payment_1", CommandType.StoredProcedure, entity1);
            string[] rDetailEntity = { "BillNo", "balanceAmt" };

            var BEntity = ObjBills.ToDictionary();
            foreach (var rProperty in BEntity.Keys.ToList())
            {
                if (!rDetailEntity.Contains(rProperty))
                    BEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_BillBalAmount_1", CommandType.StoredProcedure, BEntity);



            foreach (var item in ObjadvanceDetailList)
            {

                string[] ADetailEntity = { "AdvanceDetailId", "UsedAmount", "BalanceAmount" };
                var AdvanceDetailEntity = item.ToDictionary();
                foreach (var rProperty in AdvanceDetailEntity.Keys.ToList())
                {
                    if (!ADetailEntity.Contains(rProperty))
                        AdvanceDetailEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_AdvanceDetail_1", CommandType.StoredProcedure, AdvanceDetailEntity);

            }

            string[] AHeaderEntity = { "AdvanceId", "AdvanceUsedAmount", "BalanceAmount" };
            var AdvanceHeaderEntity = ObjadvanceHeader.ToDictionary();
            foreach (var rProperty in AdvanceHeaderEntity.Keys.ToList())
            {
                if (!AHeaderEntity.Contains(rProperty))
                    AdvanceHeaderEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_AdvanceHeader_1", CommandType.StoredProcedure, AdvanceHeaderEntity);
            foreach (var item in ObjTPayment)
            {
                item.BillNo = Convert.ToInt32(vBillNo);

                string[] PEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                                           "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};
                var pentity = item.ToDictionary();
                foreach (var rProperty in pentity.Keys.ToList())
                {
                    if (!PEntity.Contains(rProperty))
                        pentity.Remove(rProperty);
                }
                string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_Payment", CommandType.StoredProcedure, "PaymentId", pentity);
                item.PaymentId = Convert.ToInt32(VPaymentId);
            }
        }



        public virtual async Task IPbillCreditSp(Bill ObjBill, List<BillDetail> ObjBillDetailsModel, AddCharge ObjAddCharge, Admission ObjAddmission, Bill ObjBills, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "BillNo", "OpdIpdId", "RegNo", "PatientName", "Ipdno", "AgeYear", "AgeMonth", "AgeDays", "DoctorId", "DoctorName", "WardId", "BedId", "PatientType", "CompanyName", "CompanyAmt", "PatientAmt", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt", "BalanceAmt", "BillDate", "OpdIpdType", "AddedBy", "TotalAdvanceAmount", "BillTime", "ConcessionReasonId", "IsSettled", "IsPrinted", "IsFree", "CompanyId", "TariffId", "UnitId", "InterimOrFinal", "CompanyRefNo", "ConcessionAuthorizationName", "SpeTaxPer", "SpeTaxAmt", "DiscComments", "CompDiscAmt", "CashCounterId", "CreatedBy", "GovtApprovedAmt" };
            var entity = ObjBill.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_CashCounter_1", CommandType.StoredProcedure, "BillNo", entity);
            ObjBill.BillNo = Convert.ToInt32(vBillNo);
            //    ObjBillDetailsModel.BillNo = Convert.ToInt32(vBillNo);
            ObjAddCharge.BillNo = Convert.ToInt32(vBillNo);
            //     Objpayment.BillNo = Convert.ToInt32(vBillNo);

            foreach (var item in ObjBillDetailsModel)
            {
                item.BillNo = Convert.ToInt32(vBillNo);
                string[] BillEntity = { "BillNo", "ChargesId" };
                var Bentity = item.ToDictionary();
                foreach (var rProperty in Bentity.Keys.ToList())
                {
                    if (!BillEntity.Contains(rProperty))
                        Bentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_BillDetails_1", CommandType.StoredProcedure, Bentity);
            }

            string[] AEntity = { "BillNo"};

            var AddEntity = ObjAddCharge.ToDictionary();
            foreach (var rProperty in AddEntity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    AddEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Cal_DiscAmount_OPBill", CommandType.StoredProcedure, AddEntity);

            string[] rEntity2 = { "AdmissionId" };
            var entity2 = ObjAddmission.ToDictionary();
            foreach (var rProperty in entity2.Keys.ToList())
            {
                if (!rEntity2.Contains(rProperty))
                    entity2.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_T_AdmissionforIPBilling", CommandType.StoredProcedure, entity2);


            string[] rDetailEntity = { "BillNo", "balanceAmt"};

            var BEntity = ObjBills.ToDictionary();
            foreach (var rProperty in BEntity.Keys.ToList())
            {
                if (!rDetailEntity.Contains(rProperty))
                    BEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_BillBalAmount_1", CommandType.StoredProcedure, BEntity);


            foreach (var item in ObjadvanceDetailList)
            {

                string[] ADetailEntity = { "AdvanceDetailId", "UsedAmount", "BalanceAmount" };
                var AdvanceDetailEntity = item.ToDictionary();
                foreach (var rProperty in AdvanceDetailEntity.Keys.ToList())
                {
                    if (!ADetailEntity.Contains(rProperty))
                        AdvanceDetailEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_AdvanceDetail_1", CommandType.StoredProcedure, AdvanceDetailEntity);
                await _context.LogProcedureExecution(AdvanceDetailEntity, nameof(AdvanceHeader), item.AdvanceId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


            }

            string[] AHeaderEntity = { "AdvanceId", "AdvanceUsedAmount", "BalanceAmount" };
            var AdvanceHeaderEntity = ObjadvanceHeader.ToDictionary();
            foreach (var rProperty in AdvanceHeaderEntity.Keys.ToList())
            {
                if (!AHeaderEntity.Contains(rProperty))
                    AdvanceHeaderEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_AdvanceHeader_1", CommandType.StoredProcedure, AdvanceHeaderEntity);
            await _context.LogProcedureExecution(AdvanceHeaderEntity, nameof(AdvanceHeader), ObjadvanceHeader.AdvanceId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

        }



        public virtual void IPInterimBillCashCounterSp(AddCharge ObjAddCharge, Bill ObjBill, List<BillDetail> ObjBillDetails, Payment Objpayment, List<TPayment> ObjTPayment, int UserId, string UserName)
        {

            DatabaseHelper odal = new();

            string[] AEntity = { "ChargesId" };

            var yentity = ObjAddCharge.ToDictionary();
            foreach (var rProperty in yentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    yentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_InterimBillCharges_1", CommandType.StoredProcedure, yentity);

            string[] rEntity = { "BillNo", "OpdIpdId", "RegNo", "PatientName", "Ipdno", "AgeYear", "AgeMonth", "AgeDays", "DoctorId", "DoctorName", "WardId", "BedId", "PatientType", "CompanyName", "CompanyAmt", "PatientAmt", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt", "BalanceAmt", "BillDate", "OpdIpdType", "AddedBy", "TotalAdvanceAmount", "BillTime", "ConcessionReasonId", "IsSettled", "IsPrinted", "IsFree", "CompanyId", "TariffId", "UnitId", "InterimOrFinal", "CompanyRefNo", "ConcessionAuthorizationName", "SpeTaxPer", "SpeTaxAmt", "DiscComments", "CompDiscAmt", "CashCounterId", "CreatedBy" };
            var entity = ObjBill.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_CashCounter_1", CommandType.StoredProcedure, "BillNo", entity);
            ObjBill.BillNo = Convert.ToInt32(vBillNo);
            //    ObjBillDetails.BillNo = Convert.ToInt32(vBillNo);
            Objpayment.BillNo = Convert.ToInt32(vBillNo);

            foreach (var item in ObjBillDetails)
            {
                item.BillNo = Convert.ToInt32(vBillNo);
                string[] BillEntity = { "BillNo", "ChargesId" };
                var Bentity = item.ToDictionary();
                foreach (var rProperty in Bentity.Keys.ToList())
                {
                    if (!BillEntity.Contains(rProperty))
                        Bentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_BillDetails_1", CommandType.StoredProcedure, Bentity);
            }
            string[] PEntity = { "BillNo", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "UnitId" };
            var entity1 = Objpayment.ToDictionary();
            foreach (var rProperty in entity1.Keys.ToList())
            {
                if (!PEntity.Contains(rProperty))
                    entity1.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_insert_Payment_IPInterim_1", CommandType.StoredProcedure, entity1);

            foreach (var item in ObjTPayment)
            {
                item.BillNo = Convert.ToInt32(vBillNo);

                string[] Entity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                                           "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};
                var pentity = item.ToDictionary();
                foreach (var rProperty in pentity.Keys.ToList())
                {
                    if (!Entity.Contains(rProperty))
                        pentity.Remove(rProperty);
                }
                string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_Payment", CommandType.StoredProcedure, "PaymentId", pentity);
                item.PaymentId = Convert.ToInt32(VPaymentId);
            }

        }
        public virtual void IPDraftBill(TDrbill ObjTDrbill, List<TDrbillDet> ObjTDrbillDetList, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "IsCancelled", "PbillNo", "CashCounterId", "AdvanceUsedAmount" };
            var entity = ObjTDrbill.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vDRBNo = odal.ExecuteNonQuery("ps_insert_DRBill_1", CommandType.StoredProcedure, "Drbno", entity);
            ObjTDrbill.Drbno = Convert.ToInt32(vDRBNo);


            foreach (var item in ObjTDrbillDetList)
            {
                item.Drno = Convert.ToInt32(vDRBNo);
                string[] TEntity = { "DrbillDetId", };
                var Dentity = item.ToDictionary();
                foreach (var rProperty in TEntity)
                {
                    Dentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_T_DRBillDet_1", CommandType.StoredProcedure, Dentity);
            }

        }


        public virtual void IPAddcharges(AddCharge ObjaddCharge, List<AddCharge> objAddCharges, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "IsDoctorShareGenerated", "IsInterimBillFlag",  "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","UnitId","TariffId","DoctorName","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate",
                "IsHospMrk","BillNoNavigation","BillNo"};
            var entity = ObjaddCharge.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

            string AChargesId = odal.ExecuteNonQuery("ps_insert_IPAddCharges_1", CommandType.StoredProcedure, "ChargesId", entity);
            ObjaddCharge.ChargesId = Convert.ToInt32(AChargesId);

            foreach (var item in objAddCharges)
            {
                item.ChargesId = Convert.ToInt32(AChargesId);
                string[] Entity = { "IsDoctorShareGenerated", "IsInterimBillFlag",  "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId",
                "IsHospMrk","BillNoNavigation","ClassId","BillNo","TariffId","DoctorName","UnitId","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate","ChargesId"};
                var Aentity = item.ToDictionary();

                foreach (var rProperty in Entity)
                {
                    Aentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_IPChargesPackages_1", CommandType.StoredProcedure, Aentity);
            }
        }

        public virtual void Update(AddCharge ObjaddCharge, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId","IsCancelledBy",
                "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled","IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "PackageMainChargeID",
                "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId","IsDoctorShareGenerated", "IsInterimBillFlag", "PackageMainChargeId", "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","IsHospMrk","BillNoNavigation","BillNo","TariffId","UnitId","DoctorName","ServiceCode","CompanyServiceName","CreatedBy","CreatedDate","ModifiedDate","WardId","BedId"};
            var entity = ObjaddCharge.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_IPAddCharges", CommandType.StoredProcedure, entity);

        }




        public virtual void InsertLabRequest(AddCharge ObjaddCharge, int UserId, string UserName, long traiffId, long ReqDetId)
        {
            DatabaseHelper odal = new();
            string[] AEntity = {  "ChargesId","OpdIpdType",  "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount",
                "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled","IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "PackageMainChargeID",
                "IsSelfOrCompanyService", "PackageId", "ChargesTime", "IsDoctorShareGenerated", "IsInterimBillFlag", "PackageMainChargeId", "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","IsHospMrk","BillNoNavigation","BillNo","IsCancelledBy","UnitId","TariffId",
                "DoctorName","ServiceCode","CompanyServiceName","IsInclusionExclusion","WardId","BedId","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var entity = ObjaddCharge.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }
            // Add TraiffId manually to dictionary
            entity["TraiffId"] = traiffId;
            entity["ReqDetId"] = ReqDetId;
            entity["UserId"] = UserId;

            odal.ExecuteNonQuery("ps_Insert_LabRequest_Charges_1", CommandType.StoredProcedure, entity);

        }

       
        public virtual void InsertIPDPackage(AddCharge ObjaddCharge, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "ChargesId", "ChargesDate",  "OpdIpdType", "OpdIpdId", "ServiceId", "Price",
                                  "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount","NetAmount","DoctorId","DocPercentage","DocAmt",
                                  "HospitalAmt","IsGenerated","AddedBy","IsCancelled","IsCancelledBy","IsCancelledDate","IsPathology","IsRadiology","IsPackage","IsSelfOrCompanyService","PackageId","WardId","BedId","ChargesTime","PackageMainChargeId","ClassId"};
            var entity = ObjaddCharge.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            string ChargesId = odal.ExecuteNonQuery("ps_insert_IPAddCharges_1", CommandType.StoredProcedure, "ChargesId", entity);

        }
        public virtual async Task UpdateBill(List<AddCharge> ObjaddCharge, Bill ObjBill, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "BillNo", "ChargesDate", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "AddedBy", "ChargesTime", "IsInclusionExclusion","ChargesId", "IsApprovedByCamp" };
            foreach (var item in ObjaddCharge)
            {

                var entity = item.ToDictionary();

                foreach (var rProperty in entity.Keys.ToList())
                {
                    if (!AEntity.Contains(rProperty))
                        entity.Remove(rProperty);
                }

                odal.ExecuteNonQuery("ps_UpdateIPAddCharges_1", CommandType.StoredProcedure, entity);
            }
            // -------------------- BILL UPDATE ------------------------

            string[] BEntity = { "BillNo", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt", "BalanceAmt", "CompanyAmt", "PatientAmt", "SpeTaxPer", "SpeTaxAmt", "ConcessionReasonId", "DiscComments", "ModifiedBy" };
            var bentity = ObjBill.ToDictionary();

            foreach (var rProperty in bentity.Keys.ToList())
            {
                if (!BEntity.Contains(rProperty))
                    bentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_BillAmountDetails", CommandType.StoredProcedure, bentity);
            await _context.LogProcedureExecution(bentity, nameof(Bill), ObjBill.BillNo.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


        }




        public virtual async Task UpdateRefund(Refund OBJRefund, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "RefundDate", "RefundTime", "RefundId" };
            var Rentity = OBJRefund.ToDictionary();

            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("PS_RefundBillDateUpdate", CommandType.StoredProcedure, Rentity);
            await _context.LogProcedureExecution(Rentity, nameof(Refund), OBJRefund.RefundId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

        }



        public virtual void InsertSP(AddCharge ObjaddCharge, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "IsDoctorShareGenerated", "IsInterimBillFlag",  "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId",
                "IsHospMrk","BillNoNavigation","BillNo",
            "ChargesId","ChargesDate","OpdIpdType","ServiceId","Price","Qty","TotalAmt","ConcessionPercentage","ConcessionAmount","NetAmount","DoctorId","DocPercentage","DocAmt","HospitalAmt","IsGenerated","AddedBy",
            "IsCancelled","IsCancelledBy","IsCancelledDate","IsPathology","IsRadiology","IsPackage","IsSelfOrCompanyService","PackageId","WardId","BedId","ChargesTime","PackageMainChargeId","ClassId","TariffId","UnitId","DoctorName","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate",};
            var entity = ObjaddCharge.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_m_AddBedService_Charges", CommandType.StoredProcedure, entity);

        }



        public virtual void InsertSPC(AddCharge ObjaddCharge, int UserId, string UserName, long? NewClassId)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "IsDoctorShareGenerated", "IsInterimBillFlag",  "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId",
                "IsHospMrk","BillNoNavigation","BillNo",
            "ChargesId","ChargesDate","OpdIpdType","ServiceId","Price","Qty","TotalAmt","ConcessionPercentage","ConcessionAmount","NetAmount","DoctorId","DocPercentage","DocAmt","HospitalAmt","IsGenerated","AddedBy",
            "IsCancelled","IsCancelledBy","IsCancelledDate","IsPathology","IsRadiology","IsPackage","IsSelfOrCompanyService","PackageId","WardId","BedId","ChargesTime","PackageMainChargeId","UnitId","DoctorName","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate",};
            var entity = ObjaddCharge.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }
            entity["NewClassId"] = NewClassId;

            odal.ExecuteNonQuery("ps_m_ClasswiseRate_change", CommandType.StoredProcedure, entity);

        }

        public virtual void InsertSPT(AddCharge ObjaddCharge, int UserId, string UserName, long? NewClassId, long? NewTariffId)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "IsDoctorShareGenerated", "IsInterimBillFlag",  "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId",
                "IsHospMrk","BillNoNavigation","BillNo",
                "ChargesId","ChargesDate","OpdIpdType","ServiceId","Price","Qty","TotalAmt","ConcessionPercentage","ConcessionAmount","NetAmount","DoctorId","DocPercentage","DocAmt","HospitalAmt","IsGenerated","AddedBy",
               "IsCancelled","IsCancelledBy","IsCancelledDate","IsPathology","IsRadiology","IsPackage","IsSelfOrCompanyService","PackageId","WardId","BedId","ChargesTime","PackageMainChargeId","UnitId","DoctorName","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate",};
            var entity = ObjaddCharge.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }
            entity["NewClassId"] = NewClassId;
            entity["NewTariffId"] = NewTariffId;


            odal.ExecuteNonQuery("ps_m_Tariffwise_ClassRate_change", CommandType.StoredProcedure, entity);

        }


        public virtual void IPbillSp(Bill ObjBill, int UserId, string UserName)
        {

            DatabaseHelper odal = new();

            string[] AEntity = { "OpdIpdId", "TotalAmt", "PaidAmt","BillDate", "OpdIpdType", "IsCancelled",
                                              "PbillNo","TotalAdvanceAmount","AdvanceUsedAmount","AddedBy","CashCounterId","BillTime","IsSettled",
                                             "IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","IsBillCheck",
                                              "RegNo","PatientName","Ipdno","AgeYear","AgeMonth","AgeDays","DoctorId","DoctorName","WardId","BedId","PatientType","CompanyName","CompanyAmt","PatientAmt","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate",
                                              "SpeTaxPer","SpeTaxAmt","IsBillShrHold","DiscComments","ChTotalAmt","ChConcessionAmt","ChNetPayAmt","BillPrefix","BillMonth","BillYear","PrintBillNo","AddCharges","RefundAmount","BillDetails"};

            var AddEntity = ObjBill.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                AddEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_Update_BillDiscountAfter_1", CommandType.StoredProcedure, AddEntity);
        }

    }
}