using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
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
    public  class AdministrationService: IAdministrationService
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

        public virtual async Task InsertAsync(TExpense ObjTExpense, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TExpenses.Add(ObjTExpense);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        public virtual async Task UpdateAsync(TExpense ObjTExpense, int UserId, string Username, string[] strings)
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
            string[] AEntity = {  "ExpDate", "ExpTime", "ExpType", "ExpAmount", "PersonName", "Narration", "IsAddedby", "IsCancelled", "VoucharNo", "ExpHeadId"};
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
            string[] AEntity = { "RegID", "AdmissionDate", "AdmissionTime", "PatientTypeID", "HospitalID", "DocNameID", "RefDocNameID", "WardId", "BedId", "DischargeDate", "DischargeTime", "IsDischarged", "IsBillGenerated", "IPDNo", "IsCancelled", "CompanyId", "TariffId",
            "ClassId","DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMLC","MotherName","AdmittedDoctor1","AdmittedDoctor2","IsProcessing",
            "Ischarity","RefByTypeId","RefByName","IsMarkForDisNur","IsMarkForDisNurId","IsMarkForDisNurDateTime","IsCovidFlag","IsCovidUserId","IsCovidUpdateDate",
            "isUpdatedBy","SubTpaComId","PolicyNo","AprovAmount","CompDOD","IsPharClearance","IPNumber","EstimatedAmount","ApprovedAmount","HosApreAmt","PathApreAmt","PharApreAmt","RadiApreAmt","PharDisc","CompBillNo","CompBillDate","CompDiscount","CompDisDate","C_BillNo","C_FinalBillAmt","C_DisallowedAmt","ClaimNo","HDiscAmt","C_OutsideInvestAmt","RecoveredByPatient","H_ChargeAmt",
            "H_AdvAmt","H_BillId","H_BillDate","H_BillNo","H_TotalAmt","H_DiscAmt","H_NetAmt","H_PaidAmt","H_BalAmt","IsOpToIPConv","RefDoctorDept","AdmissionType","MedicalApreAmt","AdminPer","AdminAmt","SubTPAComp","IsCtoH","IsInitinatedDischarge"};
            var entity = ObjAdmission.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("IP_DISCHARGE_CANCELLATION", CommandType.StoredProcedure, entity);

        }

    }
}
