using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells.Drawing;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using HIMS.Services.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using static LinqToDB.Common.Configuration;
using HIMS.Data.DTO.Administration;

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
            return await DatabaseHelper.GetGridDataBySp<RefundOfAdvancesListDto>(model, "m_Rtrv_RefundOfAdvance");
        }
        //SHILPA CODE////
        public virtual async Task InsertAdvanceAsyncSP(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objPayment, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "AdvanceDetails" };
            var entity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string vAdvanceId = odal.ExecuteNonQuery("sp_insert_AdvanceHeader_1", CommandType.StoredProcedure, "AdvanceId", entity);
            objAdvanceHeader.AdvanceId = Convert.ToInt32(vAdvanceId);
            objAdvanceDetail.AdvanceId = Convert.ToInt32(vAdvanceId);


            string[] rDetailEntity = { "AdvanceNo", "Advance" };

            var AdvanceEntity = objAdvanceDetail.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                AdvanceEntity.Remove(rProperty);
            }
            string AdvanceDetailId = odal.ExecuteNonQuery("sp_insert_AdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", AdvanceEntity);
            objPayment.AdvanceId = Convert.ToInt32(AdvanceDetailId);


            string[] PayEntity = { "PaymentId", "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            var PAdvanceEntity = objPayment.ToDictionary();
            foreach (var rProperty in PayEntity)
            {
                PAdvanceEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("sp_m_insert_Payment_1", CommandType.StoredProcedure, PAdvanceEntity);


        }
        //SHILPA CODE////
        public virtual async Task UpdateAdvanceSP(AdvanceHeader objAdvanceHeader, AdvanceDetail objAdvanceDetail, Payment objPayment, int UserId, string UserName)
        {
            //throw new NotImplementedException();
            DatabaseHelper odal = new();
            string[] rDetailEntity = { "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceUsedAmount", "BalanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" };
            var AdvanceEntity = objAdvanceHeader.ToDictionary();
            foreach (var rProperty in rDetailEntity)
            {
                AdvanceEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("sp_Update_AdvHeader_1", CommandType.StoredProcedure, AdvanceEntity);

            string[] DetailEntity = { "AdvanceNo", "Advance" };
            var AAdvanceEntity = objAdvanceDetail.ToDictionary();
            foreach (var rProperty in DetailEntity)
            {
                AAdvanceEntity.Remove(rProperty);
            }
            string AdvanceDetailId = odal.ExecuteNonQuery("sp_insert_AdvanceDetail_1", CommandType.StoredProcedure, "AdvanceDetailId", AAdvanceEntity);
            objPayment.AdvanceId = Convert.ToInt32(AdvanceDetailId);


            string[] PayEntity = { "PaymentId", "CashCounterId", "IsSelfOrcompany", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            var PAdvanceEntity = objPayment.ToDictionary();
            foreach (var rProperty in PayEntity)
            {
                PAdvanceEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("sp_m_insert_Payment_1", CommandType.StoredProcedure, PAdvanceEntity);

        }

        public virtual async Task IPInsertAsyncSP(Refund Objrefund, AdvanceHeader ObjAdvanceHeader, AdvRefundDetail ObjadvRefundDetail, AdvanceDetail ObjAdvanceDetail, Payment ObjPayment, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            string[] rEntity = { "RefundNo", "CashCounterId", "IsRefundFlag", "CreatedBy", "ModifiedBy", "CreatedDate", "ModifiedDate", "TRefundDetails" };
            var entity = Objrefund.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            string RefundId = odal.ExecuteNonQuery("insert_IPAdvRefund_1", CommandType.StoredProcedure, "RefundId", entity);
            Objrefund.RefundId = Convert.ToInt32(RefundId);

            string[] AHeaderEntity = { "Date", "RefId", "OpdIpdType", "OpdIpdId", "AdvanceAmount", "AddedBy", "IsCancelled", "IsCancelledBy", "IsCancelledDate", "AdvanceDetails" };
            var AdvanceHeaderEntity = ObjAdvanceHeader.ToDictionary();
            foreach (var rProperty in AHeaderEntity)
            {
                AdvanceHeaderEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("update_AdvanceHeader_1", CommandType.StoredProcedure, AdvanceHeaderEntity);


            string[] ADetailEntity = { "AdvRefId" };
            var AdvDetailEntity = ObjadvRefundDetail.ToDictionary();
            foreach (var rProperty in ADetailEntity)
            {
                AdvDetailEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("insert_AdvRefundDetail_1", CommandType.StoredProcedure, AdvDetailEntity);

            string[] AdvanceDetailEntity = { "Date", "Time", "AdvanceId", "AdvanceNo", "RefId", "TransactionId", "OpdIpdId", "OpdIpdType", "AdvanceAmount", "UsedAmount", "ReasonOfAdvanceId", "AddedBy", "IsCancelled", "IsCancelledby", "IsCancelledDate", "Reason", "Advance" };
            var AdDetailEntity = ObjAdvanceDetail.ToDictionary();
            foreach (var rProperty in AdvanceDetailEntity)
            {
                AdDetailEntity.Remove(rProperty);
            }
            odal.ExecuteNonQuery("update_AdvanceDetailBalAmount_1", CommandType.StoredProcedure, AdDetailEntity);

            string[] pEntity = { "PaymentId", "IsSelfOrcompany", "CashCounterId", "CompanyId", "ChCashPayAmount", "ChChequePayAmount", "ChCardPayAmount", "ChAdvanceUsedAmount", "ChNeftpayAmount", "ChPayTmamount", "TranMode" };
            var entity1 = ObjPayment.ToDictionary();
            foreach (var rProperty in pEntity)
            {
                entity1.Remove(rProperty);
            }
            odal.ExecuteNonQuery("m_insert_Payment_1", CommandType.StoredProcedure, entity1);
        }

    }
}





