using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.OPPatient
{
    public class OPCreditBillService : IOPCreditBillService
    {
        private readonly HIMSDbContext _context;
        public OPCreditBillService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
      
      
         public virtual async Task InsertAsyncSP(Bill objBill, List<AddCharge> ObjaddCharge, int CurrentUserId, string CurrentUserName)
         {
         try
         {
            // Bill Code
                DatabaseHelper odal = new();
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
                            PathTime = objItem1?.ChargesTime,
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
                            RadTime = objItem1?.ChargesTime,
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

                        };

                        _context.TRadiologyReportHeaders.Add(objRadio);
                        await _context.SaveChangesAsync();
                    }
                    if (objItem1.IsPackage == 1)
                    {
                        foreach (var item in ObjaddCharge)
                        {
                                string[] AEntity = { "ChargesId", "ChargesDate", "OpdIpdType", "OpdIpdId", "ServiceId", "Price", "Qty", "UnitId", "TotalAmt", "ConcessionPercentage", "ConcessionAmount", "NetAmount", "DoctorId", "DoctorName", "DocPercentage", "DocAmt", "HospitalAmt", "RefundAmount", "IsGenerated", "IsComServ", "IsPrintCompSer", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "IsPathology", "IsRadiology", "IsPackage", "WardId", "BedId", "ServiceCode", "ServiceName", "CompanyServiceName", "IsInclusionExclusion", "IsHospMrk", "PackageMainChargeID", "IsSelfOrCompanyService", "PackageId", "ChargesTime", "ClassId", "TariffId", "BillNo", "CreatedBy" };
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

                            odal.ExecuteNonQuery("m_insert_BillDetails_1", CommandType.StoredProcedure, OPBillDet2);
                        }

                    }

                }


                scope.Complete();
            }
         }


        catch (Exception)
        {
            Bill? objBills = await _context.Bills.FindAsync(objBill.BillNo);
            _context.Bills.Remove(objBills);
            await _context.SaveChangesAsync();
        }
    }
}
}

