using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.Common
{
    public class OPBillingService : IOPBillingService
    {
        private readonly HIMSDbContext _context;
        public OPBillingService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<CertificateInformationListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<CertificateInformationListDto>(model, "m_Rtrv_T_CertificateInformation_List");
        }
        public virtual async Task InsertAsync(TCertificateInformation ObjCertificateInformation, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TCertificateInformations.Add(ObjCertificateInformation);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }


        public virtual async Task UpdateAsync(TCertificateInformation ObjCertificateInformation, int UserId, string Username)
        {
            // throw new NotImplementedException();
            _context.TCertificateInformations.Update(ObjCertificateInformation);
            _context.Entry(ObjCertificateInformation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //scope.Complete();
        }
        //public virtual async Task UpdateAsync(TCertificateInformation TCertificateInformation, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Update header & detail table records
        //        TCertificateInformation objReg = _context.TCertificateInformations.Where(x => x.CertificateId == TCertificateInformation.CertificateId).FirstOrDefault();
        //        if (objReg != null)
        //            _context.Entry(objReg).State = EntityState.Detached;

        //        TCertificateInformation.CertificateId = objReg.CertificateId;
        //        TCertificateInformation.CertificateDate = objReg.CertificateDate;
        //        TCertificateInformation.CertificateTime = objReg.CertificateTime;
        //        TCertificateInformation.VisitId = objReg.VisitId;
        //        //TCertificateInformation.CertificateTempId = objReg.CertificateTempId;
        //        TCertificateInformation.CertificateName = objReg.CertificateName;
        //        TCertificateInformation.CertificateText = objReg.CertificateText;
        //        //TCertificateInformation.ModifiedBy = objReg.ModifiedBy;

        //        _context.TCertificateInformations.Update(TCertificateInformation);
        //        _context.Entry(TCertificateInformation).State = EntityState.Modified;

        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}

        //public virtual async Task InsertAsyncSP(Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, int CurrentUserId, string CurrentUserName)
        //{

        //    try
        //    {
        //        DatabaseHelper odal = new();
        //        string[] rEntity = { "IsCancelled", "PbillNo",  "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo","RefundAmount", "AddCharges", "BillDetails",
        //            "Payments","CreatedDate","ModifiedBy","ModifiedDate" };
        //        var entity = objBill.ToDictionary();
        //        foreach (var rProperty in rEntity)
        //        {
        //            entity.Remove(rProperty);
        //        }
        //        string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
        //        objBill.BillNo = Convert.ToInt32(vBillNo);

        //        using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //        {
        //            foreach (var objItem1 in objBill.AddCharges)
        //            {
        //                // Add Charges Code
        //                objItem1.BillNo = objBill.BillNo;
        //                objItem1.ChargesDate = Convert.ToDateTime(objItem1.ChargesDate);
        //                objItem1.IsCancelledDate = Convert.ToDateTime(objItem1.IsCancelledDate);
        //                objItem1.ChargesTime = Convert.ToDateTime(objItem1.ChargesTime);
        //                _context.AddCharges.Add(objItem1);
        //                await _context.SaveChangesAsync();

        //                // Bill Details Code
        //                BillDetail objBillDet = new()
        //                {
        //                    BillNo = objBill.BillNo,
        //                    ChargesId = objItem1?.ChargesId
        //                };
        //                _context.BillDetails.Add(objBillDet);
        //                await _context.SaveChangesAsync();

        //                // Pathology Code
        //                if (objItem1.IsPathology == 1)
        //                {
        //                    TPathologyReportHeader objPatho = new()
        //                    {
        //                        PathDate = objItem1.ChargesDate,
        //                        PathTime = objItem1?.ChargesDate,
        //                        OpdIpdType = objItem1?.OpdIpdType,
        //                        OpdIpdId = objItem1?.OpdIpdId,
        //                        PathTestId = objItem1?.ServiceId,
        //                        AddedBy = objItem1?.AddedBy,
        //                        ChargeId = objItem1?.ChargesId,
        //                        IsCompleted = false,
        //                        IsPrinted = false,
        //                        IsSampleCollection = false,
        //                        TestType = false
        //                    };

        //                    _context.TPathologyReportHeaders.Add(objPatho);
        //                    await _context.SaveChangesAsync();
        //                }
        //                // Radiology Code
        //                if (objItem1?.IsRadiology == 1)
        //                {
        //                    TRadiologyReportHeader objRadio = new()
        //                    {
        //                        RadDate = objItem1.ChargesDate,
        //                        RadTime = objItem1?.ChargesDate,
        //                        OpdIpdType = objItem1?.OpdIpdType,
        //                        OpdIpdId = objItem1?.OpdIpdId,
        //                        RadTestId = objItem1?.ServiceId,
        //                        AddedBy = objItem1?.AddedBy,
        //                        ChargeId = objItem1?.ChargesId,
        //                        IsCompleted = false,
        //                        IsCancelled = 0,
        //                        IsPrinted = false,
        //                        TestType = false
        //                    };

        //                    _context.TRadiologyReportHeaders.Add(objRadio);
        //                    await _context.SaveChangesAsync();
        //                }
        //                if (objItem1.IsPackage == 1)
        //                {
        //                    foreach (var item in ObjaddCharge)
        //                    {
        //                        string[] AEntity = { "IsDoctorShareGenerated", "CPrice", "CQty", "CTotalAmount", "ChPrice", "ChQty", "ChTotalAmount", "IsBillableCharity", "IsInterimBillFlag", "BillNoNavigation", "CreatedDate", "ModifiedBy", "ModifiedDate" };
        //                        var Packagescharge = item.ToDictionary();

        //                        foreach (var rProperty in AEntity)
        //                        {
        //                            Packagescharge.Remove(rProperty);
        //                        }
        //                        Packagescharge["PackageMainChargeId"] = objItem1.ChargesId;
        //                        Packagescharge["BillNo"] = objBill.BillNo;
        //                        var VChargesId = odal.ExecuteNonQuery("ps_insert_AddChargesPackages_1", CommandType.StoredProcedure, "ChargesId", Packagescharge);
        //                        item.ChargesId = Convert.ToInt32(VChargesId);

        //                        // //   Package Service add in Bill Details
        //                        Dictionary<string, object> OPBillDet2 = new()
        //                        {
        //                            ["BillNo"] = objBill.BillNo,
        //                            ["ChargesId"] = VChargesId
        //                        };

        //                        odal.ExecuteNonQuery("ps_insert_BillDetails_1", CommandType.StoredProcedure, OPBillDet2);
        //                    }

        //                }


        //            }

        //            string[] rPaymentEntity = { "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "BillNoNavigation", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
        //            Payment objPay = new();
        //            objPay = objPayment;
        //            objPay.BillNo = objBill.BillNo;
        //            var entity2 = objPayment.ToDictionary();
        //            foreach (var rProperty in rPaymentEntity)
        //            {
        //                entity2.Remove(rProperty);
        //            }
        //            entity2["OPDIPDType"] = 0; // Ensure objpayment has OPDIPDType
        //            string PaymentId = odal.ExecuteNonQuery("ps_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", entity2);
        //            objPayment.PaymentId = Convert.ToInt32(PaymentId);

        //            scope.Complete();
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        Bill? objBills = await _context.Bills.FindAsync(objBill.BillNo);
        //        _context.Bills.Remove(objBills);
        //        await _context.SaveChangesAsync();
        //    }
        //}
        public virtual async Task InsertAsyncSP(Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, int CurrentUserId, string CurrentUserName)
        {

            try
            {
                DatabaseHelper odal = new();
                string[] BEntity = { "OpdIpdId", "RegNo",  "PatientName", "Ipdno", "AgeYear", "AgeMonth", "AgeDays", "DoctorId", "DoctorName", "WardId", "BedId","PatientType", "CompanyName", "CompanyAmt",
                    "PatientAmt","TotalAmt","ConcessionAmt","NetPayableAmt","PaidAmt","BalanceAmt","BillDate","OpdIpdType","AddedBy","TotalAdvanceAmount","AdvanceUsedAmount","BillTime","ConcessionReasonId","IsSettled","IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","SpeTaxPer","SpeTaxAmt","CompDiscAmt","DiscComments"/*"CashCounterId"*/,"CreatedBy","BillNo"};
                var bentity = objBill.ToDictionary();
                foreach (var rProperty in bentity.Keys.ToList())
                {
                    if (!BEntity.Contains(rProperty))
                        bentity.Remove(rProperty);
                }
                string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", bentity);
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
                        BillDetail objBillDet = new()
                        {
                            BillNo = objBill.BillNo,
                            ChargesId = objItem1?.ChargesId
                        };
                        _context.BillDetails.Add(objBillDet);
                        await _context.SaveChangesAsync();

                        // Pathology Code
                        if (objItem1.IsPathology == 1)
                        {
                            TPathologyReportHeader objPatho = new()
                            {
                                PathDate = objItem1.ChargesDate,
                                PathTime = objItem1?.ChargesDate,
                                OpdIpdType = objItem1?.OpdIpdType,
                                OpdIpdId = objItem1?.OpdIpdId,
                                PathTestId = objItem1?.ServiceId,
                                AddedBy = objItem1?.AddedBy,
                                ChargeId = objItem1?.ChargesId,
                                IsCompleted = false,
                                IsPrinted = false,
                                IsSampleCollection = false,
                                TestType = false
                            };

                            _context.TPathologyReportHeaders.Add(objPatho);
                            await _context.SaveChangesAsync();
                        }
                        // Radiology Code
                        if (objItem1?.IsRadiology == 1)
                        {
                            TRadiologyReportHeader objRadio = new()
                            {
                                RadDate = objItem1.ChargesDate,
                                RadTime = objItem1?.ChargesDate,
                                OpdIpdType = objItem1?.OpdIpdType,
                                OpdIpdId = objItem1?.OpdIpdId,
                                RadTestId = objItem1?.ServiceId,
                                AddedBy = objItem1?.AddedBy,
                                ChargeId = objItem1?.ChargesId,
                                IsCompleted = false,
                                IsCancelled = 0,
                                IsPrinted = false,
                                TestType = false
                            };

                            _context.TRadiologyReportHeaders.Add(objRadio);
                            await _context.SaveChangesAsync();
                        }
                        if (objItem1.IsPackage == 1)
                        {
                            foreach (var item in ObjaddCharge)
                            {
                                string[] AEntity = { "ChargesId", "ChargesDate", "OpdIpdType", "ServiceId", "Price", "Qty", "UnitId", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DoctorName", "DocPercentage", "DocAmt", "HospitalAmt", "RefundAmount", "IsGenerated", "IsComServ", "IsPrintCompSer", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "WardId", "BedId", "ServiceCode", "ServiceName", "CompanyServiceName", "IsInclusionExclusion", "IsHospMrk", "PackageMainChargeID", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId", "TariffId", "BillNo", "CreatedBy" };
                                var Packagescharge = item.ToDictionary();

                                foreach (var rProperty in Packagescharge.Keys.ToList())
                                {
                                    if (!AEntity.Contains(rProperty))
                                        Packagescharge.Remove(rProperty);
                                }
                                Packagescharge["PackageMainChargeId"] = objItem1.ChargesId;
                                Packagescharge["BillNo"] = objBill.BillNo;
                                var VChargesId = odal.ExecuteNonQuery("ps_insert_AddChargesPackages_1", CommandType.StoredProcedure, "ChargesId", Packagescharge);
                                item.ChargesId = Convert.ToInt32(VChargesId);
                                // //   Package Service add in Bill Details
                                Dictionary<string, object> OPBillDet2 = new()
                                {
                                    ["BillNo"] = objBill.BillNo,
                                    ["ChargesId"] = VChargesId
                                };

                                odal.ExecuteNonQuery("ps_insert_BillDetails_1", CommandType.StoredProcedure, OPBillDet2);
                            }

                        }


                    }

                    string[] rPaymentEntity = { "PaymentId", "UnitId", "BillNo", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "SalesId", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "CompanyId" };
                    Payment objPay = new();
                    objPay = objPayment;
                    objPay.BillNo = objBill.BillNo;
                    var entity2 = objPayment.ToDictionary();
                    foreach (var rProperty in entity2.Keys.ToList())
                    {
                        if (!rPaymentEntity.Contains(rProperty))
                            entity2.Remove(rProperty);
                    }
                    entity2["OPDIPDType"] = 0; // Ensure objpayment has OPDIPDType
                    string PaymentId = odal.ExecuteNonQuery("ps_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", entity2);
                    objPayment.PaymentId = Convert.ToInt32(PaymentId);


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



        //public virtual async Task InsertAsyncSP1(Bill objBill, int CurrentUserId, string CurrentUserName)
        //{
        //    try
        //    {
        //        // Bill Code
        //        DatabaseHelper odal = new();
        //        string[] rEntity = { "IsCancelled", "PbillNo", "AdvanceUsedAmount", "CashCounterId", "IsBillCheck", "IsBillShrHold", "ChTotalAmt", "ChConcessionAmt", "ChNetPayAmt", "BillPrefix", "BillMonth", "BillYear", "PrintBillNo", "RefundAmount", "AddCharges", "BillDetails", "Payments" };
        //        var entity = objBill.ToDictionary();
        //        foreach (var rProperty in rEntity)
        //        {
        //            entity.Remove(rProperty);
        //        }
        //        string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", entity);
        //        objBill.BillNo = Convert.ToInt32(vBillNo);

        //        using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //        {
        //            foreach (var objItem1 in objBill.AddCharges)
        //            {
        //                // Add Charges Code
        //                objItem1.BillNo = objBill.BillNo;
        //                objItem1.ChargesDate = Convert.ToDateTime(objItem1.ChargesDate);
        //                objItem1.IsCancelledDate = Convert.ToDateTime(objItem1.IsCancelledDate);
        //                objItem1.ChargesTime = Convert.ToDateTime(objItem1.ChargesTime);
        //                _context.AddCharges.Add(objItem1);
        //                await _context.SaveChangesAsync();

        //                // Bill Details Code
        //                BillDetail objBillDet = new()
        //                {
        //                    BillNo = objBill.BillNo,
        //                    ChargesId = objItem1?.ChargesId
        //                };
        //                _context.BillDetails.Add(objBillDet);
        //                await _context.SaveChangesAsync();

        //                // Pathology Code
        //                if (objItem1.IsPathology == 1)
        //                {
        //                    TPathologyReportHeader objPatho = new()
        //                    {
        //                        PathDate = objItem1.ChargesDate,
        //                        PathTime = objItem1?.ChargesDate,
        //                        OpdIpdType = objItem1?.OpdIpdType,
        //                        OpdIpdId = objItem1?.OpdIpdId,
        //                        PathTestId = objItem1?.ServiceId,
        //                        AddedBy = objItem1?.AddedBy,
        //                        ChargeId = objItem1?.ChargesId,
        //                        IsCompleted = false,
        //                        IsPrinted = false,
        //                        IsSampleCollection = false,
        //                        TestType = false
        //                    };

        //                    _context.TPathologyReportHeaders.Add(objPatho);
        //                    await _context.SaveChangesAsync();
        //                }
        //                // Radiology Code
        //                if (objItem1?.IsRadiology == 1)
        //                {
        //                    TRadiologyReportHeader objRadio = new()
        //                    {
        //                        RadDate = objItem1.ChargesDate,
        //                        RadTime = objItem1?.ChargesDate,
        //                        OpdIpdType = objItem1?.OpdIpdType,
        //                        OpdIpdId = objItem1?.OpdIpdId,
        //                        RadTestId = objItem1?.ServiceId,
        //                        AddedBy = objItem1?.AddedBy,
        //                        ChargeId = objItem1?.ChargesId,
        //                        IsCompleted = false,
        //                        IsCancelled = 0,
        //                        IsPrinted = false,
        //                        TestType = false
        //                    };

        //                    _context.TRadiologyReportHeaders.Add(objRadio);
        //                    await _context.SaveChangesAsync();
        //                }

        //            }

        //           scope.Complete();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Bill? objBills = await _context.Bills.FindAsync(objBill.BillNo);
        //        _context.Bills.Remove(objBills);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public virtual async Task InsertCreditBillAsyncSP( Bill objBill, int currentUserId, string currentUserName)
        {
            // throw new NotImplementedException();
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
                        objItem1.BillNo = objBill.BillNo;
                        objItem1.ChargesDate = Convert.ToDateTime(objItem1.ChargesDate);
                        objItem1.IsCancelledDate = Convert.ToDateTime(objItem1.IsCancelledDate);
                        objItem1.ChargesTime = Convert.ToDateTime(objItem1.ChargesTime);

                        _context.AddCharges.Add(objItem1);
                        await _context.SaveChangesAsync();

                        foreach (var objItem in objBill.BillDetails)
                        {
                            objItem.BillNo = objBill.BillNo;
                            objItem.ChargesId = objItem1?.ChargesId;
                            _context.BillDetails.Add(objItem);
                            await _context.SaveChangesAsync();

                        }

                        // Pathology Code
                        if (objItem1.IsPathology == 1)
                        {
                            TPathologyReportHeader objPatho = new()
                            {
                                PathDate = objItem1.ChargesDate,
                                PathTime = objItem1?.ChargesDate,
                                OpdIpdType = objItem1?.OpdIpdType,
                                OpdIpdId = objItem1?.OpdIpdId,
                                PathTestId = objItem1?.ServiceId,
                                AddedBy = objItem1?.AddedBy,
                                ChargeId = objItem1?.ChargesId,
                                IsCompleted = false,
                                IsPrinted = false,
                                IsSampleCollection = false,
                                TestType = false
                            };

                            _context.TPathologyReportHeaders.Add(objPatho);
                            await _context.SaveChangesAsync();
                        }
                        // Radiology Code
                        if (objItem1?.IsRadiology == 1)
                        {
                            TRadiologyReportHeader objRadio = new()
                            {
                                RadDate = objItem1.ChargesDate,
                                RadTime = objItem1?.ChargesDate,
                                OpdIpdType = objItem1?.OpdIpdType,
                                OpdIpdId = objItem1?.OpdIpdId,
                                RadTestId = objItem1?.ServiceId,
                                AddedBy = objItem1?.AddedBy,
                                ChargeId = objItem1?.ChargesId,
                                IsCompleted = false,
                                IsCancelled = 0,
                                IsPrinted = false,
                                TestType = false
                            };

                            _context.TRadiologyReportHeaders.Add(objRadio);
                            await _context.SaveChangesAsync();
                        }
                    }

                }
            }


            catch (Exception)
            {
                Bill objBills = await _context.Bills.FindAsync(objBill.BillNo);
                _context.Bills.Remove(objBills);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task AppBillInsert(Registration objRegistration, VisitDetail objVisitDetail, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, int CurrentUserId, string CurrentUserName)
        {

           
            try
            {
                DatabaseHelper odal1 = new();
                string[] rEntity = { "RegNo", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember", "UpdatedBy", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
                var entity = objRegistration.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string RegId = odal1.ExecuteNonQuery("ps_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
                objRegistration.RegId = Convert.ToInt32(RegId);
               

                objVisitDetail.RegId = objRegistration.RegId;
                _context.VisitDetails.Add(objVisitDetail);
                await _context.SaveChangesAsync();
                objBill.OpdIpdId = objVisitDetail.VisitId;

                 ConfigSetting objConfigSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                objConfigSetting.Opno = Convert.ToString(Convert.ToInt32(objConfigSetting.Opno) + 1);
                _context.ConfigSettings.Update(objConfigSetting);
                _context.Entry(objConfigSetting).State = EntityState.Modified;
                await _context.SaveChangesAsync();


                DatabaseHelper odal = new();
                    string[] BEntity = { "OpdIpdId", "RegNo",  "PatientName", "Ipdno", "AgeYear", "AgeMonth", "AgeDays", "DoctorId", "DoctorName", "WardId", "BedId","PatientType", "CompanyName", "CompanyAmt",
                    "PatientAmt","TotalAmt","ConcessionAmt","NetPayableAmt","PaidAmt","BalanceAmt","BillDate","OpdIpdType","AddedBy","TotalAdvanceAmount","AdvanceUsedAmount","BillTime","ConcessionReasonId","IsSettled","IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","SpeTaxPer","SpeTaxAmt","CompDiscAmt","DiscComments"/*"CashCounterId"*/,"CreatedBy","BillNo"};
                    var bentity = objBill.ToDictionary();
                    

                foreach (var rProperty in bentity.Keys.ToList())
                    {
                        if (!BEntity.Contains(rProperty))
                            bentity.Remove(rProperty);
                    }
                    string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", bentity);
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
                           objItem1.OpdIpdId = objVisitDetail.VisitId;

                        _context.AddCharges.Add(objItem1);
                            await _context.SaveChangesAsync();

                            // Bill Details Code
                            BillDetail objBillDet = new()
                            {
                                BillNo = objBill.BillNo,
                                ChargesId = objItem1?.ChargesId
                            };
                            _context.BillDetails.Add(objBillDet);
                            await _context.SaveChangesAsync();

                            // Pathology Code
                            if (objItem1.IsPathology == 1)
                            {
                                TPathologyReportHeader objPatho = new()
                                {
                                    PathDate = objItem1.ChargesDate,
                                    PathTime = objItem1?.ChargesDate,
                                    OpdIpdType = objItem1?.OpdIpdType,
                                    OpdIpdId = objItem1?.OpdIpdId,
                                    PathTestId = objItem1?.ServiceId,
                                    AddedBy = objItem1?.AddedBy,
                                    ChargeId = objItem1?.ChargesId,
                                    IsCompleted = false,
                                    IsPrinted = false,
                                    IsSampleCollection = false,
                                    TestType = false
                                };

                                _context.TPathologyReportHeaders.Add(objPatho);
                                await _context.SaveChangesAsync();
                            }
                            // Radiology Code
                            if (objItem1?.IsRadiology == 1)
                            {
                                TRadiologyReportHeader objRadio = new()
                                {
                                    RadDate = objItem1.ChargesDate,
                                    RadTime = objItem1?.ChargesDate,
                                    OpdIpdType = objItem1?.OpdIpdType,
                                    OpdIpdId = objItem1?.OpdIpdId,
                                    RadTestId = objItem1?.ServiceId,
                                    AddedBy = objItem1?.AddedBy,
                                    ChargeId = objItem1?.ChargesId,
                                    IsCompleted = false,
                                    IsCancelled = 0,
                                    IsPrinted = false,
                                    TestType = false
                                };

                                _context.TRadiologyReportHeaders.Add(objRadio);
                                await _context.SaveChangesAsync();
                            }
                            if (objItem1.IsPackage == 1)
                            {
                                foreach (var item in ObjaddCharge)
                                {
                                    string[] AEntity = { "ChargesId", "ChargesDate", "OpdIpdType", "ServiceId", "Price", "Qty", "UnitId", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DoctorName", "DocPercentage", "DocAmt", "HospitalAmt", "RefundAmount", "IsGenerated", "IsComServ", "IsPrintCompSer", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "WardId", "BedId", "ServiceCode", "ServiceName", "CompanyServiceName", "IsInclusionExclusion", "IsHospMrk", "PackageMainChargeID", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId", "TariffId", "BillNo", "CreatedBy" };
                                    var Packagescharge = item.ToDictionary();

                                    foreach (var rProperty in Packagescharge.Keys.ToList())
                                    {
                                        if (!AEntity.Contains(rProperty))
                                            Packagescharge.Remove(rProperty);
                                    }
                                    Packagescharge["PackageMainChargeId"] = objItem1.ChargesId;
                                    Packagescharge["BillNo"] = objBill.BillNo;
                                    var VChargesId = odal.ExecuteNonQuery("ps_insert_AddChargesPackages_1", CommandType.StoredProcedure, "ChargesId", Packagescharge);
                                    item.ChargesId = Convert.ToInt32(VChargesId);
                                    // //   Package Service add in Bill Details
                                    Dictionary<string, object> OPBillDet2 = new()
                                    {
                                        ["BillNo"] = objBill.BillNo,
                                        ["ChargesId"] = VChargesId
                                    };

                                    odal.ExecuteNonQuery("ps_insert_BillDetails_1", CommandType.StoredProcedure, OPBillDet2);
                                }

                            }


                        }

                        string[] rPaymentEntity = { "PaymentId", "UnitId", "BillNo", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "SalesId", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "CompanyId" };
                        Payment objPay = new();
                        objPay = objPayment;
                        objPay.BillNo = objBill.BillNo;
                        var entity2 = objPayment.ToDictionary();
                        foreach (var rProperty in entity2.Keys.ToList())
                        {
                            if (!rPaymentEntity.Contains(rProperty))
                                entity2.Remove(rProperty);
                        }
                        entity2["OPDIPDType"] = 0; // Ensure objpayment has OPDIPDType
                        string PaymentId = odal.ExecuteNonQuery("ps_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", entity2);
                        objPayment.PaymentId = Convert.ToInt32(PaymentId);


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

        public virtual async Task InsertAppointmentCreditBillAsyncSP(Registration objRegistration, VisitDetail objVisitDetail, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, int currentUserId, string currentUserName)
        {
            try
            {
                DatabaseHelper odal1 = new();
                string[] rEntity = { "RegNo", "RegPrefix", "AnnualIncome", "IsIndientOrWeaker", "RationCardNo", "IsMember", "UpdatedBy", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
                var entity = objRegistration.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                string RegId = odal1.ExecuteNonQuery("ps_insert_Registration_1", CommandType.StoredProcedure, "RegId", entity);
                objRegistration.RegId = Convert.ToInt32(RegId);


                objVisitDetail.RegId = objRegistration.RegId;
                _context.VisitDetails.Add(objVisitDetail);
                await _context.SaveChangesAsync();
                objBill.OpdIpdId = objVisitDetail.VisitId;

                ConfigSetting objConfigSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                objConfigSetting.Opno = Convert.ToString(Convert.ToInt32(objConfigSetting.Opno) + 1);
                _context.ConfigSettings.Update(objConfigSetting);
                _context.Entry(objConfigSetting).State = EntityState.Modified;
                await _context.SaveChangesAsync();


                DatabaseHelper odal = new();
                string[] BEntity = { "OpdIpdId", "RegNo",  "PatientName", "Ipdno", "AgeYear", "AgeMonth", "AgeDays", "DoctorId", "DoctorName", "WardId", "BedId","PatientType", "CompanyName", "CompanyAmt",
                    "PatientAmt","TotalAmt","ConcessionAmt","NetPayableAmt","PaidAmt","BalanceAmt","BillDate","OpdIpdType","AddedBy","TotalAdvanceAmount","AdvanceUsedAmount","BillTime","ConcessionReasonId","IsSettled","IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","SpeTaxPer","SpeTaxAmt","CompDiscAmt","DiscComments"/*"CashCounterId"*/,"CreatedBy","BillNo"};
                var bentity = objBill.ToDictionary();


                foreach (var rProperty in bentity.Keys.ToList())
                {
                    if (!BEntity.Contains(rProperty))
                        bentity.Remove(rProperty);
                }
                string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", bentity);
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
                        objItem1.OpdIpdId = objVisitDetail.VisitId;

                        _context.AddCharges.Add(objItem1);
                        await _context.SaveChangesAsync();

                        // Bill Details Code
                        BillDetail objBillDet = new()
                        {
                            BillNo = objBill.BillNo,
                            ChargesId = objItem1?.ChargesId
                        };
                        _context.BillDetails.Add(objBillDet);
                        await _context.SaveChangesAsync();

                        // Pathology Code
                        if (objItem1.IsPathology == 1)
                        {
                            TPathologyReportHeader objPatho = new()
                            {
                                PathDate = objItem1.ChargesDate,
                                PathTime = objItem1?.ChargesDate,
                                OpdIpdType = objItem1?.OpdIpdType,
                                OpdIpdId = objItem1?.OpdIpdId,
                                PathTestId = objItem1?.ServiceId,
                                AddedBy = objItem1?.AddedBy,
                                ChargeId = objItem1?.ChargesId,
                                IsCompleted = false,
                                IsPrinted = false,
                                IsSampleCollection = false,
                                TestType = false
                            };

                            _context.TPathologyReportHeaders.Add(objPatho);
                            await _context.SaveChangesAsync();
                        }
                        // Radiology Code
                        if (objItem1?.IsRadiology == 1)
                        {
                            TRadiologyReportHeader objRadio = new()
                            {
                                RadDate = objItem1.ChargesDate,
                                RadTime = objItem1?.ChargesDate,
                                OpdIpdType = objItem1?.OpdIpdType,
                                OpdIpdId = objItem1?.OpdIpdId,
                                RadTestId = objItem1?.ServiceId,
                                AddedBy = objItem1?.AddedBy,
                                ChargeId = objItem1?.ChargesId,
                                IsCompleted = false,
                                IsCancelled = 0,
                                IsPrinted = false,
                                TestType = false
                            };

                            _context.TRadiologyReportHeaders.Add(objRadio);
                            await _context.SaveChangesAsync();
                        }
                        if (objItem1.IsPackage == 1)
                        {
                            foreach (var item in ObjaddCharge)
                            {
                                string[] AEntity = { "ChargesId", "ChargesDate", "OpdIpdType", "ServiceId", "Price", "Qty", "UnitId", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DoctorName", "DocPercentage", "DocAmt", "HospitalAmt", "RefundAmount", "IsGenerated", "IsComServ", "IsPrintCompSer", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "WardId", "BedId", "ServiceCode", "ServiceName", "CompanyServiceName", "IsInclusionExclusion", "IsHospMrk", "PackageMainChargeID", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId", "TariffId", "BillNo", "CreatedBy" };
                                var Packagescharge = item.ToDictionary();

                                foreach (var rProperty in Packagescharge.Keys.ToList())
                                {
                                    if (!AEntity.Contains(rProperty))
                                        Packagescharge.Remove(rProperty);
                                }
                                Packagescharge["PackageMainChargeId"] = objItem1.ChargesId;
                                Packagescharge["BillNo"] = objBill.BillNo;
                                var VChargesId = odal.ExecuteNonQuery("ps_insert_AddChargesPackages_1", CommandType.StoredProcedure, "ChargesId", Packagescharge);
                                item.ChargesId = Convert.ToInt32(VChargesId);
                                // //   Package Service add in Bill Details
                                Dictionary<string, object> OPBillDet2 = new()
                                {
                                    ["BillNo"] = objBill.BillNo,
                                    ["ChargesId"] = VChargesId
                                };

                                odal.ExecuteNonQuery("ps_insert_BillDetails_1", CommandType.StoredProcedure, OPBillDet2);
                            }

                        }


                    }

                    //string[] rPaymentEntity = { "PaymentId", "UnitId", "BillNo", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "SalesId", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount" };
                    //Payment objPay = new();
                    //objPay = objPayment;
                    //objPay.BillNo = objBill.BillNo;
                    //var entity2 = objPayment.ToDictionary();
                    //foreach (var rProperty in entity2.Keys.ToList())
                    //{
                    //    if (!rPaymentEntity.Contains(rProperty))
                    //        entity2.Remove(rProperty);
                    //}
                    //entity2["OPDIPDType"] = 0; // Ensure objpayment has OPDIPDType
                    //string PaymentId = odal.ExecuteNonQuery("ps_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", entity2);
                    //objPayment.PaymentId = Convert.ToInt32(PaymentId);


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

        public virtual async Task RegisteredAppBillInsert(VisitDetail objVisitDetail, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, int CurrentUserId, string CurrentUserName)
        {
            try
            {
               
              _context.VisitDetails.Add(objVisitDetail);
                await _context.SaveChangesAsync();
                objBill.OpdIpdId = objVisitDetail.VisitId;

                ConfigSetting objConfigSetting = await _context.ConfigSettings.FindAsync(Convert.ToInt64(1));
                objConfigSetting.Opno = Convert.ToString(Convert.ToInt32(objConfigSetting.Opno) + 1);
                _context.ConfigSettings.Update(objConfigSetting);
                _context.Entry(objConfigSetting).State = EntityState.Modified;
                await _context.SaveChangesAsync();


                DatabaseHelper odal = new();
                string[] BEntity = { "OpdIpdId", "RegNo",  "PatientName", "Ipdno", "AgeYear", "AgeMonth", "AgeDays", "DoctorId", "DoctorName", "WardId", "BedId","PatientType", "CompanyName", "CompanyAmt",
                    "PatientAmt","TotalAmt","ConcessionAmt","NetPayableAmt","PaidAmt","BalanceAmt","BillDate","OpdIpdType","AddedBy","TotalAdvanceAmount","AdvanceUsedAmount","BillTime","ConcessionReasonId","IsSettled","IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","SpeTaxPer","SpeTaxAmt","CompDiscAmt","DiscComments"/*"CashCounterId"*/,"CreatedBy","BillNo"};
                var bentity = objBill.ToDictionary();


                foreach (var rProperty in bentity.Keys.ToList())
                {
                    if (!BEntity.Contains(rProperty))
                        bentity.Remove(rProperty);
                }
                string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", bentity);
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
                        objItem1.OpdIpdId = objVisitDetail.VisitId;

                        _context.AddCharges.Add(objItem1);
                        await _context.SaveChangesAsync();

                        // Bill Details Code
                        BillDetail objBillDet = new()
                        {
                            BillNo = objBill.BillNo,
                            ChargesId = objItem1?.ChargesId
                        };
                        _context.BillDetails.Add(objBillDet);
                        await _context.SaveChangesAsync();

                        // Pathology Code
                        if (objItem1.IsPathology == 1)
                        {
                            TPathologyReportHeader objPatho = new()
                            {
                                PathDate = objItem1.ChargesDate,
                                PathTime = objItem1?.ChargesDate,
                                OpdIpdType = objItem1?.OpdIpdType,
                                OpdIpdId = objItem1?.OpdIpdId,
                                PathTestId = objItem1?.ServiceId,
                                AddedBy = objItem1?.AddedBy,
                                ChargeId = objItem1?.ChargesId,
                                IsCompleted = false,
                                IsPrinted = false,
                                IsSampleCollection = false,
                                TestType = false
                            };

                            _context.TPathologyReportHeaders.Add(objPatho);
                            await _context.SaveChangesAsync();
                        }
                        // Radiology Code
                        if (objItem1?.IsRadiology == 1)
                        {
                            TRadiologyReportHeader objRadio = new()
                            {
                                RadDate = objItem1.ChargesDate,
                                RadTime = objItem1?.ChargesDate,
                                OpdIpdType = objItem1?.OpdIpdType,
                                OpdIpdId = objItem1?.OpdIpdId,
                                RadTestId = objItem1?.ServiceId,
                                AddedBy = objItem1?.AddedBy,
                                ChargeId = objItem1?.ChargesId,
                                IsCompleted = false,
                                IsCancelled = 0,
                                IsPrinted = false,
                                TestType = false
                            };

                            _context.TRadiologyReportHeaders.Add(objRadio);
                            await _context.SaveChangesAsync();
                        }
                        if (objItem1.IsPackage == 1)
                        {
                            foreach (var item in ObjaddCharge)
                            {
                                string[] AEntity = { "ChargesId", "ChargesDate", "OpdIpdType", "ServiceId", "Price", "Qty", "UnitId", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DoctorName", "DocPercentage", "DocAmt", "HospitalAmt", "RefundAmount", "IsGenerated", "IsComServ", "IsPrintCompSer", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "WardId", "BedId", "ServiceCode", "ServiceName", "CompanyServiceName", "IsInclusionExclusion", "IsHospMrk", "PackageMainChargeID", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId", "TariffId", "BillNo", "CreatedBy" };
                                var Packagescharge = item.ToDictionary();

                                foreach (var rProperty in Packagescharge.Keys.ToList())
                                {
                                    if (!AEntity.Contains(rProperty))
                                        Packagescharge.Remove(rProperty);
                                }
                                Packagescharge["PackageMainChargeId"] = objItem1.ChargesId;
                                Packagescharge["BillNo"] = objBill.BillNo;
                                var VChargesId = odal.ExecuteNonQuery("ps_insert_AddChargesPackages_1", CommandType.StoredProcedure, "ChargesId", Packagescharge);
                                item.ChargesId = Convert.ToInt32(VChargesId);
                                // //   Package Service add in Bill Details
                                Dictionary<string, object> OPBillDet2 = new()
                                {
                                    ["BillNo"] = objBill.BillNo,
                                    ["ChargesId"] = VChargesId
                                };

                                odal.ExecuteNonQuery("ps_insert_BillDetails_1", CommandType.StoredProcedure, OPBillDet2);
                            }

                        }


                    }

                    string[] rPaymentEntity = { "PaymentId", "UnitId", "BillNo", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "SalesId", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "CompanyId" };
                    Payment objPay = new();
                    objPay = objPayment;
                    objPay.BillNo = objBill.BillNo;
                    var entity2 = objPayment.ToDictionary();
                    foreach (var rProperty in entity2.Keys.ToList())
                    {
                        if (!rPaymentEntity.Contains(rProperty))
                            entity2.Remove(rProperty);
                    }
                    entity2["OPDIPDType"] = 0; // Ensure objpayment has OPDIPDType
                    string PaymentId = odal.ExecuteNonQuery("ps_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", entity2);
                    objPayment.PaymentId = Convert.ToInt32(PaymentId);


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
    }
    }
