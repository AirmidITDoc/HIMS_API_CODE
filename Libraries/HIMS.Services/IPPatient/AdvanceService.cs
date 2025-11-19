using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System.Data;

namespace HIMS.Services.IPPatient
{
    public class AdvanceService : IAdvanceService
    {
        private readonly HIMSDbContext _context;
        public AdvanceService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<PatientWiseAdvanceListDto>> PatientWiseAdvanceList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PatientWiseAdvanceListDto>(model, "ps_Rtrv_T_PatientWiseAdvanceList");
        }

        public virtual async Task<IPagedList<AdvanceListDto>> GetAdvanceListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<AdvanceListDto>(model, "ps_Rtrv_BrowseIPAdvanceList");
        }

        public virtual async Task<IPagedList<RefundOfAdvanceListDto>> GetRefundOfAdvanceListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RefundOfAdvanceListDto>(model, "ps_Rtrv_BrowseIPRefundAdvanceReceipt");
        }
        public virtual async Task<IPagedList<RefundOfAdvancesListDto>> GetAdvancesListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<RefundOfAdvancesListDto>(model, "ps_Rtrv_RefundOfAdvance");
        }
        //SHILPA CODE////
        public virtual async Task InsertAdvanceAsyncSP(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objPayment, int CurrentUserId, string CurrentUserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceAmount", "AdvanceUsedAmount", "BalanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceId", "UnitId" };
            var entity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }

            string vAdvanceId = odal.ExecuteNonQuery("ps_insert_AdvanceHeader_1", CommandType.StoredProcedure, "AdvanceId", entity);
            objAdvanceHeader.AdvanceId = Convert.ToInt32(vAdvanceId);
            objAdvanceDetail.AdvanceId = Convert.ToInt32(vAdvanceId);
            await _context.LogProcedureExecution(entity, nameof(AdvanceHeader), objAdvanceHeader.AdvanceId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);



            string[] rDetailEntity = { "Date", "Time", "AdvanceId", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "UsedAmount", "BalanceAmount", "RefundAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "AdvanceDetailId", "UnitId", "CashCounterId" };
            var AdvanceEntity = objAdvanceDetail.ToDictionary();
            foreach (var rProperty in AdvanceEntity.Keys.ToList())
            {
                if (!rDetailEntity.Contains(rProperty))
                    AdvanceEntity.Remove(rProperty);
            }

            string AdvanceDetailId = odal.ExecuteNonQuery("ps_insert_AdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", AdvanceEntity);
            objPayment.AdvanceId = Convert.ToInt32(AdvanceDetailId);
            await _context.LogProcedureExecution(entity, nameof(AdvanceDetail), objAdvanceDetail.AdvanceId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);



            string[] PayEntity = { "BillNo", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "UnitId" };
            var PAdvanceEntity = objPayment.ToDictionary();
            foreach (var rProperty in PAdvanceEntity.Keys.ToList())
            {
                if (!PayEntity.Contains(rProperty))
                    PAdvanceEntity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("ps_m_insert_Payment_Advance_1", CommandType.StoredProcedure, PAdvanceEntity);
            await _context.LogProcedureExecution(entity, nameof(Payment), objAdvanceHeader.AdvanceId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);



        }
        //SHILPA CODE////
        public virtual async Task UpdateAdvanceSP(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objPayment, int CurrentUserId, string CurrentUserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rDetailEntity = { "AdvanceId", "AdvanceAmount" };
            var AdvanceEntity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in AdvanceEntity.Keys.ToList())
            {
                if (!rDetailEntity.Contains(rProperty))
                    AdvanceEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_Update_AdvHeader_1", CommandType.StoredProcedure, AdvanceEntity);
            await _context.LogProcedureExecution(AdvanceEntity, nameof(AdvanceHeader), objAdvanceDetail.AdvanceId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

            string[] DetailEntity = { "Date", "Time", "AdvanceId", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "UsedAmount", "BalanceAmount", "RefundAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "AdvanceDetailId", "UnitId", "CashCounterId" };
            var AAdvanceEntity = objAdvanceDetail.ToDictionary();
            foreach (var rProperty in AAdvanceEntity.Keys.ToList())
            {
                if (!DetailEntity.Contains(rProperty))
                    AAdvanceEntity.Remove(rProperty);
            }

            string AdvanceDetailId = odal.ExecuteNonQuery("ps_insert_AdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", AAdvanceEntity);
            objPayment.AdvanceId = Convert.ToInt32(AdvanceDetailId);
            await _context.LogProcedureExecution(AAdvanceEntity, nameof(AdvanceDetail), objAdvanceDetail.AdvanceId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

            string[] PayEntity = { "BillNo", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "UnitId" };
            var PAdvanceEntity = objPayment.ToDictionary();
            foreach (var rProperty in PAdvanceEntity.Keys.ToList())
            {
                if (!PayEntity.Contains(rProperty))
                    PAdvanceEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_m_insert_Payment_Advance_1", CommandType.StoredProcedure, PAdvanceEntity);
            await _context.LogProcedureExecution(PAdvanceEntity, nameof(Payment), objAdvanceHeader.AdvanceId.ToInt(), Core.Domain.Logging.LogAction.Add, CurrentUserId, CurrentUserName);

        }
        public virtual void Cancel(AdvanceHeader ObjAdvanceHeader, long AdvanceDetailId)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rDetailEntity = { "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceUsedAmount", "BalanceAmount", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "UnitId", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
            var CAdvanceEntity = ObjAdvanceHeader.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                CAdvanceEntity.Remove(rProperty);
            }
            CAdvanceEntity["AdvanceDetailId"] = AdvanceDetailId;

            odal.ExecuteNonQuery("m_UpdateAdvanceCancel", CommandType.StoredProcedure, CAdvanceEntity);
        }





        //public virtual void IPInsertSP(Refund Objrefund, AdvanceHeader ObjAdvanceHeader, List<AdvRefundDetail> ObjadvRefundDetailList, List<AdvanceDetail> ObjAdvanceDetailList, Payment ObjPayment, int UserId, string UserName)
        //{

        //    DatabaseHelper odal = new();
        //    string[] rEntity = { "RefundNo", "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate", "TRefundDetails", "AddBy" };
        //    var entity = Objrefund.ToDictionary();
        //    foreach (var rProperty in rEntity)
        //    {
        //        entity.Remove(rProperty);
        //    }
        //    string RefundId = odal.ExecuteNonQuery("ps_insert_IPAdvRefund_1", CommandType.StoredProcedure, "RefundId", entity);
        //    Objrefund.RefundId = Convert.ToInt32(RefundId);
        //    ObjPayment.RefundId = Convert.ToInt32(RefundId);


        //    string[] AHeaderEntity = { "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails",
        //    "UnitId","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
        //    var AdvanceHeaderEntity = ObjAdvanceHeader.ToDictionary();
        //    foreach (var rProperty in AHeaderEntity)
        //    {
        //        AdvanceHeaderEntity.Remove(rProperty);
        //    }
        //    odal.ExecuteNonQuery("ps_update_AdvanceHeader_1", CommandType.StoredProcedure, AdvanceHeaderEntity);

        //    foreach (var item in ObjadvRefundDetailList)
        //    {

        //        string[] ADetailEntity = { "AdvRefId" };
        //        var AdvDetailEntity = item.ToDictionary();
        //        foreach (var rProperty in ADetailEntity)
        //        {
        //            AdvDetailEntity.Remove(rProperty);
        //        }
        //        odal.ExecuteNonQuery("ps_insert_AdvRefundDetail_1", CommandType.StoredProcedure, AdvDetailEntity);
        //    }

        //    foreach (var item in ObjAdvanceDetailList)
        //    {
        //        string[] AdvanceDetailEntity = { "Date", "Time", "AdvanceId", "AdvanceNo", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "UsedAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "Advance",
        //        "UnitId","CashCounterId","CreatedBy","CreatedDate","ModifiedBy","ModifiedDate"};
        //        var AdDetailEntity = item.ToDictionary();
        //        foreach (var rProperty in AdvanceDetailEntity)
        //        {
        //            AdDetailEntity.Remove(rProperty);
        //        }
        //        odal.ExecuteNonQuery("ps_update_AdvanceDetailBalAmount_1", CommandType.StoredProcedure, AdDetailEntity);
        //    }
        //    string[] pEntity = { "PaymentId", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" };
        //    var entity1 = ObjPayment.ToDictionary();
        //    foreach (var rProperty in pEntity)
        //    {
        //        entity1.Remove(rProperty);
        //    }
        //    odal.ExecuteNonQuery("ps_m_insert_Payment_Advance_1", CommandType.StoredProcedure, entity1);
        //}
        public virtual void IPInsertSP(Refund Objrefund, AdvanceHeader ObjAdvanceHeader, List<AdvRefundDetail> ObjadvRefundDetailList, List<AdvanceDetail> ObjAdvanceDetailList, Payment ObjPayment, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "RefundDate", "RefundTime", "BillId", "AdvanceId", "OpdIpdType", "OpdIpdId", "RefundAmount", "Remark", "TransactionId", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "RefundId", "UnitId" };
            var entity = Objrefund.ToDictionary();
            foreach (var rProperty in entity.Keys.ToList())
            {
                if (!rEntity.Contains(rProperty))
                    entity.Remove(rProperty);
            }
            string RefundId = odal.ExecuteNonQuery("ps_insert_IPAdvRefund_1", CommandType.StoredProcedure, "RefundId", entity);
            Objrefund.RefundId = Convert.ToInt32(RefundId);
            ObjPayment.RefundId = Convert.ToInt32(RefundId);


            string[] AHeaderEntity = { "AdvanceId", "AdvanceUsedAmount", "BalanceAmount" };
            var AdvanceHeaderEntity = ObjAdvanceHeader.ToDictionary();
            foreach (var rProperty in AdvanceHeaderEntity.Keys.ToList())
            {
                if (!AHeaderEntity.Contains(rProperty))
                    AdvanceHeaderEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_update_AdvanceHeader_1", CommandType.StoredProcedure, AdvanceHeaderEntity);

            foreach (var item in ObjadvRefundDetailList)
            {

                string[] ADetailEntity = { "AdvDetailId", "RefundDate", "RefundTime", "AdvRefundAmt" };
                var AdvDetailEntity = item.ToDictionary();
                foreach (var rProperty in AdvDetailEntity.Keys.ToList())
                {
                    if (!ADetailEntity.Contains(rProperty))
                        AdvDetailEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_insert_AdvRefundDetail_1", CommandType.StoredProcedure, AdvDetailEntity);
            }

            foreach (var item in ObjAdvanceDetailList)
            {
                string[] AdvanceDetailEntity = { "AdvanceDetailId", "BalanceAmount", "RefundAmount" };
                var AdDetailEntity = item.ToDictionary();
                foreach (var rProperty in AdDetailEntity.Keys.ToList())
                {
                    if (!AdvanceDetailEntity.Contains(rProperty))
                        AdDetailEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("ps_update_AdvanceDetailBalAmount_1", CommandType.StoredProcedure, AdDetailEntity);
            }
            string[] PEntity = { "BillNo", "ReceiptNo", "PaymentDate", "PaymentTime", "CashPayAmount", "ChequePayAmount", "ChequeNo", "BankName", "ChequeDate", "CardPayAmount", "CardNo", "CardBankName", "CardDate", "AdvanceUsedAmount", "AdvanceId", "RefundId", "TransactionType", "Remark", "AddBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "NeftpayAmount", "Neftno", "NeftbankMaster", "Neftdate", "PayTmamount", "PayTmtranNo", "PayTmdate", "Tdsamount", "Wfamount", "UnitId" };
            var entity1 = ObjPayment.ToDictionary();
            foreach (var rProperty in entity1.Keys.ToList())
            {
                if (!PEntity.Contains(rProperty))
                    entity1.Remove(rProperty);
            }
            odal.ExecuteNonQuery("ps_m_insert_Payment_Advance_1", CommandType.StoredProcedure, entity1);
        }


        public virtual async Task UpdateAdvance(AdvanceDetail OBJAdvanceDetail, int CurrentUserId, string CurrentUserName)
        {
            DatabaseHelper odal = new();
            string[] AEntity = { "Date", "Time", "AdvanceDetailId" };
            var Rentity = OBJAdvanceDetail.ToDictionary();

            foreach (var rProperty in Rentity.Keys.ToList())
            {
                if (!AEntity.Contains(rProperty))
                    Rentity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("Ps_Update_advancedetail", CommandType.StoredProcedure, Rentity);
            await _context.LogProcedureExecution(Rentity, nameof(Refund), OBJAdvanceDetail.AdvanceDetailId.ToInt(), Core.Domain.Logging.LogAction.Edit, CurrentUserId, CurrentUserName);

        }


    }

}





