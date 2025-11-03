using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Transactions;

namespace HIMS.Services.DoctorPayout
{
    public class DoctorPayService : IDoctorPayService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public DoctorPayService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        public virtual async Task<IPagedList<DoctorPayListDto>> GetList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorPayListDto>(model, "ps_rtrv_T_AdditionalDocPay_List");
        }

        public virtual async Task<IPagedList<DoctorBilldetailListDto>> GetBillDetailList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorBilldetailListDto>(model, "Rtrv_IPBillForDocShr");
        }
        public virtual async Task<IPagedList<DcotorpaysummaryListDto>> GetDoctroSummaryList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DcotorpaysummaryListDto>(model, "ps_rtrv_DoctorWiseShareAmount");
        }
        public virtual async Task<IPagedList<DoctorPaysummarydetailListDto>> GetDoctorsummaryDetailList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorPaysummarydetailListDto>(model, "ps_view_DoctorPayoutPatientSummary");
        }
        public virtual async Task<IPagedList<DoctorShareListDto>> GetLists(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorShareListDto>(model, "PS_Rtrv_BillListForDocShr");
        }
        public virtual async Task<IPagedList<DoctorShareLbyNameListDto>> GetList1(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorShareLbyNameListDto>(model, "PS_m_Rtrv_DoctorShareList_by_Name");
        }




        public virtual async Task InsertAsync(TAdditionalDocPay ObjTAdditionalDocPay, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "TranDate", "TranTime", "PbillNo", "CompanyId", "BillDate", "ServiceName", "Price", "Qty", "TotalAmount", "DocAmount", "IsProcess", "DocId", "PatientName", "TranId" };
            var entity = ObjTAdditionalDocPay.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_insert_T_AdditionalDocPay_1", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(TAdditionalDocPay), (int)ObjTAdditionalDocPay.TranId, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
        }
        public virtual async Task UpdateAsync(List<AddCharge> ObjAddCharge, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            foreach (var item in ObjAddCharge)
            {

                string[] rEntity = { "DocAmt", "HospitalAmt", "ChargesId" };

                var Aentity = item.ToDictionary();
                foreach (var rProperty in Aentity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        Aentity.Remove(rProperty);
                }


                odal.ExecuteNonQuery("ps_Update_ShrDoc_AddChar_1", CommandType.StoredProcedure, Aentity);
                //await _context.LogProcedureExecution(Aentity, nameof(AddCharge), ObjAddCharge.ChargesId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

            }

        }
        public virtual void InsertSP(TDoctorPayoutProcessHeader ObjTDoctorPayoutProcessHeader, List<TDoctorPayoutProcessDetail> ObjTDoctorPayoutProcessDetail, int UserId, string Username)
        {

            DatabaseHelper odal = new();
            string[] rEntity = {  "DoctorId", "ProcessStartDate", "ProcessEndDate", "ProcessDate", "NetAmount", "DoctorAmount", "HospitalAmount", "Tdsamount", "CreatedBy" };

            var dentity = ObjTDoctorPayoutProcessHeader.ToDictionary();
            foreach (var rProperty in dentity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    dentity.Remove(rProperty);
            }
            string DoctorPayoutId = odal.ExecuteNonQuery("ps_Insert_T_DoctorPayoutProcessHeader", CommandType.StoredProcedure, "DoctorPayoutId", dentity);
            ObjTDoctorPayoutProcessHeader.DoctorPayoutId = Convert.ToInt32(DoctorPayoutId);

            foreach (var item in ObjTDoctorPayoutProcessDetail)
            {
                item.DoctorPayoutId = Convert.ToInt32(DoctorPayoutId);
                string[] rRefundEntity = { "DoctorPayoutDetId", "DoctorPayoutId", "DoctorId", "ChargeId", "CreatedBy" };
                var RefundEntity = item.ToDictionary();
                foreach (var rProperty in RefundEntity.Keys.ToList())
                {
                    if (!rRefundEntity.Contains(rProperty))
                        RefundEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_Insert_DoctorPayoutProcessDetails", CommandType.StoredProcedure, RefundEntity);
            }
        }



        //public virtual async Task InsertAsync(TDoctorPayoutProcessHeader ObjTDoctorPayoutProcessHeader, int UserId, string Username)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        _context.TDoctorPayoutProcessHeaders.Add(ObjTDoctorPayoutProcessHeader);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}

        //public virtual async Task UpdateAsync(TDoctorPayoutProcessHeader ObjTDoctorPayoutProcessHeader, int UserId, string Username, string[]? ignoreColumns = null)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // 1. Attach the entity without marking everything as modified
        //        _context.Attach(ObjTDoctorPayoutProcessHeader);
        //        _context.Entry(ObjTDoctorPayoutProcessHeader).State = EntityState.Modified;

        //        // 2. Ignore specific columns
        //        if (ignoreColumns?.Length > 0)
        //        {
        //            foreach (var column in ignoreColumns)
        //            {
        //                _context.Entry(ObjTDoctorPayoutProcessHeader).Property(column).IsModified = false;
        //            }
        //        }
        //        //Delete details table realted records
        //        var lst = await _context.TDoctorPayoutProcessDetails.Where(x => x.DoctorPayoutId == ObjTDoctorPayoutProcessHeader.DoctorPayoutId).ToListAsync();
        //        if (lst.Count > 0)
        //        {
        //            _context.TDoctorPayoutProcessDetails.RemoveRange(lst);
        //        }

        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}
        public virtual async Task InsertAsync(AddCharge ObjAddCharge, int CurrentUserId, string CurrentUserName)
            {
            DatabaseHelper odal = new();
            string[] AEntity = { "BillNo", "DoctorId" };
            var entity = ObjAddCharge.ToDictionary();

            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }


            odal.ExecuteNonQuery("ps_IP_DoctorShrCalcAsPerReferDocVisitBillWise_]", CommandType.StoredProcedure, entity);
            await _context.LogProcedureExecution(entity, nameof(TAdditionalDocPay), (int)ObjAddCharge.BillNo, Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);
        }

        public virtual async Task<IPagedList<DoctorShareprocessListDto>> GetDoctorProcessList(GridRequestModel objGrid)
        {
            return await DatabaseHelper.GetGridDataBySp<DoctorShareprocessListDto>(objGrid, "ps_Rtrv_T_DoctorPayoutProcesslist");
        }
    }
}

