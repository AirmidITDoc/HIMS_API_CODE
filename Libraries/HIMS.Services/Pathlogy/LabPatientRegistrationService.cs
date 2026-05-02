using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WkHtmlToPdfDotNet;


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
        public virtual async Task<IPagedList<LabResultListDto>> LabResultListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabResultListDto>(model, "ps_Rtrv_LabResultList");
        }
        public virtual async Task<IPagedList<LabResultDetailsListDto>> LabResultDetailsListAsynch(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabResultDetailsListDto>(model, "ps_Rtrv_LabResult_Detail_List");
        }
        public virtual async Task<IPagedList<LabPatientRegistrationListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabPatientRegistrationListDto>(model, "ps_LabPatientRegistrationList");
        }
        public virtual async Task<IPagedList<LabSampleCollectionListDto>> GetSamColListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabSampleCollectionListDto>(model, "ps_Rtrv_LabSamcollectionList");
        }
        public virtual async Task<IPagedList<LabSampleCollectionDetailListDto>> GetSamColListDetailAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabSampleCollectionDetailListDto>(model, "ps_Rtrv_LabSamcollection_Detail_List");
        }
        public virtual async Task<IPagedList<PrevDrVisistListDto>> GeOPPreviousDrVisitListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PrevDrVisistListDto>(model, "ps_Rtrv_PreviousLabDoctorVisitList");
        }
        public virtual async Task<IPagedList<LabResultPrintedListDto>> LabResultPrintedListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabResultPrintedListDto>(model, "ps_Rtrv_LabResultPrintedList");
        }
        public List<CompanyComboDto> CompanyComboList(string keywoard)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para =
             {
             new SqlParameter("@keywoard",string.IsNullOrWhiteSpace(keywoard) ? DBNull.Value : keywoard)
             };
            var data = sql.FetchListBySP<CompanyComboDto>("ps_Rtrv_CompanyMasterCombo", para);

            return data;
        }

        public async Task<List<MConstant>> GetMConstant(string ConstantType)
        {
            var data = await _context.MConstants
                        .Where(x => x.ConstantType == ConstantType)
                        .ToListAsync();
            return data;
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
        public virtual async Task InsertAsyncSP(TLabPatientRegistration ObjTLabPatientRegistration, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName)
        {

            try
            {

                DatabaseHelper odal = new();
                string[] rEntity = { "RegDate", "RegTime", "UnitId", "PrefixId", "FirstName", "MiddleName", "LastName", "GenderId", "MobileNo", "DateofBirth", "AgeYear", "AgeMonth", "AgeDay", "Address", "CityId",
                    "StateId", "CountryId", "PatientTypeId", "TariffId", "ClassId", "DepartmentId", "DoctorId", "RefDocId", "CreatedBy", "LabPatientId", "LabPatRegId", "AdharCardNo", "CompanyId", "SubCompanyId", "CampId",
                    "PatientType","Comments","ReferByName","CompanyExecutiveId","LabAppointmentId","HomeCollectionId"};

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
                    "IsSettled","IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","SpeTaxPer","SpeTaxAmt","CompDiscAmt","DiscComments"/*"CashCounterId"*/,"CreatedBy","BillNo","GovtApprovedAmt"};
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
                                //OpdIpdId = objItem1?.OpdIpdId,
                                OpdIpdId = objBill?.OpdIpdId,

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
                                CreatedDate = AppTime.Now,
                                UnitId = objBill.UnitId
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
                                //OpdIpdId = objItem1?.OpdIpdId,
                                OpdIpdId = objBill?.OpdIpdId,

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
                                CreatedDate = AppTime.Now,
                                UnitId = objBill.UnitId
                            };

                            _context.TRadiologyReportHeaders.Add(objRadio);
                            await _context.SaveChangesAsync();
                        }
                        // Is othertest Code
                        if (objItem1?.IsOtherService == true)
                        {
                            TRadiologyReportHeader objRadio = new()
                            {
                                RadDate = objItem1.ChargesDate,
                                RadTime = objItem1?.ChargesTime,
                                OpdIpdType = objItem1?.OpdIpdType,
                                //OpdIpdId = objItem1?.OpdIpdId,
                                OpdIpdId = objBill?.OpdIpdId,

                                RadTestId = objItem1?.ServiceId,
                                AddedBy = objItem1?.AddedBy,
                                ChargeId = objItem1?.ChargesId,
                                IsCompleted = false,
                                IsCancelled = 0,
                                IsPrinted = false,
                                TestType = true,
                                PatientName = objBill.PatientName,
                                RegNo = objBill.RegNo.ToString(),
                                Opipnumber = objBill.Ipdno,
                                DoctorName = objBill.DoctorName,
                                CreatedBy = CurrentUserId,
                                CreatedDate = AppTime.Now,
                                UnitId = objBill.UnitId
                            };

                            _context.TRadiologyReportHeaders.Add(objRadio);
                            await _context.SaveChangesAsync();
                        }
                        if (objItem1.IsPackage == 1)
                        {
                            foreach (var item in ObjaddCharge)
                            {
                                string[] AEntity = { "ChargesId", "ChargesDate", "OpdIpdType","OpdIpdId", "ServiceId", "Price", "Qty", "UnitId", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DoctorName",
                                    "DocPercentage", "DocAmt", "HospitalAmt", "RefundAmount", "IsGenerated", "IsComServ", "IsPrintCompSer", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology",
                                    "IsPackage", "WardId", "BedId", "ServiceCode", "ServiceName", "CompanyServiceName", "IsInclusionExclusion", "IsHospMrk", "PackageMainChargeID", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId", "TariffId", "BillNo", "CreatedBy","IsOtherService" };
                                var Packagescharge = item.ToDictionary();

                                foreach (var rProperty in Packagescharge.Keys.ToList())
                                {
                                    if (!AEntity.Contains(rProperty))
                                        Packagescharge.Remove(rProperty);
                                }
                                Packagescharge["PackageMainChargeId"] = objItem1.ChargesId;
                                Packagescharge["OpdIpdId"] = objBill.OpdIpdId;
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
                        foreach (var item in ObjTPayment)
                        {
                            string[] PEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
                              "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};

                            TPayment objTPay = new();
                            objTPay = item;
                            objTPay.BillNo = objBill.BillNo;

                            var pentity = item.ToDictionary();
                            foreach (var rProperty in pentity.Keys.ToList())
                            {
                                if (!PEntity.Contains(rProperty))
                                    pentity.Remove(rProperty);
                            }
                            string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_Payment", CommandType.StoredProcedure, "PaymentId", pentity);
                            item.PaymentId = Convert.ToInt32(VPaymentId);
                            await _context.LogProcedureExecution(pentity, nameof(TPayment), Convert.ToInt32(item.PaymentId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                        }
                    }

                    if (objBill.BillNo > 0)
                    {
                        Dictionary<string, object> param = new()
                        {
                            ["BillNo"] = objBill.BillNo
                        };
                        odal.ExecuteNonQuery("Cal_DiscAmount_Bill", CommandType.StoredProcedure, param);
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
        public virtual async Task InsertPaidBillAsync(TLabPatientRegistration ObjTLabPatientRegistration, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName, CancellationToken cancellationToken = default)
        {
            // Whitelists declared once (avoid per-call allocation churn)
            var rEntity = new HashSet<string>(StringComparer.Ordinal)
    {
        "RegDate","RegTime","UnitId","PrefixId","FirstName","MiddleName","LastName","GenderId","MobileNo",
        "DateofBirth","AgeYear","AgeMonth","AgeDay","Address","CityId","StateId","CountryId","PatientTypeId",
        "TariffId","ClassId","DepartmentId","DoctorId","RefDocId","CreatedBy","LabPatientId","LabPatRegId",
        "AdharCardNo","CompanyId","SubCompanyId","CampId","PatientType","Comments","ReferByName",
        "CompanyExecutiveId","LabAppointmentId","HomeCollectionId"
    };

            var BEntity = new HashSet<string>(StringComparer.Ordinal)
    {
        "OpdIpdId","RegNo","PatientName","Ipdno","AgeYear","AgeMonth","AgeDays","DoctorId","DoctorName",
        "WardId","BedId","PatientType","CompanyName","CompanyAmt","PatientAmt","TotalAmt","ConcessionAmt",
        "NetPayableAmt","PaidAmt","BalanceAmt","BillDate","OpdIpdType","AddedBy","TotalAdvanceAmount",
        "AdvanceUsedAmount","BillTime","ConcessionReasonId","IsSettled","IsPrinted","IsFree","CompanyId",
        "TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","SpeTaxPer",
        "SpeTaxAmt","CompDiscAmt","DiscComments","CreatedBy","BillNo","GovtApprovedAmt"
    };

            var AEntity = new HashSet<string>(StringComparer.Ordinal)
    {
        "ChargesId","ChargesDate","OpdIpdType","OpdIpdId","ServiceId","Price","Qty","UnitId","TotalAmt",
        "ConcessionPercentage","ConcessionAmount","NetAmount","DoctorId","DoctorName","DocPercentage",
        "DocAmt","HospitalAmt","RefundAmount","IsGenerated","IsComServ","IsPrintCompSer","AddedBy",
        "IsCancelled","IsCancelledBy","IsCancelledDate","IsPathology","IsRadiology","IsPackage","WardId",
        "BedId","ServiceCode","ServiceName","CompanyServiceName","IsInclusionExclusion","IsHospMrk",
        "PackageMainChargeID","IsSelfOrCompanyService","PackageId","ChargesTime","ClassId","TariffId",
        "BillNo","CreatedBy","IsOtherService"
    };

            var rPaymentEntity = new HashSet<string>(StringComparer.Ordinal)
    {
        "PaymentId","UnitId","BillNo","ReceiptNo","PaymentDate","PaymentTime","CashPayAmount",
        "ChequePayAmount","ChequeNo","BankName","ChequeDate","CardPayAmount","CardNo","CardBankName",
        "CardDate","AdvanceUsedAmount","AdvanceId","RefundId","TransactionType","Remark","AddBy",
        "IsCancelled","SalesId","IsCancelledBy","IsCancelledDate","NeftpayAmount","Neftno",
        "NeftbankMaster","Neftdate","PayTmamount","PayTmtranNo","PayTmdate","Tdsamount","Wfamount","CompanyId"
    };

            var PEntity = new HashSet<string>(StringComparer.Ordinal)
    {
        "PaymentId","UnitId","BillNo","Opdipdtype","PaymentDate","PaymentTime","PayAmount","TranNo",
        "BankName","ValidationDate","AdvanceUsedAmount","Comments","PayMode","OnlineTranNo",
        "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType",
        "IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"
    };

            // Open ONE connection, use ONE local transaction. No TransactionScope, no MSDTC.
            // Use the EF Core context's connection so EF and ADO.NET share the same transaction.
            var dbConnection = _context.Database.GetDbConnection();
            var wasClosed = dbConnection.State != ConnectionState.Open;

            if (wasClosed)
                await dbConnection.OpenAsync(cancellationToken);

            // Use READ COMMITTED + small timeout to FAIL FAST instead of hanging
            using var efTransaction = await _context.Database.BeginTransactionAsync(
                System.Data.IsolationLevel.ReadCommitted, cancellationToken);

            try
            {
                var odal = new DatabaseHelper();
                odal.SetConnection(dbConnection);
                odal.SetTransaction(efTransaction.GetDbTransaction());

                // Tell EF to use the same transaction (already done by BeginTransactionAsync on context)

                // ---------- 1) Insert LabPatientRegistration ----------
                var lentity = ObjTLabPatientRegistration.ToDictionary();
                FilterDict(lentity, rEntity);

                string vLabPatientId = odal.ExecuteNonQueryNew("ps_Insert_LabPatientRegistration", CommandType.StoredProcedure, "LabPatientId", lentity);

                ObjTLabPatientRegistration.LabPatientId = Convert.ToInt32(vLabPatientId);

                await _context.LogProcedureExecution(lentity, nameof(TLabPatientRegistration), ObjTLabPatientRegistration.LabPatientId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

                objBill.OpdIpdId = ObjTLabPatientRegistration.LabPatientId;

                // ---------- 2) Insert Bill ----------
                var bentity = objBill.ToDictionary();
                FilterDict(bentity, BEntity);

                string vBillNo = odal.ExecuteNonQueryNew("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", bentity);

                objBill.BillNo = Convert.ToInt32(vBillNo);

                await _context.LogProcedureExecution(bentity, nameof(Bill), objBill.BillNo.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                // ---------- 3) Add Charges + Bill Details + Pathology/Radiology + Package ----------
                // Stage all EF entities first, SaveChanges ONCE at the end of the EF block.
                foreach (var objItem1 in objBill.AddCharges)
                {
                    objItem1.BillNo = objBill.BillNo;
                    objItem1.ChargesDate = Convert.ToDateTime(objItem1.ChargesDate);
                    objItem1.IsCancelledDate = Convert.ToDateTime(objItem1.IsCancelledDate);
                    objItem1.ChargesTime = Convert.ToDateTime(objItem1.ChargesTime);
                    objItem1.OpdIpdId = ObjTLabPatientRegistration.LabPatientId;

                    _context.AddCharges.Add(objItem1);
                }

                // SaveChanges so AddCharges get their identity IDs (needed for BillDetails / Patho / Radio)
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

                // Now stage dependent rows in bulk
                foreach (var objItem1 in objBill.AddCharges)
                {
                    _context.BillDetails.Add(new BillDetail
                    {
                        BillNo = objBill.BillNo,
                        ChargesId = objItem1?.ChargesId
                    });

                    if (objItem1?.IsPathology == 1)
                    {
                        _context.TPathologyReportHeaders.Add(new TPathologyReportHeader
                        {
                            PathDate = objItem1.ChargesDate,
                            PathTime = objItem1.ChargesTime,
                            OpdIpdType = objItem1.OpdIpdType,
                            OpdIpdId = objBill.OpdIpdId,
                            PathTestId = objItem1.ServiceId,
                            AddedBy = objItem1.AddedBy,
                            ChargeId = objItem1.ChargesId,
                            IsCompleted = false,
                            IsPrinted = false,
                            IsSampleCollection = false,
                            TestType = false,
                            PatientName = objBill.PatientName,
                            RegNo = objBill.RegNo.ToString(),
                            Opipnumber = objBill.Ipdno,
                            DoctorName = objBill.DoctorName,
                            CreatedBy = CurrentUserId,
                            CreatedDate = AppTime.Now,
                            UnitId = objBill.UnitId
                        });
                    }

                    if (objItem1?.IsRadiology == 1 || objItem1?.IsOtherService == true)
                    {
                        _context.TRadiologyReportHeaders.Add(new TRadiologyReportHeader
                        {
                            RadDate = objItem1.ChargesDate,
                            RadTime = objItem1.ChargesTime,
                            OpdIpdType = objItem1.OpdIpdType,
                            OpdIpdId = objItem1.OpdIpdId,
                            RadTestId = objItem1.ServiceId,
                            AddedBy = objItem1.AddedBy,
                            ChargeId = objItem1.ChargesId,
                            IsCompleted = false,
                            IsCancelled = 0,
                            IsPrinted = false,
                            TestType = objItem1.IsOtherService == true,
                            PatientName = objBill.PatientName,
                            RegNo = objBill.RegNo.ToString(),
                            Opipnumber = objBill.Ipdno,
                            DoctorName = objItem1.DoctorName,
                            CreatedBy = CurrentUserId,
                            CreatedDate = AppTime.Now,
                            UnitId = objItem1.UnitId
                        });
                    }

                    // Package items via SP (still on the same connection + transaction)
                    if (objItem1?.IsPackage == 1 && ObjaddCharge != null)
                    {
                        foreach (var item in ObjaddCharge)
                        {
                            var packageDict = item.ToDictionary();
                            FilterDict(packageDict, AEntity);

                            packageDict["PackageMainChargeId"] = objItem1.ChargesId;
                            packageDict["OpdIpdId"] = objBill.OpdIpdId;
                            packageDict["BillNo"] = objBill.BillNo;

                            var vChargesId = odal.ExecuteNonQueryNew("ps_insert_AddChargesPackages_1", CommandType.StoredProcedure, "ChargesId", packageDict);

                            item.ChargesId = Convert.ToInt32(vChargesId);

                            var billDetParam = new Dictionary<string, object>
                            {
                                ["BillNo"] = objBill.BillNo,
                                ["ChargesId"] = vChargesId
                            };

                            odal.ExecuteNonQueryNew("ps_insert_BillDetails_1", CommandType.StoredProcedure, null, billDetParam);
                        }
                    }
                }

                // Single SaveChanges for BillDetails / Patho / Radio batch
                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

                // ---------- 4) Calculate Discount ----------
                if (objBill.BillNo > 0)
                {
                    odal.ExecuteNonQueryNew("Cal_DiscAmount_Bill", CommandType.StoredProcedure, null, new Dictionary<string, object> { ["BillNo"] = objBill.BillNo });
                }

                // ---------- 5) Insert Payment header ----------
                objPayment.BillNo = objBill.BillNo;
                var paymentDict = objPayment.ToDictionary();
                FilterDict(paymentDict, rPaymentEntity);
                paymentDict["OPDIPDType"] = 4;

                string paymentId = odal.ExecuteNonQueryNew("ps_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", paymentDict);

                objPayment.PaymentId = Convert.ToInt32(paymentId);

                // ---------- 6) Insert TPayment rows ----------
                if (ObjTPayment != null)
                {
                    List<Dictionary<string, object>> paymentLogs = new();
                    foreach (var item in ObjTPayment)
                    {
                        item.BillNo = objBill.BillNo;
                        var pentity = item.ToDictionary();
                        FilterDict(pentity, PEntity);

                        string vPaymentId = odal.ExecuteNonQueryNew("ps_insert_T_Payment", CommandType.StoredProcedure, "PaymentId", pentity);

                        item.PaymentId = Convert.ToInt32(vPaymentId);
                        paymentLogs.Add(pentity);

                    }
                    if (paymentLogs.Count > 0)
                        await _context.LogProcedureExecution(paymentLogs, nameof(TPayment), objBill.BillNo, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
                }

                // ---------- COMMIT ----------
                await efTransaction.CommitAsync(cancellationToken);
            }
            catch
            {
                // Rollback EVERYTHING (LabPatient, Bill, Charges, Payments) atomically
                try { await efTransaction.RollbackAsync(cancellationToken); } catch { /* swallow rollback errors */ }
                throw; // surface the real error to the caller — never swallow silently
            }
            finally
            {
                if (wasClosed && dbConnection.State == ConnectionState.Open)
                    await dbConnection.CloseAsync();
            }

            // Local helper
            static void FilterDict(Dictionary<string, object> dict, HashSet<string> allowed)
            {
                foreach (var key in dict.Keys.ToList())
                    if (!allowed.Contains(key))
                        dict.Remove(key);
            }
        }
        //public virtual async Task InsertPaidBillAsync(TLabPatientRegistration ObjTLabPatientRegistration, Bill objBill, Payment objPayment, List<AddCharge> ObjaddCharge, List<TPayment> ObjTPayment, int CurrentUserId, string CurrentUserName)
        //{

        //    try
        //    {

        //        DatabaseHelper odal = new();
        //        string[] rEntity = { "RegDate", "RegTime", "UnitId", "PrefixId", "FirstName", "MiddleName", "LastName", "GenderId", "MobileNo", "DateofBirth", "AgeYear", "AgeMonth", "AgeDay", "Address",
        //            "CityId", "StateId", "CountryId", "PatientTypeId", "TariffId", "ClassId", "DepartmentId", "DoctorId", "RefDocId", "CreatedBy", "LabPatientId", "LabPatRegId","AdharCardNo", "CompanyId", "SubCompanyId", "CampId",
        //            "PatientType","Comments","ReferByName","CompanyExecutiveId","LabAppointmentId","HomeCollectionId"};

        //        var lentity = ObjTLabPatientRegistration.ToDictionary();
        //        foreach (var rProperty in lentity.Keys.ToList())
        //        {
        //            if (!rEntity.Contains(rProperty))
        //                lentity.Remove(rProperty);
        //        }
        //        string VLabPatientId = odal.ExecuteNonQuery("ps_Insert_LabPatientRegistration", CommandType.StoredProcedure, "LabPatientId", lentity);
        //        ObjTLabPatientRegistration.LabPatientId = Convert.ToInt32(VLabPatientId);
        //        objBill.OpdIpdId = ObjTLabPatientRegistration.LabPatientId;



        //        string[] BEntity = { "OpdIpdId", "RegNo",  "PatientName", "Ipdno", "AgeYear", "AgeMonth", "AgeDays", "DoctorId", "DoctorName", "WardId", "BedId","PatientType", "CompanyName", "CompanyAmt",
        //            "PatientAmt","TotalAmt","ConcessionAmt","NetPayableAmt","PaidAmt","BalanceAmt","BillDate","OpdIpdType","AddedBy","TotalAdvanceAmount","AdvanceUsedAmount","BillTime","ConcessionReasonId",
        //            "IsSettled","IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","SpeTaxPer","SpeTaxAmt","CompDiscAmt","DiscComments"/*"CashCounterId"*/,"CreatedBy","BillNo","GovtApprovedAmt"};
        //        var bentity = objBill.ToDictionary();
        //        foreach (var rProperty in bentity.Keys.ToList())
        //        {
        //            if (!BEntity.Contains(rProperty))
        //                bentity.Remove(rProperty);
        //        }
        //        string vBillNo = odal.ExecuteNonQuery("ps_insert_Bill_1", CommandType.StoredProcedure, "BillNo", bentity);
        //        objBill.BillNo = Convert.ToInt32(vBillNo);


        //        //Vimal need to cck
        //        using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //        {

        //            foreach (var objItem1 in objBill.AddCharges)
        //            {
        //                // Add Charges Code
        //                objItem1.BillNo = objBill.BillNo;
        //                objItem1.ChargesDate = Convert.ToDateTime(objItem1.ChargesDate);
        //                objItem1.IsCancelledDate = Convert.ToDateTime(objItem1.IsCancelledDate);
        //                objItem1.ChargesTime = Convert.ToDateTime(objItem1.ChargesTime);
        //                objItem1.OpdIpdId = ObjTLabPatientRegistration.LabPatientId;


        //                _context.AddCharges.Add(objItem1);
        //                await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);

        //                // Bill Details Code
        //                BillDetail objBillDet = new()
        //                {
        //                    BillNo = objBill.BillNo,
        //                    ChargesId = objItem1?.ChargesId
        //                };
        //                _context.BillDetails.Add(objBillDet);
        //                // await _context.SaveChangesAsync();

        //                // Pathology Code
        //                if (objItem1.IsPathology == 1)
        //                {
        //                    TPathologyReportHeader objPatho = new()
        //                    {
        //                        PathDate = objItem1.ChargesDate,
        //                        PathTime = objItem1?.ChargesTime,
        //                        OpdIpdType = objItem1?.OpdIpdType,

        //                        //OpdIpdId = objItem1?.OpdIpdId,
        //                        OpdIpdId = objBill?.OpdIpdId,

        //                        PathTestId = objItem1?.ServiceId,
        //                        AddedBy = objItem1?.AddedBy,
        //                        ChargeId = objItem1?.ChargesId,
        //                        IsCompleted = false,
        //                        IsPrinted = false,
        //                        IsSampleCollection = false,
        //                        TestType = false,
        //                        PatientName = objBill.PatientName,
        //                        RegNo = objBill.RegNo.ToString(),
        //                        Opipnumber = objBill.Ipdno,
        //                        DoctorName = objBill.DoctorName,
        //                        CreatedBy = CurrentUserId,
        //                        CreatedDate = AppTime.Now,
        //                        UnitId = objBill.UnitId,

        //                    };

        //                    _context.TPathologyReportHeaders.Add(objPatho);
        //                    // await _context.SaveChangesAsync();
        //                }
        //                // Radiology + OtherService (merged)
        //                if (objItem1?.IsRadiology == 1 || objItem1?.IsOtherService == true)
        //                {
        //                    var objRadio = new TRadiologyReportHeader
        //                    {
        //                        RadDate = objItem1?.ChargesDate,
        //                        RadTime = objItem1?.ChargesTime,
        //                        OpdIpdType = objItem1?.OpdIpdType,
        //                        OpdIpdId = objItem1?.OpdIpdId,
        //                        RadTestId = objItem1?.ServiceId,
        //                        AddedBy = objItem1?.AddedBy,
        //                        ChargeId = objItem1?.ChargesId,
        //                        IsCompleted = false,
        //                        IsCancelled = 0,
        //                        IsPrinted = false,
        //                        TestType = objItem1?.IsOtherService == true, // only difference
        //                        PatientName = objBill?.PatientName,
        //                        RegNo = objBill?.RegNo.ToString(),
        //                        Opipnumber = objBill?.Ipdno,
        //                        DoctorName = objItem1?.DoctorName,
        //                        CreatedBy = CurrentUserId,
        //                        CreatedDate = AppTime.Now,
        //                        UnitId = objItem1?.UnitId
        //                    };

        //                    _context.TRadiologyReportHeaders.Add(objRadio);
        //                }
        //                if (objItem1.IsPackage == 1)
        //                {
        //                    foreach (var item in ObjaddCharge)
        //                    {
        //                        string[] AEntity = { "ChargesId", "ChargesDate", "OpdIpdType","OpdIpdId", "ServiceId", "Price", "Qty", "UnitId", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DoctorName", "DocPercentage",
        //                            "DocAmt", "HospitalAmt", "RefundAmount", "IsGenerated", "IsComServ", "IsPrintCompSer", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "WardId", "BedId",
        //                            "ServiceCode", "ServiceName", "CompanyServiceName", "IsInclusionExclusion", "IsHospMrk", "PackageMainChargeID", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId", "TariffId", "BillNo", "CreatedBy","IsOtherService" };
        //                        var Packagescharge = item.ToDictionary();

        //                        foreach (var rProperty in Packagescharge.Keys.ToList())
        //                        {
        //                            if (!AEntity.Contains(rProperty))
        //                                Packagescharge.Remove(rProperty);
        //                        }
        //                        Packagescharge["PackageMainChargeId"] = objItem1.ChargesId;
        //                        Packagescharge["OpdIpdId"] = objBill.OpdIpdId;
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
        //            await _context.SaveChangesAsync(CurrentUserId, CurrentUserName);
        //            if (objBill.BillNo > 0)
        //            {
        //                Dictionary<string, object> param = new()
        //                {
        //                    ["BillNo"] = objBill.BillNo
        //                };
        //                odal.ExecuteNonQuery("Cal_DiscAmount_Bill", CommandType.StoredProcedure, param);
        //            }

        //            string[] rPaymentEntity = { "PaymentId", "UnitId", "BillNo", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount",
        //                "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "SalesId", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "CompanyId" };
        //            Payment objPay = new();
        //            objPay = objPayment;
        //            objPay.BillNo = objBill.BillNo;
        //            var entity2 = objPayment.ToDictionary();
        //            foreach (var rProperty in entity2.Keys.ToList())
        //            {
        //                if (!rPaymentEntity.Contains(rProperty))
        //                    entity2.Remove(rProperty);
        //            }
        //            entity2["OPDIPDType"] = 4; // Ensure objpayment has OPDIPDType
        //            string PaymentId = odal.ExecuteNonQuery("ps_Commoninsert_Payment_1", CommandType.StoredProcedure, "PaymentId", entity2);
        //            objPayment.PaymentId = Convert.ToInt32(PaymentId);

        //            foreach (var item in ObjTPayment)
        //            {

        //                string[] PEntity = { "PaymentId", "UnitId",  "BillNo", "Opdipdtype", "PaymentDate", "PaymentTime", "PayAmount", "TranNo", "BankName", "ValidationDate", "AdvanceUsedAmount","Comments", "PayMode", "OnlineTranNo",
        //                                   "OnlineTranResponse","CompanyId","AdvanceId","RefundId","CashCounterId","TransactionType","IsSelfOrcompany","TranMode","CreatedBy","TransactionLabel"};

        //                TPayment objTPay = new();
        //                objTPay = item;
        //                objTPay.BillNo = objBill.BillNo;

        //                var pentity = item.ToDictionary();
        //                foreach (var rProperty in pentity.Keys.ToList())
        //                {
        //                    if (!PEntity.Contains(rProperty))
        //                        pentity.Remove(rProperty);
        //                }
        //                string VPaymentId = odal.ExecuteNonQuery("ps_insert_T_Payment", CommandType.StoredProcedure, "PaymentId", pentity);
        //                item.PaymentId = Convert.ToInt32(VPaymentId);
        //                await _context.LogProcedureExecution(pentity, nameof(TPayment), Convert.ToInt32(item.PaymentId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


        //            }
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


        public List<LabVisitDetailsListSearchDto> SearchlabRegistration(long UnitId, string Keyword)
        {
            DatabaseHelper sql = new();

            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@UnitId", UnitId);
            para[1] = new SqlParameter("@Keyword", Keyword);

            return sql.FetchListBySP<LabVisitDetailsListSearchDto>("ps_Rtrv_PatientLabRegisteredListSearch", para);
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


        //public virtual async Task UpdateAsync(TLabPatientRegisteredMaster ObjTLabPatientRegistration, int UserId, string Username, string[]? ignoreColumns = null)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // 1. Attach the entity without marking everything as modified
        //        _context.Attach(ObjTLabPatientRegistration);
        //        _context.Entry(ObjTLabPatientRegistration).State = EntityState.Modified;
        //        // Always ignore LabRequestNo (auto-increment column)
        //        _context.Entry(ObjTLabPatientRegistration).Property(x => x.LabRequestNo).IsModified = false;

        //        // 2. Ignore specific columns
        //        if (ignoreColumns?.Length > 0)
        //        {
        //            foreach (var column in ignoreColumns)
        //            {
        //                _context.Entry(ObjTLabPatientRegistration).Property(column).IsModified = false;
        //            }
        //        }
        //        await _context.SaveChangesAsync();
        //        scope.Complete();
        //    }
        //}

        public virtual async Task UpdateAsync(TLabPatientRegisteredMaster ObjTLabPatientRegistrationMaster, TLabPatientRegistration ObjTLabPatientRegistration, int CurrentUserId, string CurrentUserName)
        {


            DatabaseHelper odal = new();
            string[] Entity = { "LabPatRegId", "RegDate", "RegTime", "UnitId", "PrefixId", "FirstName", "MiddleName", "LastName", "GenderId", "MobileNo", "DateofBirth", "AgeYear", "AgeMonth", "AgeDay", "Address", "AdharCardNo", "CityId", "StateId", "CountryId", "ModifiedBy" };

            var entity = ObjTLabPatientRegistrationMaster.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!Entity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_UpdateT_LabPatientRegisteredMaster", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(TLabPatientRegisteredMaster), Convert.ToInt32(ObjTLabPatientRegistrationMaster.LabPatRegId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


            string[] LEntity = { "LabPatientId", "UnitId", "PatientTypeId", "TariffId", "DoctorId", "RefDocId", "CompanyId", "PatientType", "Comments", "ReferByName", "ModifiedBy" };

            var lEntity = ObjTLabPatientRegistration.ToDictionary();
            foreach (var rProperty in lEntity.Keys.ToList())
            {
                if (!LEntity.Contains(rProperty))
                    lEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_T_LabPatientRegistration", CommandType.StoredProcedure, lEntity);
            await _context.LogProcedureExecution(entity, nameof(TLabPatientRegistration), Convert.ToInt32(ObjTLabPatientRegistration.LabPatientId), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


        }

        public virtual async Task<IPagedList<WhatsAppsendOutListDto>> GetLabPatientWhatsAppconfig(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<WhatsAppsendOutListDto>(model, "ps_Rtrv_LabPatienSMShistory");
        }

        public virtual async Task<IPagedList<EmailSendoutListDto>> GetLabPatientEmailSconfig(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<EmailSendoutListDto>(model, "ps_Rtrv_T_LabPatientEmailOutgoinglist");
        }

        public virtual async Task<IPagedList<LabResultDetailsListDto>> LabApprovalResultListAsync(GridRequestModel model)
        {

            return await DatabaseHelper.GetGridDataBySp<LabResultDetailsListDto>(model, "ps_Rtrv_LabLabApprovalList");
        }





        public virtual async Task<IPagedList<LabDiscountDetailListDto>> LabDiscountDetailListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabDiscountDetailListDto>(model, "ps_Rtrv_LabRegisterTestDiscountDetail");
        }

        public virtual async Task<IPagedList<LabPaymentDetailListDto>> LabPaymentDetailListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabPaymentDetailListDto>(model, "ps_Rtrv_LabRegisterPaymentDetail");
        }

        public virtual async Task<IPagedList<LabCreditDetailDto>> LabCreditDetailListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LabCreditDetailDto>(model, "ps_Rtrv_LabRegisterCreditDetail");
        }
        public List<ServiceMasterDTO> SearchLabServiceListwithTraiff(int TariffId, int ClassId, int GroupId, int SubGroupId, string SrvcName)
        {
            DatabaseHelper sql = new();
            SqlParameter[] para = new SqlParameter[5];

            para[0] = new SqlParameter("@TariffId", TariffId);
            para[1] = new SqlParameter("@ClassId", ClassId);
            para[2] = new SqlParameter("@GroupId", GroupId);
            para[3] = new SqlParameter("@SubGroupId", SubGroupId);
            para[4] = new SqlParameter("@SrvcName", SrvcName);

            List<ServiceMasterDTO> lstServiceList = sql.FetchListBySP<ServiceMasterDTO>("ps_Rtrv_LabServicesList ", para);
            return lstServiceList;
        }
        public virtual async Task InsertAsync(TDiscApprovalDetail ObjTDiscApprovalDetail, int CurrentUserId, string CurrentUserName)
        {


            DatabaseHelper odal = new();
            string[] Entity = { "DiscSeqId", "Opipid", "Opiptype", "BillNo", "RequestAmount", "ApprovedAmount", "AppovedBy", "ApprovedDateTime", "Comments", "IsActive" };

            var entity = ObjTDiscApprovalDetail.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!Entity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string DiscSeqId = odal.ExecuteNonQuery("ps_Insert_T_DiscApprovalDetails", CommandType.StoredProcedure, "DiscSeqId", entity);
            ObjTDiscApprovalDetail.DiscSeqId = Convert.ToInt32(DiscSeqId);
            await _context.LogProcedureExecution(entity, nameof(TDiscApprovalDetail), Convert.ToInt32(ObjTDiscApprovalDetail.DiscSeqId), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);


        }
    }
}
