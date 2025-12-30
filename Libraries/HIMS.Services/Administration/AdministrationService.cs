using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
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

        public virtual async Task  Update(Admission ObjAdmission, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = { "AdmissionId", "AdmissionDate", "AdmissionTime", "Ipdno"};
            var Rentity = ObjAdmission.ToDictionary();

            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_Update_AdmissionDateTime", CommandType.StoredProcedure, Rentity);
            await _context.LogProcedureExecution(Rentity, nameof(Admission), ObjAdmission.AdmissionId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


        }

        public virtual async Task  PaymentUpdate(Payment ObjPayment, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] AEntity = {"PaymentId","PaymentDate", "PaymentTime" };
            var pentity = ObjPayment.ToDictionary();
            foreach (var rProperty in pentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    pentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("Update_AdmninistartionPaymentDatetime", CommandType.StoredProcedure, pentity);
            await _context.LogProcedureExecution(pentity, nameof(Payment), ObjPayment.PaymentId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);


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


        public virtual void Insert(List<MAutoServiceList> ObjMAutoServiceList, int UserId, string UserName)
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
      
        public async Task InsertListAsync(List<MAutoServiceList> objList, int userId, string username)
        {
            // 1. DELETE ALL OLD RECORDS
            var oldRecords = await _context.MAutoServiceLists.ToListAsync();
            _context.MAutoServiceLists.RemoveRange(oldRecords);

            // 2. INSERT NEW RECORDS
            foreach (var item in objList)
            {
                item.SysId = 0;                 
                item.ServiceId = item.ServiceId; 
                item.CreatedBy = userId;
                item.CreatedDate = AppTime.Now;
                item.ModifiedBy = userId;
                item.ModifiedDate = AppTime.Now;

                _context.MAutoServiceLists.Add(item);
            }
            await _context.SaveChangesAsync();
        }

    }
}


