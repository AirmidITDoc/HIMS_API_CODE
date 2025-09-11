using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Administration
{
    public class AdministrationService : IAdministrationService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public AdministrationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<BrowseOPDBillPagiListDto>> BrowseOPDBillPagiList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseOPDBillPagiListDto>(model, "ps_Rtrv_BrowseOPDBill_Pagi");
        }

        public virtual async Task<IPagedList<RoleMasterListDto>> RoleMasterList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RoleMasterListDto>(model, "m_Rtrv_Rolemaster");
        }

        public virtual async Task<IPagedList<PaymentModeDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PaymentModeDto>(model, "Retrieve_BrowseIPAdvPaymentReceipt");
        }
        public virtual async Task<IPagedList<BrowseIPAdvPayPharReceiptListDto>> BrowseIPAdvPayPharReceiptList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<BrowseIPAdvPayPharReceiptListDto>(model, "Retrieve_BrowseIPAdvPayPharReceipt");
        }

        public virtual async Task<IPagedList<ReportTemplateListDto>> BrowseReportTemplateList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<ReportTemplateListDto>(model, "m_Rtrv_ReportTemplateConfig");
        }

        public virtual async Task<IPagedList<DailyExpenceListtDto>> DailyExpencesList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DailyExpenceListtDto>(model, "m_Rtrv_T_Expenses");
        }

        public virtual async Task InsertAsync(TExpense ObjTExpense, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TExpenses.Add(ObjTExpense);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateExpensesAsync(TExpense ObjTExpense, int UserId, string Username, string[] strings)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.TExpenses.Update(ObjTExpense);
                _context.Entry(ObjTExpense).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task TExpenseCancel(TExpense ObjTExpense, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "ExpDate", "ExpTime", "ExpType", "ExpAmount", "PersonName", "Narration", "IsAddedby", "IsCancelled", "VoucharNo", "ExpHeadId" };
            var entity = ObjTExpense.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("m_Update_T_Expenses_IsCancel", CommandType.StoredProcedure, entity);

        }
        public virtual async Task DeleteAsync(Admission ObjAdmission, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "RegId", "AdmissionDate", "AdmissionTime", "PatientTypeId", "HospitalId", "DocNameId", "RefDocNameId", "WardId", "BedId", "DischargeDate", "DischargeTime", "IsDischarged", "IsBillGenerated", "Ipdno", "IsCancelled", "CompanyId", "TariffId",
            "ClassId","DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc","MotherName","AdmittedDoctor1","AdmittedDoctor2","IsProcessing",
            "Ischarity","RefByTypeId","RefByName","IsMarkForDisNur","IsMarkForDisNurId","IsMarkForDisNurDateTime","IsCovidFlag","IsCovidUserId","IsCovidUpdateDate",
            "IsUpdatedBy","SubTpaComId","PolicyNo","AprovAmount","CompDod","IsPharClearance","Ipnumber","EstimatedAmount","ApprovedAmount","HosApreAmt","PathApreAmt","PharApreAmt","RadiApreAmt","PharDisc","CompBillNo","CompBillDate","CompDiscount","CompDisDate","CBillNo","CFinalBillAmt","CDisallowedAmt","ClaimNo","HdiscAmt","COutsideInvestAmt","RecoveredByPatient","HChargeAmt",
            "HAdvAmt","HBillId","HBillDate","HBillNo","HTotalAmt","HDiscAmt1","HNetAmt","HPaidAmt","HBalAmt","IsOpToIpconv","RefDoctorDept","AdmissionType","MedicalApreAmt","AdminPer","AdminAmt","SubTpacomp","IsCtoH","IsInitinatedDischarge"};
            var entity = ObjAdmission.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("IP_DISCHARGE_CANCELLATION", CommandType.StoredProcedure, entity);

        }
        public virtual async Task UpdateAsync(Admission ObjAdmission, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "RegId", "PatientTypeId", "HospitalId", "DocNameId", "RefDocNameId","Ipdno", "WardId", "BedId", "DischargeDate", "DischargeTime", "IsDischarged", "IsBillGenerated", "IPDNo", "IsCancelled", "CompanyId", "TariffId",
            "ClassId","DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc","MotherName","AdmittedDoctor1","AdmittedDoctor2","IsProcessing",
            "Ischarity","RefByTypeId","RefByName","IsMarkForDisNur","IsMarkForDisNurId","IsMarkForDisNurDateTime","IsCovidFlag","IsCovidUserId","IsCovidUpdateDate",
            "IsUpdatedBy","SubTpaComId","PolicyNo","AprovAmount","CompDod","IsPharClearance","Ipnumber","EstimatedAmount","ApprovedAmount","HosApreAmt","PathApreAmt","PharApreAmt","RadiApreAmt","PharDisc","CompBillNo","CompBillDate","CompDiscount","CompDisDate","CBillNo","CFinalBillAmt","CDisallowedAmt","ClaimNo","HdiscAmt","COutsideInvestAmt","RecoveredByPatient","HChargeAmt",
            "HAdvAmt","HBillId","HBillDate","HBillNo","HTotalAmt","HDiscAmt1","HNetAmt","HPaidAmt","HBalAmt","IsOpToIpconv","RefDoctorDept","AdmissionType","MedicalApreAmt","AdminPer","AdminAmt","SubTpacomp","IsCtoH","IsInitinatedDischarge","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var Rentity = ObjAdmission.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("Update_Admissiondatetime", CommandType.StoredProcedure, Rentity);

        }

        public virtual async Task PaymentUpdateAsync(Payment ObjPayment, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {"BillNo","ReceiptNo","CashPayAmount","ChequePayAmount","ChequeNo","BankName","ChequeDate","CardPayAmount", "CardNo",  "CardBankName",  "CardDate",  "AdvanceUsedAmount",  "AdvanceId",
           "RefundId",  "TransactionType",  "Remark",  "AddBy",  "IsCancelled",  "IsCancelledBy",  "IsCancelledDate",  "OpdipdType",  "NeftpayAmount",  "Neftno",  "NeftbankMaster",  "Neftdate",  "PayTmamount",  "PayTmtranNo",  "PayTmdate",  "Tdsamount","TranMode",
            "ReceiptNo","CashCounterId","IsSelfOrcompany","CompanyId","ChCashPayAmount","ChChequePayAmount","ChCardPayAmount","ChAdvanceUsedAmount","ChNeftpayAmount","ChPayTmamount"};
            var pentity = ObjPayment.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                pentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("Update_AdmninistartionPaymentDatetime", CommandType.StoredProcedure, pentity);

        }

        public virtual async Task BilldateUpdateAsync(Bill ObjBill, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] AEntity = {  "OpdIpdId","TotalAmt","ConcessionAmt","NetPayableAmt","PaidAmt","BalanceAmt","OpdIpdType","PbillNo","TotalAdvanceAmount","AddedBy","ConcessionReasonId","IsSettled", "IsPrinted","IsFree","CompanyId","TariffId","UnitId","InterimOrFinal","CompanyRefNo","ConcessionAuthorizationName","IsBillCheck","SpeTaxPer",
            "SpeTaxAmt","IsBillShrHold","DiscComments","ChTotalAmt","ChConcessionAmt","ChNetPayAmt","AddCharges","IsCancelled","AdvanceUsedAmount","CashCounterId","CompDiscAmt","BillDetails","BillPrefix","BillMonth","BillYear","PrintBillNo","RegNo","PatientName","Ipdno","AgeYear","AgeMonth","AgeDays","DoctorId","DoctorName","PatientType","CompanyName","CompanyAmt","PatientAmt","WardId","BedId","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var Rentity = ObjBill.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("Update_Administrtaion_BillDate", CommandType.StoredProcedure, Rentity);

            //_context.Bills.Add(ObjBill);
            //await _context.SaveChangesAsync();

        }

        public virtual async Task InsertAsync(MDoctorPerMaster ObjMDoctorPerMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.MDoctorPerMasters.Add(ObjMDoctorPerMaster);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(MDoctorPerMaster ObjMDoctorPerMaster, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header & detail table records
                _context.MDoctorPerMasters.Update(ObjMDoctorPerMaster);
                _context.Entry(ObjMDoctorPerMaster).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task DoctorShareInsertAsync(AddCharge ObjAddCharges, int UserId, string Username, DateTime FromDate, DateTime ToDate)
        {
            DatabaseHelper odal = new();
            string[] AEntity = {  "ChargesId","ChargesDate","OpdIpdType","OpdIpdId","ServiceId","Price","Qty","TotalAmt","ConcessionPercentage","ConcessionAmount","NetAmount","DoctorId", "DocPercentage","DocAmt","HospitalAmt","IsGenerated","AddedBy","IsCancelled","IsCancelledBy","IsCancelledDate","IsPathology","IsRadiology",
            "IsDoctorShareGenerated","IsInterimBillFlag","IsPackage","IsSelfOrCompanyService","PackageId","ChargesTime","PackageMainChargeId","ClassId","RefundAmount","CPrice","CQty","CTotalAmount","IsComServ","IsPrintCompSer","ServiceName","ChPrice","ChQty","ChTotalAmount","IsBillableCharity","SalesId","BillNo","IsHospMrk","BillNoNavigation"};
            var Rentity = ObjAddCharges.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                Rentity.Remove(rProperty);
            }
            Rentity["FromDate"] = FromDate;
            Rentity["ToDate"] = ToDate;

            odal.ExecuteNonQuery("OP_DoctorSharePerCalculation_1", CommandType.StoredProcedure, Rentity);
            odal.ExecuteNonQuery("IP_DoctorSharePerCalculation_1", CommandType.StoredProcedure, Rentity);

        }


        public virtual async Task InsertAsync(List<MAutoServiceList> ObjMAutoServiceList, int UserId, string UserName)
        {

            DatabaseHelper odal = new();


            foreach (var item in ObjMAutoServiceList)
            {
                string[] rEntity = { "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };

                var entity = item.ToDictionary();
                foreach (var rProperty in rEntity)
                {
                    entity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("Insert_ServiceBedCharges", CommandType.StoredProcedure, entity);

            }
        }
             
    }
}


