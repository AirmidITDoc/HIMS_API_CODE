using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;
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
            return await DatabaseHelper.GetGridDataBySp<ServiceClassdetailListDto>(model, "ps_Rtrv_ServiceClassdetail");
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

        public virtual async Task InsertAsync(AddCharge objAddCharge, List<AddCharge> objAddCharges, int UserId, string Username)
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
                        string[] AEntity = { "ClassId", "RefundAmount", "CPrice", "CQty", "CTotalAmount", "IsComServ",
                                                   "IsPrintCompSer", "ServiceName", "ChPrice", "ChQty", "ChTotalAmount", "IsBillableCharity", "SalesId", "BillNo", "IsHospMrk","ChargesId",
                                                               "BillNoNavigation","IsDoctorShareGenerated","IsInterimBillFlag","TariffId","UnitId","ServiceCode","CompanyServiceName","IsInclusionExclusion","DoctorName","CreatedBy",
                            "CreatedDate","ModifiedBy","ModifiedDate"};
                        var Packagescharge = item.ToDictionary();

                        foreach (var rProperty in AEntity)
                        {
                            Packagescharge.Remove(rProperty);
                        }
                        Packagescharge["PackageMainChargeId"] = objAddCharge.ChargesId;
                       odal.ExecuteNonQuery("m_insert_IPChargesPackages_1", CommandType.StoredProcedure,  Packagescharge);
                    
                       
                    }

                }


                scope.Complete();
            }
        }

        public virtual async Task IPAddchargesdelete(AddCharge ObjaddCharge, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId",
                "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled","IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "PackageMainChargeID",
                "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId","IsDoctorShareGenerated", "IsInterimBillFlag", "PackageMainChargeId", "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","IsHospMrk","BillNoNavigation","BillNo","TariffId","UnitId","DoctorName","CompanyServiceName","ServiceCode","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var entity = ObjaddCharge.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_Delete_IPAddcharges", CommandType.StoredProcedure, entity);

        }

        public virtual async Task paymentAsyncSP(Payment objPayment, Bill ObjBill, List<AdvanceDetail> objadvanceDetailList, AdvanceHeader objAdvanceHeader, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "ReceiptNo", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            var entity = objPayment.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            entity["OPDIPDType"] = 1; // Ensure objpayment has OPDIPDType
            string PaymentId = odal.ExecuteNonQuery("ps_insert_Payment_New_1", CommandType.StoredProcedure, "PaymentId", entity);
            objPayment.PaymentId = Convert.ToInt32(PaymentId);


            string[] rDetailEntity = { "OpdIpdId", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt","BillDate", "OpdIpdType", "IsCancelled",
                                              "PbillNo","TotalAdvanceAmount","AdvanceUsedAmount","AddedBy","CashCounterId","BillTime","ConcessionReasonId","IsSettled",
                                             "IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","IsBillCheck",
                                              "SpeTaxPer","SpeTaxAmt","IsBillShrHold","DiscComments","ChTotalAmt","ChConcessionAmt","ChNetPayAmt","CompDiscAmt","BillPrefix","BillMonth","BillYear","PrintBillNo","AddCharges","BillDetails"};

            var BillEntity = ObjBill.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                BillEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_BillBalAmount_1", CommandType.StoredProcedure, BillEntity);

            foreach (var item in objadvanceDetailList)
            {

                string[] ADetailEntity = { "Date", "Time", "AdvanceId", "AdvanceNo", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "RefundAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "Advance" };

                var AdvanceDetailEntity = item.ToDictionary();
                foreach (var rProperty in ADetailEntity)
                {
                    AdvanceDetailEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_AdvanceDetail_1", CommandType.StoredProcedure, AdvanceDetailEntity);

            }


            string[] AHeaderEntity = { "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" };

            var AdvanceHeaderEntity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in AHeaderEntity)
            {
                AdvanceHeaderEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_AdvanceHeader_1", CommandType.StoredProcedure, AdvanceHeaderEntity);


            await _context.SaveChangesAsync(UserId, UserName);
        }
        public virtual async Task IPbillAsyncSp(Bill ObjBill, List<BillDetail> ObjBillDetailsModel, AddCharge ObjAddCharge, Admission ObjAddmission, Payment Objpayment, Bill ObjBills, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails" };
            var entity = ObjBill.ToDictionary();
            foreach (var rProperty in rEntity)
            {
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
                string[] BillEntity = { "BillDetailId", "BillNoNavigation" };
                var Bentity = item.ToDictionary();
                foreach (var rProperty in BillEntity)
                {
                    Bentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_BillDetails_1", CommandType.StoredProcedure, Bentity);
            }

            string[] AEntity = { "ChargesId", "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId",
                "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "PackageMainChargeID",
                "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId","IsDoctorShareGenerated", "IsInterimBillFlag", "PackageMainChargeId", "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","IsHospMrk","UnitId","TariffId","DoctorName","ServiceCode","CompanyServiceName",
                "IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate","BillNoNavigation"};

            var AddEntity = ObjAddCharge.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                AddEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Cal_DiscAmount_OPBill", CommandType.StoredProcedure, AddEntity);

            string[] rEntity2 = { "RegId", "AdmissionDate", "AdmissionTime", "PatientTypeId", "HospitalId", "DocNameId", "RefDocNameId", "WardId", "BedId", "DischargeDate", "DischargeTime", "IsDischarged", "IsBillGenerated", "Ipdno", "IsCancelled", "CompanyId",
                             "TariffId","ClassId","DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc","MotherName","AdmittedDoctor1","AdmittedDoctor2","IsProcessing","Ischarity","RefByTypeId","RefByName","IsMarkForDisNur",
                "IsMarkForDisNurId","IsMarkForDisNurDateTime","IsCovidFlag","IsCovidUserId","IsCovidUpdateDate","IsUpdatedBy","SubTpaComId","PolicyNo","AprovAmount","CompDod","IsPharClearance","Ipnumber","EstimatedAmount","ApprovedAmount","HosApreAmt","PathApreAmt",
                "PharApreAmt","RadiApreAmt","PharDisc","CompBillNo","CompBillDate","CompDiscount","CompDisDate","CBillNo","CFinalBillAmt","CDisallowedAmt","ClaimNo","HdiscAmt","COutsideInvestAmt","RecoveredByPatient","HChargeAmt","HAdvAmt","HBillId","HBillDate",
                "HBillNo","HTotalAmt","HDiscAmt1","HNetAmt","HPaidAmt","HBalAmt","IsOpToIpconv","RefDoctorDept","AdmissionType","MedicalApreAmt", "AdminPer","AdminAmt","SubTpacomp","IsCtoH","ConvertId","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate","IsInitinatedDischarge"};
            var entity2 = ObjAddmission.ToDictionary();
            foreach (var rProperty in rEntity2)
            {
                entity2.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_T_AdmissionforIPBilling", CommandType.StoredProcedure, entity2);

            string[] pEntity = { "PaymentId", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            var entity1 = Objpayment.ToDictionary();
            foreach (var rProperty in pEntity)
            {
                entity1.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_insert_Payment_1", CommandType.StoredProcedure, entity1);

            string[] rDetailEntity = { "OpdIpdId", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt","BillDate", "OpdIpdType", "IsCancelled",
                                              "PbillNo","TotalAdvanceAmount","AdvanceUsedAmount","AddedBy","CashCounterId","BillTime","ConcessionReasonId","IsSettled",
                                             "IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","IsBillCheck",
                                              "SpeTaxPer","SpeTaxAmt","IsBillShrHold","DiscComments","ChTotalAmt","ChConcessionAmt","ChNetPayAmt","CompDiscAmt","BillPrefix","BillMonth","BillYear","PrintBillNo","AddCharges","BillDetails"};

            var BEntity = ObjBills.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                BEntity.Remove(rProperty);
            }
                odal.ExecuteNonQuery("ps_update_BillBalAmount_1", CommandType.StoredProcedure, BEntity);


            foreach (var item in ObjadvanceDetailList)
            {

                string[] ADetailEntity = { "Date", "Time", "AdvanceId", "AdvanceNo", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "RefundAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "Advance" };
                var AdvanceDetailEntity = item.ToDictionary();
                foreach (var rProperty in ADetailEntity)
                {
                    AdvanceDetailEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_AdvanceDetail_1", CommandType.StoredProcedure, AdvanceDetailEntity);

            }

            string[] AHeaderEntity = { "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" };
            var AdvanceHeaderEntity = ObjadvanceHeader.ToDictionary();
            foreach (var rProperty in AHeaderEntity)
            {
                AdvanceHeaderEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_AdvanceHeader_1", CommandType.StoredProcedure, AdvanceHeaderEntity);


        }

        public virtual async Task IPbillCreditAsyncSp(Bill ObjBill,List<BillDetail> ObjBillDetailsModel, AddCharge ObjAddCharge, Admission ObjAddmission, Bill ObjBills, List<AdvanceDetail> ObjadvanceDetailList, AdvanceHeader ObjadvanceHeader, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails" };
            var entity = ObjBill.ToDictionary();
            foreach (var rProperty in rEntity)
            {
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
                string[] BillEntity = { "BillDetailId", "BillNoNavigation" };
                var Bentity = item.ToDictionary();
                foreach (var rProperty in BillEntity)
                {
                    Bentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_BillDetails_1", CommandType.StoredProcedure, Bentity);
            }

            string[] AEntity = { "ChargesId", "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId",
                "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "PackageMainChargeID",
                "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId","IsDoctorShareGenerated", "IsInterimBillFlag", "PackageMainChargeId", "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","IsHospMrk","TariffId","ServiceCode","UnitId","DoctorName","CompanyServiceName","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate","IsInclusionExclusion","BillNoNavigation"};

            var AddEntity = ObjAddCharge.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                AddEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Cal_DiscAmount_OPBill", CommandType.StoredProcedure, AddEntity);

            string[] rEntity2 = { "RegId", "AdmissionDate", "AdmissionTime", "PatientTypeId", "HospitalId", "DocNameId", "RefDocNameId", "WardId", "BedId", "DischargeDate", "DischargeTime", "IsDischarged", "IsBillGenerated", "Ipdno", "IsCancelled", "CompanyId",
                             "TariffId","ClassId","DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc","MotherName","AdmittedDoctor1","AdmittedDoctor2","IsProcessing","Ischarity","RefByTypeId","RefByName","IsMarkForDisNur",
                "IsMarkForDisNurId","IsMarkForDisNurDateTime","IsCovidFlag","IsCovidUserId","IsCovidUpdateDate","IsUpdatedBy","SubTpaComId","PolicyNo","AprovAmount","CompDod","IsPharClearance","Ipnumber","EstimatedAmount","ApprovedAmount","HosApreAmt","PathApreAmt",
                "PharApreAmt","RadiApreAmt","PharDisc","CompBillNo","CompBillDate","CompDiscount","CompDisDate","CBillNo","CFinalBillAmt","CDisallowedAmt","ClaimNo","HdiscAmt","COutsideInvestAmt","RecoveredByPatient","HChargeAmt","HAdvAmt","HBillId","HBillDate",
                "HBillNo","HTotalAmt","HDiscAmt1","HNetAmt","HPaidAmt","HBalAmt","IsOpToIpconv","RefDoctorDept","AdmissionType","MedicalApreAmt","AdminPer","AdminAmt","SubTpacomp","IsCtoH","IsInitinatedDischarge","ConvertId","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var entity2 = ObjAddmission.ToDictionary();
            foreach (var rProperty in rEntity2)
            {
                entity2.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_T_AdmissionforIPBilling", CommandType.StoredProcedure, entity2);


            string[] rDetailEntity = { "OpdIpdId", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmt","BillDate", "OpdIpdType", "IsCancelled",
                                              "PbillNo","TotalAdvanceAmount","AdvanceUsedAmount","AddedBy","CashCounterId","BillTime","ConcessionReasonId","IsSettled",
                                             "IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","IsBillCheck",
                                              "SpeTaxPer","SpeTaxAmt","IsBillShrHold","DiscComments","ChTotalAmt","ChConcessionAmt","ChNetPayAmt","CompDiscAmt","BillPrefix","BillMonth","BillYear","PrintBillNo","AddCharges","BillDetails"};

            var BEntity = ObjBills.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                BEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_BillBalAmount_1", CommandType.StoredProcedure, BEntity);


            foreach (var item in ObjadvanceDetailList)
            {

                string[] ADetailEntity = { "Date", "Time", "AdvanceId", "AdvanceNo", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "RefundAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "Advance" };
                var AdvanceDetailEntity = item.ToDictionary();
                foreach (var rProperty in ADetailEntity)
                {
                    AdvanceDetailEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_AdvanceDetail_1", CommandType.StoredProcedure, AdvanceDetailEntity);

            }

            string[] AHeaderEntity = { "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" };
            var AdvanceHeaderEntity = ObjadvanceHeader.ToDictionary();
            foreach (var rProperty in AHeaderEntity)
            {
                AdvanceHeaderEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_AdvanceHeader_1", CommandType.StoredProcedure, AdvanceHeaderEntity);
        }


        public virtual async Task IPInterimBillCashCounterAsyncSp( AddCharge ObjAddCharge, Bill ObjBill, List<BillDetail> ObjBillDetails, Payment Objpayment, int UserId, string UserName)
        {

            DatabaseHelper odal = new();

            string[] AEntity = {  "BillNo","ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId",
                "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "PackageMainChargeID",
                "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId","IsDoctorShareGenerated", "IsInterimBillFlag", "PackageMainChargeId", "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","IsHospMrk","TariffId","ServiceCode","IsInclusionExclusion","UnitId","DoctorName","CompanyServiceName","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate","BillNoNavigation"};

            var yentity = ObjAddCharge.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                yentity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_InterimBillCharges_1", CommandType.StoredProcedure, yentity);

            string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "AddCharges", "BillDetails" };
            var entity = ObjBill.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_CashCounter_1", CommandType.StoredProcedure, "BillNo", entity);
            ObjBill.BillNo = Convert.ToInt32(vBillNo);
        //    ObjBillDetails.BillNo = Convert.ToInt32(vBillNo);
            Objpayment.BillNo = Convert.ToInt32(vBillNo);

            foreach (var item in ObjBillDetails)
            {
                item.BillNo = Convert.ToInt32(vBillNo);
                string[] BillEntity = { "BillDetailId", "BillNoNavigation" };
                var Bentity = item.ToDictionary();
                foreach (var rProperty in BillEntity)
                {
                    Bentity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_BillDetails_1", CommandType.StoredProcedure, Bentity);
            }
            string[] pEntity = { "PaymentId", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            var entity1 = Objpayment.ToDictionary();
            foreach (var rProperty in pEntity)
            {
                entity1.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_insert_Payment_1", CommandType.StoredProcedure, entity1);
        }
        public virtual async Task IPDraftBillAsync(TDrbill ObjTDrbill,List<TDrbillDet> ObjTDrbillDetList, int UserId, string UserName)
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


        public virtual async Task IPAddcharges(AddCharge ObjaddCharge, List<AddCharge> objAddCharges, int UserId, string UserName)
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
                "IsHospMrk","BillNoNavigation","ClassId","BillNo","TariffId","DoctorName","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate",};
               var Aentity = item.ToDictionary();

                foreach (var rProperty in Entity)
                {
                   Aentity.Remove(rProperty);
                }

                string ChargesId = odal.ExecuteNonQuery("ps_insert_IPChargesPackages_1", CommandType.StoredProcedure, "ChargesId", Aentity);
            }
        }

        public virtual async Task Update(AddCharge ObjaddCharge, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId","IsCancelledBy",
                "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled","IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "PackageMainChargeID",
                "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId","IsDoctorShareGenerated", "IsInterimBillFlag", "PackageMainChargeId", "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","IsHospMrk","BillNoNavigation","BillNo","TariffId","UnitId","DoctorName","ServiceCode","CompanyServiceName","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var entity = ObjaddCharge.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

             odal.ExecuteNonQuery("m_update_IPAddCharges", CommandType.StoredProcedure, entity);

        }




        public virtual async Task InsertLabRequest(AddCharge ObjaddCharge, int UserId, string UserName, long traiffId, long ReqDetId)
        {
            DatabaseHelper odal = new();
            string[] AEntity = {  "ChargesId","OpdIpdType",  "Price", "Qty", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount",
         "DocPercentage", "DocAmt", "HospitalAmt", "IsGenerated", "AddedBy", "IsCancelled","IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "PackageMainChargeID",
         "IsSelfOrCompanyService", "PackageId", "ChargesTime", "IsDoctorShareGenerated", "IsInterimBillFlag", "PackageMainChargeId", "RefundAmount", "CPrice", "CQty", "CTotalAmount",
         "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","IsHospMrk","BillNoNavigation","BillNo","IsCancelledBy","UnitId","TariffId","DoctorName","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
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

        public virtual async Task InsertIPDPackage(AddCharge ObjaddCharge, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "IsDoctorShareGenerated", "IsInterimBillFlag",  "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId",
                "IsHospMrk","BillNoNavigation","BillNo","TariffId","UnitId","DoctorName","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate",};
            var entity = ObjaddCharge.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

            string ChargesId = odal.ExecuteNonQuery("ps_insert_IPAddCharges_1", CommandType.StoredProcedure, "ChargesId", entity);

        }
        public virtual async Task UpdateRefund(Refund OBJRefund, int UserId, string UserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] DetailEntity = { "RefundNo", "BillId", "AdvanceId", "OpdIpdType", "OpdIpdId", "RefundAmount", "Remark", "TransactionId", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "CashCounterId", "IsRefundFlag", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "AddedBy", "TRefundDetails" };
            var UEntity = OBJRefund.ToDictionary();
            foreach (var rProperty in DetailEntity)
            {
                UEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("PS_RefundBillDateUpdate", CommandType.StoredProcedure, UEntity);

        }



        public virtual async Task InsertSP(AddCharge ObjaddCharge, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "IsDoctorShareGenerated", "IsInterimBillFlag",  "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId",
                "IsHospMrk","BillNoNavigation","BillNo",
            "ChargesId","ChargesDate","OpdIpdType","ServiceId","Price","Qty","TotalAmt","ConcessionPercentage","ConcessionAmount","NetAmount","DoctorId","DocPercentage","DocAmt","HospitalAmt","IsGenerated","AddedBy",
            "IsCancelled","IsCancelledBy","IsCancelledDate","IsPathology","IsRadiology","IsPackage","IsSelfOrCompanyService","PackageId","ChargesTime","PackageMainChargeId","ClassId","TariffId","UnitId","DoctorName","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate",};
            var entity = ObjaddCharge.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

           odal.ExecuteNonQuery("ps_m_AddBedService_Charges", CommandType.StoredProcedure,  entity);

        }



        public virtual async Task InsertSPC(AddCharge ObjaddCharge, int UserId, string UserName, long? NewClassId)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "IsDoctorShareGenerated", "IsInterimBillFlag",  "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId",
                "IsHospMrk","BillNoNavigation","BillNo",
            "ChargesId","ChargesDate","OpdIpdType","ServiceId","Price","Qty","TotalAmt","ConcessionPercentage","ConcessionAmount","NetAmount","DoctorId","DocPercentage","DocAmt","HospitalAmt","IsGenerated","AddedBy",
            "IsCancelled","IsCancelledBy","IsCancelledDate","IsPathology","IsRadiology","IsPackage","IsSelfOrCompanyService","PackageId","ChargesTime","PackageMainChargeId","UnitId","DoctorName","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate",};
            var entity = ObjaddCharge.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }
            entity["NewClassId"] = NewClassId;

            odal.ExecuteNonQuery("ps_m_ClasswiseRate_change", CommandType.StoredProcedure, entity);

        }

        public virtual async Task InsertSPT(AddCharge ObjaddCharge, int UserId, string UserName, long? NewClassId, long? NewTariffId)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {  "IsDoctorShareGenerated", "IsInterimBillFlag",  "RefundAmount", "CPrice", "CQty", "CTotalAmount",
                "IsComServ", "IsPrintCompSer", "ServiceName", "ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId",
                "IsHospMrk","BillNoNavigation","BillNo",
            "ChargesId","ChargesDate","OpdIpdType","ServiceId","Price","Qty","TotalAmt","ConcessionPercentage","ConcessionAmount","NetAmount","DoctorId","DocPercentage","DocAmt","HospitalAmt","IsGenerated","AddedBy",
            "IsCancelled","IsCancelledBy","IsCancelledDate","IsPathology","IsRadiology","IsPackage","IsSelfOrCompanyService","PackageId","ChargesTime","PackageMainChargeId","UnitId","DoctorName","ServiceCode","CompanyServiceName","IsInclusionExclusion","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate",};
            var entity = ObjaddCharge.ToDictionary();

            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }
            entity["NewClassId"] = NewClassId;
            entity["NewTariffId"] = NewTariffId;


            odal.ExecuteNonQuery("ps_m_Tariffwise_ClassRate_change", CommandType.StoredProcedure, entity);

        }
    }
}