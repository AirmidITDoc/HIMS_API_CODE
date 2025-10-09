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

      
       
        public virtual async Task DeleteAsync(Admission ObjAdmission, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "AdmissionId" };
            var Rentity = ObjAdmission.ToDictionary();

            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("IP_DISCHARGE_CANCELLATION", CommandType.StoredProcedure, Rentity);
            await _context.LogProcedureExecution(Rentity, nameof(Admission), ObjAdmission.AdmissionId.ToInt(), Core.Domain.Logging.LogAction.Delete, CurrentUserId, CurrentUserName);

        }

        public virtual async Task UpdateAsync(Admission ObjAdmission, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "RegId", "PatientTypeId", "HospitalId", "DocNameId", "RefDocNameId", "WardId", "BedId", "DischargeDate", "DischargeTime", "IsDischarged", "IsBillGenerated", "IPDNo", "IsCancelled", "CompanyId", "TariffId",
            "ClassId","DepartmentId","RelativeName","RelativeAddress","PhoneNo","MobileNo","RelationshipId","AddedBy","IsMlc","MotherName","AdmittedDoctor1","AdmittedDoctor2","IsProcessing",
            "Ischarity","RefByTypeId","RefByName","IsMarkForDisNur","IsMarkForDisNurId","IsMarkForDisNurDateTime","IsCovidFlag","IsCovidUserId","IsCovidUpdateDate","ConvertId",
            "IsUpdatedBy","SubTpaComId","PolicyNo","AprovAmount","CompDod","IsPharClearance","Ipnumber","EstimatedAmount","ApprovedAmount","HosApreAmt","PathApreAmt","PharApreAmt","RadiApreAmt","PharDisc","CompBillNo","CompBillDate","CompDiscount","CompDisDate","CBillNo","CFinalBillAmt","CDisallowedAmt","ClaimNo","HdiscAmt","COutsideInvestAmt","RecoveredByPatient","HChargeAmt",
            "HAdvAmt","HBillId","HBillDate","HBillNo","HTotalAmt","HDiscAmt1","HNetAmt","HPaidAmt","HBalAmt","IsOpToIpconv","RefDoctorDept","AdmissionType","MedicalApreAmt","AdminPer","AdminAmt","SubTpacomp","IsCtoH","IsInitinatedDischarge","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var Rentity = ObjAdmission.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_Update_AdmissionDateTime", CommandType.StoredProcedure, Rentity);

        }

        public virtual async Task PaymentUpdateAsync(Payment ObjPayment, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {"BillNo","ReceiptNo","CashPayAmount","ChequePayAmount","ChequeNo","BankName","ChequeDate","CardPayAmount", "CardNo",  "CardBankName",  "CardDate",  "AdvanceUsedAmount",  "AdvanceId",
           "RefundId",  "TransactionType",  "Remark",  "AddBy",  "IsCancelled",  "IsCancelledBy",  "IsCancelledDate",  "OpdipdType",  "NeftpayAmount",  "Neftno",  "NeftbankMaster",  "Neftdate",  "PayTmamount",  "PayTmtranNo",  "PayTmdate",  "Tdsamount","TranMode",
            "ReceiptNo","CashCounterId","IsSelfOrcompany","CompanyId","ChCashPayAmount","ChChequePayAmount","ChCardPayAmount","ChAdvanceUsedAmount","ChNeftpayAmount","ChPayTmamount","UnitId","Wfamount","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
            var pentity = ObjPayment.ToDictionary();
            foreach (var rProperty in AEntity)
            {
                pentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("Update_AdmninistartionPaymentDatetime", CommandType.StoredProcedure, pentity);

        }

        public virtual async Task BilldateUpdateAsync(Bill ObjBill, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "BillNo", "BillDate", "BillTime" };
            var Rentity = ObjBill.ToDictionary();

            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty)) 
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("Update_Administrtaion_BillDate", CommandType.StoredProcedure, Rentity);
            await _context.LogProcedureExecution(Rentity, nameof(Bill), ObjBill.BillNo.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

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


