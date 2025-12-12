using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Pathlogy
{
    public class LabPatientRegistrationService : ILabPatientRegistrationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public LabPatientRegistrationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<LabregBilldetailListDto>> GetBillDetailListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabregBilldetailListDto>(model, "ps_Rtrv_LabRegisterBillDetail");
        }

        public virtual async Task<IPagedList<LabPatientRegistrationListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabPatientRegistrationListDto>(model, "ps_LabPatientRegistrationList");
        }

        public virtual async Task InsertAsync(TLabPatientRegistration ObjTLabPatientRegistration, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Get last SequenceNo (string type)
                var lastSeqNoStr = await _context.TLabPatientRegistrations
                    .OrderByDescending(x => x.LabRequestNo)
                    .Select(x => x.LabRequestNo)
                    .FirstOrDefaultAsync();

                int lastSeqNo = 0;
                if (!string.IsNullOrEmpty(lastSeqNoStr) && int.TryParse(lastSeqNoStr, out var parsed))
                    lastSeqNo = parsed;

                ObjTLabPatientRegistration.LabRequestNo = (lastSeqNo + 1).ToString();
                _context.TLabPatientRegistrations.Add(ObjTLabPatientRegistration);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task InsertAsyncSP(TLabPatientRegistration ObjTLabPatientRegistration, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, TPayment ObjTPayment, int CurrentUserId, string CurrentUserName)
        {

            try
            {

                DatabaseHelper odal = new();
                string[] rEntity = { "RegDate", "RegTime", "UnitId", "PrefixId", "FirstName", "MiddleName", "LastName", "GenderId", "MobileNo", "DateofBirth", "AgeYear", "AgeMonth", "AgeDay", "Address", "CityId", "StateId", "CountryId", "PatientTypeId", "TariffId", "ClassId", "DepartmentId", "DoctorId", "RefDocId", "CreatedBy", "LabPatientId", "LabPatRegId", "AdharCardNo", "CompanyId", "SubCompanyId", "CampId"};

                var lentity = ObjTLabPatientRegistration.ToDictionary();
                foreach (var rProperty in lentity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        lentity.Remove(rProperty);
                }
                string VLabPatientId = odal.ExecuteNonQuery("ps_Insert_LabPatientRegistration", CommandType.StoredProcedure, "LabPatientId", lentity);
                ObjTLabPatientRegistration.LabPatientId = Convert.ToInt32(VLabPatientId);
                objBill.OpdIpdId = ObjTLabPatientRegistration.LabPatientId;



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
                        objItem1.OpdIpdId = ObjTLabPatientRegistration.LabPatientId;

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
                                TestType = false,
                                PatientName = objBill.PatientName,
                                RegNo = objBill.RegNo.ToString(),
                                Opipnumber = objBill.Ipdno,
                                DoctorName = objBill.DoctorName,
                                CreatedBy = CurrentUserId,
                                CreatedDate = DateTime.Now
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
                                TestType = false,
                                PatientName = objBill.PatientName,
                                RegNo = objBill.RegNo.ToString(),
                                Opipnumber = objBill.Ipdno,
                                DoctorName = objBill.DoctorName,
                                CreatedBy = CurrentUserId,
                                CreatedDate = DateTime.Now
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
                        
                            string[] PEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                                           "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};

                            TPayment objTPay = new();
                            objTPay = ObjTPayment;
                            objTPay.BillNo = objBill.BillNo;

                            var pentity = ObjTPayment.ToDictionary();
                            foreach (var rProperty in pentity.Keys.ToList())
                            {
                                if (!PEntity.Contains(rProperty))
                                    pentity.Remove(rProperty);
                            }
                            string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_Payment", CommandType.StoredProcedure, "PaymentId", pentity);
                            ObjTPayment.PaymentId = Convert.ToInt32(VPaymentId);
                        
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

        public virtual async Task InsertPaidBillAsync(TLabPatientRegistration ObjTLabPatientRegistration, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, TPayment ObjTPayment, int CurrentUserId, string CurrentUserNameint)
        {

            try
            {

                DatabaseHelper odal = new();
                string[] rEntity = { "RegDate", "RegTime", "UnitId", "PrefixId", "FirstName", "MiddleName", "LastName", "GenderId", "MobileNo", "DateofBirth", "AgeYear", "AgeMonth", "AgeDay", "Address", "CityId", "StateId", "CountryId", "PatientTypeId", "TariffId", "ClassId", "DepartmentId", "DoctorId", "RefDocId", "CreatedBy", "LabPatientId", "LabPatRegId","AdharCardNo", "CompanyId", "SubCompanyId", "CampId" };

                var lentity = ObjTLabPatientRegistration.ToDictionary();
                foreach (var rProperty in lentity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        lentity.Remove(rProperty);
                }
                string VLabPatientId = odal.ExecuteNonQuery("ps_Insert_LabPatientRegistration", CommandType.StoredProcedure, "LabPatientId", lentity);
                ObjTLabPatientRegistration.LabPatientId = Convert.ToInt32(VLabPatientId);
                objBill.OpdIpdId = ObjTLabPatientRegistration.LabPatientId;



                string[] BEntity = { "OpdIpdId", "RegNo",  "PatientName", "Ipdno", "AgeYear", "AgeMonth", "AgeDays", "DoctorId", "DoctorName", "WardId", "BedId","PatientType", "CompanyName", "CompanyAmt",
                    "PatientAmt","TotalAmt","ConcessionAmt","NetPayableAmt","PaidAmt","BalanceAmt","BillDate","OpdIpdType","AddedBy","TotalAdvanceAmount","AdvanceUsedAmount","BillTime","ConcessionReasonId",
                    "IsSettled","IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","SpeTaxPer","SpeTaxAmt","CompDiscAmt","DiscComments"/*"CashCounterId"*/,"CreatedBy","BillNo"};
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
                        objItem1.OpdIpdId = ObjTLabPatientRegistration.LabPatientId;


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
                                TestType = false,
                                PatientName = objBill.PatientName,
                                RegNo = objBill.RegNo.ToString(),
                                Opipnumber = objBill.Ipdno,
                                DoctorName = objBill.DoctorName,
                                CreatedBy = CurrentUserId,
                                CreatedDate = DateTime.Now
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
                                TestType = false,
                                PatientName = objBill.PatientName,
                                RegNo = objBill.RegNo.ToString(),
                                Opipnumber = objBill.Ipdno,
                                DoctorName = objBill.DoctorName,
                                CreatedBy = CurrentUserId,
                                CreatedDate = DateTime.Now
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
                    entity2["OPDIPDType"] = 4; // Ensure objpayment has OPDIPDType
                    string PaymentId = odal.ExecuteNonQuery("ps_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", entity2);
                    objPayment.PaymentId = Convert.ToInt32(PaymentId);

                    
                        string[] PEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                                           "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};

                        TPayment objTPay = new();
                        objTPay = ObjTPayment;
                        objTPay.BillNo = objBill.BillNo;

                        var pentity = ObjTPayment.ToDictionary();
                        foreach (var rProperty in pentity.Keys.ToList())
                        {
                            if (!PEntity.Contains(rProperty))
                                pentity.Remove(rProperty);
                        }
                        string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_Payment", CommandType.StoredProcedure, "PaymentId", pentity);
                        ObjTPayment.PaymentId = Convert.ToInt32(VPaymentId);
                    

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

        public List<TLabPatientRegistration> SearchlabRegistration(string Keyword)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@Keyword", Keyword);
            return sql.FetchListBySP<TLabPatientRegistration>("ps_Rtrv_PatientLabRegisteredListSearch", para);
        }

        public virtual async Task<List<TLabPatientRegistration>> SearchLabRegistration(string str)
        {
            return await this._context.TLabPatientRegistrations
               .Where(x =>
                   (x.FirstName + " " + x.LastName).ToLower().StartsWith(str) || // Optional: if you want full name search
                   x.FirstName.ToLower().StartsWith(str) ||                     // Match first name starting with str
                   //x.RegNo.ToLower().StartsWith(str) ||                         // Match RegNo starting with str
                   x.MobileNo.ToLower().Contains(str)                           // Keep full Contains() for MobileNo
               )
               .Take(25)
               .Select(x => new TLabPatientRegistration
               {
                   FirstName = x.FirstName,
                   LabPatientId = x.LabPatientId,
                   LastName = x.LastName,
                   MiddleName = x.MiddleName,
                   MobileNo = x.MobileNo,
                   //RegNo = x.RegNo,
                   
                   AgeYear = x.AgeYear,
                   AgeMonth = x.AgeMonth,
                   AgeDay = x.AgeDay,
                   DateofBirth = x.DateofBirth
               })
              .OrderByDescending(x => x.MobileNo == str ? 2 : (x.FirstName + " " + x.LastName) == str ? 1 : 0)
              .ThenBy(x => x.FirstName).ToListAsync();
        }

        //public virtual async Task<List<TLabPatientRegistration>> SearchlabRegistration(string str)
        //{



        //    return await this._context.TLabPatientRegistrations
        //        .Where(x =>
        //            (x.FirstName + " " + x.LastName).ToLower().StartsWith(str) || // Optional: if you want full name search
        //            x.FirstName.ToLower().StartsWith(str) ||                     // Match first name starting with str
        //            //x.RegNo.ToLower().StartsWith(str) ||                         // Match RegNo starting with str
        //            x.MobileNo.ToLower().Contains(str)                           // Keep full Contains() for MobileNo
        //        )
        //        .Take(25)
        //        .Select(x => new TLabPatientRegistration
        //        {
        //            FirstName = x.FirstName,
        //            LabPatientId = x.LabPatientId,
        //            LastName = x.LastName,
        //            MiddleName = x.MiddleName,
        //            MobileNo = x.MobileNo,
        //            AgeYear = x.AgeYear,
        //            AgeMonth = x.AgeMonth,
        //            AgeDay = x.AgeDay,
        //            DateofBirth = x.DateofBirth
        //        })
        //       .OrderByDescending(x => x.MobileNo == str ? 2 : (x.FirstName + " " + x.LastName) == str ? 1 : 0)
        //       .ThenBy(x => x.FirstName).ToListAsync();

        //}

        public virtual async Task UpdateAsync(TLabPatientRegisteredMaster ObjTLabPatientRegistration, int UserId, string Username, string[]? ignoreColumns = null)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // 1. Attach the entity without marking everything as modified
                _context.Attach(ObjTLabPatientRegistration);
                _context.Entry(ObjTLabPatientRegistration).State = EntityState.Modified;
                // Always ignore LabRequestNo (auto-increment column)
                _context.Entry(ObjTLabPatientRegistration).Property(x => x.LabRequestNo).IsModified = false;

                // 2. Ignore specific columns
                if (ignoreColumns?.Length > 0)
                {
                    foreach (var column in ignoreColumns)
                    {
                        _context.Entry(ObjTLabPatientRegistration).Property(column).IsModified = false;
                    }
                }
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
    }
}
